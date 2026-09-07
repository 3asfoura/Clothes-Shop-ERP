namespace Clothes_Shop_ERP.modlestore.Settings.Users
{
    partial class UcUsers_Roles
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColUsername = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColRoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dgv_Roles = new DevExpress.XtraGrid.GridControl();
            this.dgv_RolesList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Col_Role = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Roles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RolesList)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.groupControl3);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(711, 502);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.gridControl2);
            this.groupControl3.Location = new System.Drawing.Point(12, 12);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(325, 478);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "Users";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dgv_Roles);
            this.groupControl1.Location = new System.Drawing.Point(341, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(358, 478);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Roles";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(711, 502);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl3;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(329, 482);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(329, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(362, 482);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 22);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(321, 454);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl2_MouseUp);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColUsername,
            this.ColFullName,
            this.ColRoleName,
            this.ColIsActive});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            // 
            // ColUsername
            // 
            this.ColUsername.Caption = "Username";
            this.ColUsername.FieldName = "Username";
            this.ColUsername.Name = "ColUsername";
            this.ColUsername.Visible = true;
            this.ColUsername.VisibleIndex = 0;
            // 
            // ColFullName
            // 
            this.ColFullName.Caption = "FullName";
            this.ColFullName.FieldName = "FullName";
            this.ColFullName.Name = "ColFullName";
            this.ColFullName.Visible = true;
            this.ColFullName.VisibleIndex = 1;
            // 
            // ColRoleName
            // 
            this.ColRoleName.Caption = "RoleName";
            this.ColRoleName.FieldName = "RoleName";
            this.ColRoleName.Name = "ColRoleName";
            this.ColRoleName.Visible = true;
            this.ColRoleName.VisibleIndex = 2;
            // 
            // ColIsActive
            // 
            this.ColIsActive.Caption = "IsActive";
            this.ColIsActive.FieldName = "IsActive";
            this.ColIsActive.Name = "ColIsActive";
            this.ColIsActive.Visible = true;
            this.ColIsActive.VisibleIndex = 3;
            // 
            // dgv_Roles
            // 
            this.dgv_Roles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Roles.Location = new System.Drawing.Point(2, 22);
            this.dgv_Roles.MainView = this.dgv_RolesList;
            this.dgv_Roles.Name = "dgv_Roles";
            this.dgv_Roles.Size = new System.Drawing.Size(354, 454);
            this.dgv_Roles.TabIndex = 2;
            this.dgv_Roles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgv_RolesList});
            this.dgv_Roles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgv_Roles_MouseUp);
            // 
            // dgv_RolesList
            // 
            this.dgv_RolesList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Col_Role});
            this.dgv_RolesList.GridControl = this.dgv_Roles;
            this.dgv_RolesList.Name = "dgv_RolesList";
            // 
            // Col_Role
            // 
            this.Col_Role.Caption = "Name";
            this.Col_Role.FieldName = "Name";
            this.Col_Role.Name = "Col_Role";
            this.Col_Role.OptionsColumn.AllowEdit = false;
            this.Col_Role.Visible = true;
            this.Col_Role.VisibleIndex = 0;
            // 
            // UcUsers_Roles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcUsers_Roles";
            this.Size = new System.Drawing.Size(711, 502);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Roles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RolesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn ColUsername;
        private DevExpress.XtraGrid.Columns.GridColumn ColFullName;
        private DevExpress.XtraGrid.Columns.GridColumn ColRoleName;
        private DevExpress.XtraGrid.Columns.GridColumn ColIsActive;
        private DevExpress.XtraGrid.GridControl dgv_Roles;
        private DevExpress.XtraGrid.Views.Grid.GridView dgv_RolesList;
        private DevExpress.XtraGrid.Columns.GridColumn Col_Role;
    }
}
