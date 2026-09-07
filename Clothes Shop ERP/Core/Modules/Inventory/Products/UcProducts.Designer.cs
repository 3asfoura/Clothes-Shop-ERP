namespace Clothes_Shop_ERP.modlestore
{
    partial class UcProducts
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
            this.ColCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColBasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Col = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(308, 260);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColCode,
            this.ColName,
            this.ColBasePrice,
            this.Col});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // ColCode
            // 
            this.ColCode.Caption = "Code";
            this.ColCode.FieldName = "Code";
            this.ColCode.Name = "ColCode";
            this.ColCode.Visible = true;
            this.ColCode.VisibleIndex = 0;
            this.ColCode.Width = 183;
            // 
            // ColName
            // 
            this.ColName.Caption = "Name";
            this.ColName.FieldName = "Name";
            this.ColName.Name = "ColName";
            this.ColName.Visible = true;
            this.ColName.VisibleIndex = 1;
            this.ColName.Width = 816;
            // 
            // ColBasePrice
            // 
            this.ColBasePrice.Caption = "BasePrice";
            this.ColBasePrice.FieldName = "BasePrice";
            this.ColBasePrice.Name = "ColBasePrice";
            this.ColBasePrice.Visible = true;
            this.ColBasePrice.VisibleIndex = 2;
            this.ColBasePrice.Width = 407;
            // 
            // Col
            // 
            this.Col.Caption = "IsActive";
            this.Col.FieldName = "IsActive";
            this.Col.Name = "Col";
            this.Col.Visible = true;
            this.Col.VisibleIndex = 3;
            this.Col.Width = 208;
            // 
            // UcProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UcProducts";
            this.Size = new System.Drawing.Size(308, 260);
            this.Load += new System.EventHandler(this.UcProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ColCode;
        private DevExpress.XtraGrid.Columns.GridColumn ColName;
        private DevExpress.XtraGrid.Columns.GridColumn ColBasePrice;
        private DevExpress.XtraGrid.Columns.GridColumn Col;
    }
}
