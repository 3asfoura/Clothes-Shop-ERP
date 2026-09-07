namespace Clothes_Shop_ERP.modlestore
{
    partial class UcPurchaseInvoices
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColInvoiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColPaidAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(652, 424);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColSupplier,
            this.ColBranch,
            this.ColInvoiceDate,
            this.ColTotalAmount,
            this.ColPaidAmount,
            this.ColStatus});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // ColSupplier
            // 
            this.ColSupplier.Caption = "Supplier";
            this.ColSupplier.FieldName = "Supplier";
            this.ColSupplier.Name = "ColSupplier";
            this.ColSupplier.Visible = true;
            this.ColSupplier.VisibleIndex = 0;
            // 
            // ColBranch
            // 
            this.ColBranch.Caption = "Branch";
            this.ColBranch.FieldName = "Branch";
            this.ColBranch.Name = "ColBranch";
            this.ColBranch.Visible = true;
            this.ColBranch.VisibleIndex = 1;
            // 
            // ColInvoiceDate
            // 
            this.ColInvoiceDate.Caption = "InvoiceDate";
            this.ColInvoiceDate.FieldName = "InvoiceDate";
            this.ColInvoiceDate.Name = "ColInvoiceDate";
            this.ColInvoiceDate.Visible = true;
            this.ColInvoiceDate.VisibleIndex = 2;
            // 
            // ColTotalAmount
            // 
            this.ColTotalAmount.Caption = "TotalAmount";
            this.ColTotalAmount.FieldName = "TotalAmount";
            this.ColTotalAmount.Name = "ColTotalAmount";
            this.ColTotalAmount.Visible = true;
            this.ColTotalAmount.VisibleIndex = 3;
            // 
            // ColPaidAmount
            // 
            this.ColPaidAmount.Caption = "PaidAmount";
            this.ColPaidAmount.FieldName = "PaidAmount";
            this.ColPaidAmount.Name = "ColPaidAmount";
            this.ColPaidAmount.Visible = true;
            this.ColPaidAmount.VisibleIndex = 4;
            // 
            // ColStatus
            // 
            this.ColStatus.Caption = "Status";
            this.ColStatus.FieldName = "Status";
            this.ColStatus.Name = "ColStatus";
            this.ColStatus.Visible = true;
            this.ColStatus.VisibleIndex = 5;
            // 
            // UcPurchaseInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UcPurchaseInvoices";
            this.Size = new System.Drawing.Size(652, 424);
            this.Load += new System.EventHandler(this.UcPurchaseInvoices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ColSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn ColBranch;
        private DevExpress.XtraGrid.Columns.GridColumn ColInvoiceDate;
        private DevExpress.XtraGrid.Columns.GridColumn ColTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ColPaidAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ColStatus;
    }
}
