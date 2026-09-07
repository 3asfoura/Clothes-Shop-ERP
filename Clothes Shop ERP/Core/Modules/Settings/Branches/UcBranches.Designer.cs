namespace Clothes_Shop_ERP.modlestore
{
    partial class UcBranches
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
            this.ColName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColPhone = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl1.Size = new System.Drawing.Size(701, 471);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColName,
            this.ColAddress,
            this.ColPhone,
            this.ColIsActive});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // ColName
            // 
            this.ColName.Caption = "Name";
            this.ColName.FieldName = "Name";
            this.ColName.Name = "ColName";
            this.ColName.Visible = true;
            this.ColName.VisibleIndex = 0;
            // 
            // ColAddress
            // 
            this.ColAddress.Caption = "Address";
            this.ColAddress.FieldName = "Address";
            this.ColAddress.Name = "ColAddress";
            this.ColAddress.Visible = true;
            this.ColAddress.VisibleIndex = 1;
            // 
            // ColPhone
            // 
            this.ColPhone.Caption = "Phone";
            this.ColPhone.FieldName = "Phone";
            this.ColPhone.Name = "ColPhone";
            this.ColPhone.Visible = true;
            this.ColPhone.VisibleIndex = 2;
            // 
            // ColIsActive
            // 
            this.ColIsActive.Caption = "IsActive";
            this.ColIsActive.FieldName = "IsActive";
            this.ColIsActive.Name = "ColIsActive";
            this.ColIsActive.Visible = true;
            this.ColIsActive.VisibleIndex = 3;
            // 
            // UcBranches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "UcBranches";
            this.Size = new System.Drawing.Size(701, 471);
            this.Load += new System.EventHandler(this.UcBranches_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ColName;
        private DevExpress.XtraGrid.Columns.GridColumn ColAddress;
        private DevExpress.XtraGrid.Columns.GridColumn ColPhone;
        private DevExpress.XtraGrid.Columns.GridColumn ColIsActive;
    }
}
