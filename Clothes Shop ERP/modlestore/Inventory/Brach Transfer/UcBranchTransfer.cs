using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using TransferEntity = Clothes_Shop_ERP.DAL.StockTransfers;
using TransferDetailEntity = Clothes_Shop_ERP.DAL.StockTransferDetails;
using StockMovementEntity = Clothes_Shop_ERP.DAL.StockMovements;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcBranchTransfer : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBranchTransfer()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColFrom.Caption = LocalizationManager.T("Shared_ColFrom");
            Col.Caption = LocalizationManager.T("Shared_ColTo");
            ColStatus.Caption = LocalizationManager.T("Shared_Status");
            ColCreatedAt.Caption = LocalizationManager.T("Shared_CreatedAt");
        }

        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.StockTransfers
                    .Include(x => x.FromBranch)
                    .Include(x => x.ToBranch)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => new
                    {
                        x.Id,
                        From = x.FromBranch.Name,
                        To = x.ToBranch.Name,
                        x.Status,
                        x.CreatedAt
                    })
                    .ToList();
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
           
        }


        private void AddNew()
        {
            var form = new FrmStockTransferEdit(LocalizationManager.T("BranchTransfer_NewTitle"));
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var transfer = new TransferEntity
                    {
                        FromBranchId = form.FromBranchId,
                        ToBranchId = form.ToBranchId,
                        Status = "Pending",
                        CreatedAt = DateTime.Now,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    };
                    db.StockTransfers.Add(transfer);
                    db.SaveChanges();   // generates transfer.Id for the lines below

                    foreach (var line in form.Lines)
                    {
                        db.StockTransferDetails.Add(new TransferDetailEntity
                        {
                            StockTransferId = transfer.Id,
                            ProductVariantId = line.ProductVariantId,
                            Quantity = line.Quantity
                        });
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    Sett.MsgBlue(LocalizationManager.T("Shared_Success"), LocalizationManager.T("BranchTransfer_CreatedPending"));
                    GetData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("BranchTransfer_CreateFailed"), ex.Message));
                }
            }
        }

        private void SetStatus(string newStatus)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var transfer = db.StockTransfers.Where(x => x.Id == id).FirstOrDefault();
                    if (transfer == null || transfer.Status != "Pending")
                    {
                        Sett.MsgBlue(LocalizationManager.T("Shared_Error"), LocalizationManager.T("BranchTransfer_Locked"));
                        return;
                    }

                    if (newStatus == "Completed")
                    {
                        var lines = db.StockTransferDetails.Where(d => d.StockTransferId == id).ToList();

                        foreach (var line in lines)
                        {
                            // Safely take stock out of the source branch — fails cleanly if
                            // it no longer has enough (someone may have sold it meanwhile).
                            int rowsAffected = db.Database.ExecuteSqlCommand(
                                "UPDATE BranchStock SET Quantity = Quantity - {0} WHERE ProductVariantId = {1} AND BranchId = {2} AND Quantity >= {0}",
                                line.Quantity, line.ProductVariantId, transfer.FromBranchId);

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                Sett.MsgBlue(LocalizationManager.T("Shared_Error"), LocalizationManager.T("BranchTransfer_NotEnoughStock"));
                                return;
                            }

                            db.StockMovements.Add(new StockMovementEntity
                            {
                                ProductVariantId = line.ProductVariantId,
                                BranchId = transfer.FromBranchId,
                                MovementType = "TransferOut",
                                Quantity = -line.Quantity,
                                RefType = "StockTransfer",
                                RefId = transfer.Id,
                                CreatedAt = DateTime.Now,
                                CreatedByUserId = FrmLogin.CurrentUserId
                            });

                            // Add stock into the destination branch
                            var destStock = db.BranchStock.FirstOrDefault(s =>
                                s.ProductVariantId == line.ProductVariantId && s.BranchId == transfer.ToBranchId);

                            if (destStock == null)
                            {
                                db.BranchStock.Add(new Clothes_Shop_ERP.DAL.BranchStock
                                {
                                    ProductVariantId = line.ProductVariantId,
                                    BranchId = transfer.ToBranchId,
                                    Quantity = line.Quantity,
                                    MinQuantity = 0
                                });
                            }
                            else
                            {
                                destStock.Quantity += line.Quantity;
                            }

                            db.StockMovements.Add(new StockMovementEntity
                            {
                                ProductVariantId = line.ProductVariantId,
                                BranchId = transfer.ToBranchId,
                                MovementType = "TransferIn",
                                Quantity = line.Quantity,
                                RefType = "StockTransfer",
                                RefId = transfer.Id,
                                CreatedAt = DateTime.Now,
                                CreatedByUserId = FrmLogin.CurrentUserId
                            });
                        }
                    }

                    transfer.Status = newStatus;
                    db.SaveChanges();
                    transaction.Commit();

                    Sett.MsgGreen(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("BranchTransfer_StatusChanged"), newStatus));
                    GetData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("BranchTransfer_UpdateFailed"), ex.Message));
                }
            }
        }

        private void UcBranchTransfer_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = gridView1.CalcHitInfo(e.Location);
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            menu.Items.Add(LocalizationManager.T("BranchTransfer_MenuNewTransfer"), null, (s, ev) => AddNew());

            if (hit.InRow)
            {
                gridView1.FocusedRowHandle = hit.RowHandle;
                string status = gridView1.GetFocusedRowCellValue("Status")?.ToString();
                if (status == "Pending")
                {
                    menu.Items.Add(LocalizationManager.T("BranchTransfer_MenuMarkCompleted"), null, (s, ev) => SetStatus("Completed"));
                    menu.Items.Add(LocalizationManager.T("BranchTransfer_MenuCancelTransfer"), null, (s, ev) => SetStatus("Cancelled"));
                }
            }

            menu.Show(gridControl1, e.Location);
        }
    }
}
