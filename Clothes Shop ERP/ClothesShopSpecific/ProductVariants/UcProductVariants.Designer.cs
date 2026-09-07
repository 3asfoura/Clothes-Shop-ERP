namespace Clothes_Shop_ERP
{
    partial class UcProductVariants
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
            this.ColProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColColor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColCostPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(499, 352);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColProductName,
            this.ColColor,
            this.ColSize,
            this.ColBarcode,
            this.ColSalePrice,
            this.ColCostPrice,
            this.ColIsActive});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // ColProductName
            // 
            this.ColProductName.Caption = "ProductName";
            this.ColProductName.FieldName = "ProductName";
            this.ColProductName.Name = "ColProductName";
            this.ColProductName.Visible = true;
            this.ColProductName.VisibleIndex = 0;
            this.ColProductName.Width = 386;
            // 
            // ColColor
            // 
            this.ColColor.Caption = "Color";
            this.ColColor.FieldName = "Color";
            this.ColColor.Name = "ColColor";
            this.ColColor.Visible = true;
            this.ColColor.VisibleIndex = 2;
            this.ColColor.Width = 101;
            // 
            // ColSize
            // 
            this.ColSize.Caption = "Size";
            this.ColSize.FieldName = "Size";
            this.ColSize.Name = "ColSize";
            this.ColSize.Visible = true;
            this.ColSize.VisibleIndex = 1;
            this.ColSize.Width = 90;
            // 
            // ColBarcode
            // 
            this.ColBarcode.Caption = "Barcode";
            this.ColBarcode.FieldName = "Barcode";
            this.ColBarcode.Name = "ColBarcode";
            this.ColBarcode.Visible = true;
            this.ColBarcode.VisibleIndex = 3;
            this.ColBarcode.Width = 330;
            // 
            // ColSalePrice
            // 
            this.ColSalePrice.Caption = "SalePrice";
            this.ColSalePrice.FieldName = "SalePrice";
            this.ColSalePrice.Name = "ColSalePrice";
            this.ColSalePrice.Visible = true;
            this.ColSalePrice.VisibleIndex = 4;
            this.ColSalePrice.Width = 260;
            // 
            // ColCostPrice
            // 
            this.ColCostPrice.Caption = "CostPrice";
            this.ColCostPrice.FieldName = "CostPrice";
            this.ColCostPrice.Name = "ColCostPrice";
            this.ColCostPrice.Visible = true;
            this.ColCostPrice.VisibleIndex = 5;
            this.ColCostPrice.Width = 303;
            // 
            // ColIsActive
            // 
            this.ColIsActive.Caption = "IsActive";
            this.ColIsActive.FieldName = "IsActive";
            this.ColIsActive.Name = "ColIsActive";
            this.ColIsActive.Visible = true;
            this.ColIsActive.VisibleIndex = 6;
            this.ColIsActive.Width = 144;
            // 
            // UcProductVariants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UcProductVariants";
            this.Size = new System.Drawing.Size(499, 352);
            this.Load += new System.EventHandler(this.UcProductVariants_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ColProductName;
        private DevExpress.XtraGrid.Columns.GridColumn ColBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn ColSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn ColCostPrice;
        private DevExpress.XtraGrid.Columns.GridColumn ColIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn ColColor;
        private DevExpress.XtraGrid.Columns.GridColumn ColSize;
    }
}
