namespace Clothes_Shop_ERP.modlestore
{
    partial class UcPurchaseReturns
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
            this.ColReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(781, 430);
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
            this.ColReturnDate,
            this.ColTotalAmount});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowSort = false;
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
            // ColReturnDate
            //
            this.ColReturnDate.Caption = "ReturnDate";
            this.ColReturnDate.FieldName = "ReturnDate";
            this.ColReturnDate.Name = "ColReturnDate";
            this.ColReturnDate.Visible = true;
            this.ColReturnDate.VisibleIndex = 2;
            //
            // ColTotalAmount
            //
            this.ColTotalAmount.Caption = "TotalAmount";
            this.ColTotalAmount.FieldName = "TotalAmount";
            this.ColTotalAmount.Name = "ColTotalAmount";
            this.ColTotalAmount.Visible = true;
            this.ColTotalAmount.VisibleIndex = 3;
            //
            // UcPurchaseReturns
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UcPurchaseReturns";
            this.Size = new System.Drawing.Size(781, 430);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ColSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn ColBranch;
        private DevExpress.XtraGrid.Columns.GridColumn ColReturnDate;
        private DevExpress.XtraGrid.Columns.GridColumn ColTotalAmount;
    }
}
