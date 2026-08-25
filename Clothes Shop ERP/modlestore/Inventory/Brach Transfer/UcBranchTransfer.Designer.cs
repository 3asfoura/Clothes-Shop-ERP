namespace Clothes_Shop_ERP.modlestore
{
    partial class UcBranchTransfer
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
            this.ColFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(631, 444);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColFrom,
            this.Col,
            this.ColStatus,
            this.ColCreatedAt});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // ColFrom
            // 
            this.ColFrom.Caption = "From";
            this.ColFrom.FieldName = "From";
            this.ColFrom.Name = "ColFrom";
            this.ColFrom.Visible = true;
            this.ColFrom.VisibleIndex = 0;
            // 
            // Col
            // 
            this.Col.Caption = "To";
            this.Col.FieldName = "To";
            this.Col.Name = "Col";
            this.Col.Visible = true;
            this.Col.VisibleIndex = 1;
            // 
            // ColStatus
            // 
            this.ColStatus.Caption = "Status";
            this.ColStatus.FieldName = "Status";
            this.ColStatus.Name = "ColStatus";
            this.ColStatus.Visible = true;
            this.ColStatus.VisibleIndex = 2;
            // 
            // ColCreatedAt
            // 
            this.ColCreatedAt.Caption = "CreatedAt";
            this.ColCreatedAt.FieldName = "CreatedAt";
            this.ColCreatedAt.Name = "ColCreatedAt";
            this.ColCreatedAt.Visible = true;
            this.ColCreatedAt.VisibleIndex = 3;
            // 
            // UcBranchTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UcBranchTransfer";
            this.Size = new System.Drawing.Size(631, 444);
            this.Load += new System.EventHandler(this.UcBranchTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ColFrom;
        private DevExpress.XtraGrid.Columns.GridColumn Col;
        private DevExpress.XtraGrid.Columns.GridColumn ColStatus;
        private DevExpress.XtraGrid.Columns.GridColumn ColCreatedAt;
    }
}
