namespace Clothes_Shop_ERP.modlestore
{
    partial class UcBackupSettings
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
            this.LblLastBackup = new DevExpress.XtraEditors.LabelControl();
            this.btnBackupNow = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAs = new DevExpress.XtraEditors.SimpleButton();
            this.TxtFolder = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblFolder = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemBrowse = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemBackupNow = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemLastBackup = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemSaveAs = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblHint = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBackupNow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemLastBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSaveAs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            //
            // layoutControl1
            //
            this.layoutControl1.Controls.Add(this.LblLastBackup);
            this.layoutControl1.Controls.Add(this.btnBackupNow);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnBrowse);
            this.layoutControl1.Controls.Add(this.btnSaveAs);
            this.layoutControl1.Controls.Add(this.TxtFolder);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1007, 300);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            //
            // LblLastBackup
            //
            this.LblLastBackup.Location = new System.Drawing.Point(512, 90);
            this.LblLastBackup.Name = "LblLastBackup";
            this.LblLastBackup.Size = new System.Drawing.Size(91, 13);
            this.LblLastBackup.StyleController = this.layoutControl1;
            this.LblLastBackup.TabIndex = 4;
            this.LblLastBackup.Text = "Last backup: never";
            //
            // btnBackupNow
            //
            this.btnBackupNow.Location = new System.Drawing.Point(262, 78);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(238, 22);
            this.btnBackupNow.StyleController = this.layoutControl1;
            this.btnBackupNow.TabIndex = 3;
            this.btnBackupNow.Text = "Backup Now";
            this.btnBackupNow.Click += new System.EventHandler(this.btnBackupNow_Click);
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(12, 78);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(238, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Folder";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnBrowse
            //
            this.btnBrowse.Location = new System.Drawing.Point(760, 44);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(235, 22);
            this.btnBrowse.StyleController = this.layoutControl1;
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // btnSaveAs
            //
            this.btnSaveAs.Location = new System.Drawing.Point(12, 112);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(300, 22);
            this.btnSaveAs.StyleController = this.layoutControl1;
            this.btnSaveAs.TabIndex = 5;
            this.btnSaveAs.Text = "Save Database As...";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            //
            // TxtFolder
            //
            this.TxtFolder.Location = new System.Drawing.Point(12, 44);
            this.TxtFolder.Name = "TxtFolder";
            this.TxtFolder.Properties.ReadOnly = true;
            this.TxtFolder.Size = new System.Drawing.Size(744, 20);
            this.TxtFolder.StyleController = this.layoutControl1;
            this.TxtFolder.TabIndex = 0;
            //
            // Root
            //
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblFolder,
            this.itemBrowse,
            this.itemSave,
            this.itemBackupNow,
            this.itemLastBackup,
            this.itemSaveAs,
            this.lblHint,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1007, 300);
            this.Root.TextVisible = false;
            //
            // lblFolder
            //
            this.lblFolder.Control = this.TxtFolder;
            this.lblFolder.Location = new System.Drawing.Point(0, 0);
            this.lblFolder.MaxSize = new System.Drawing.Size(756, 34);
            this.lblFolder.MinSize = new System.Drawing.Size(756, 34);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(756, 34);
            this.lblFolder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFolder.Text = "Backup Folder:";
            this.lblFolder.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblFolder.TextSize = new System.Drawing.Size(70, 13);
            //
            // itemBrowse
            //
            this.itemBrowse.Control = this.btnBrowse;
            this.itemBrowse.Location = new System.Drawing.Point(756, 0);
            this.itemBrowse.MaxSize = new System.Drawing.Size(251, 34);
            this.itemBrowse.MinSize = new System.Drawing.Size(251, 34);
            this.itemBrowse.Name = "itemBrowse";
            this.itemBrowse.Size = new System.Drawing.Size(251, 34);
            this.itemBrowse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.itemBrowse.TextSize = new System.Drawing.Size(0, 0);
            this.itemBrowse.TextVisible = false;
            //
            // itemSave
            //
            this.itemSave.Control = this.btnSave;
            this.itemSave.Location = new System.Drawing.Point(0, 34);
            this.itemSave.MaxSize = new System.Drawing.Size(250, 34);
            this.itemSave.MinSize = new System.Drawing.Size(250, 34);
            this.itemSave.Name = "itemSave";
            this.itemSave.Size = new System.Drawing.Size(250, 34);
            this.itemSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.itemSave.TextSize = new System.Drawing.Size(0, 0);
            this.itemSave.TextVisible = false;
            //
            // itemBackupNow
            //
            this.itemBackupNow.Control = this.btnBackupNow;
            this.itemBackupNow.Location = new System.Drawing.Point(250, 34);
            this.itemBackupNow.MaxSize = new System.Drawing.Size(250, 34);
            this.itemBackupNow.MinSize = new System.Drawing.Size(250, 34);
            this.itemBackupNow.Name = "itemBackupNow";
            this.itemBackupNow.Size = new System.Drawing.Size(250, 34);
            this.itemBackupNow.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.itemBackupNow.TextSize = new System.Drawing.Size(0, 0);
            this.itemBackupNow.TextVisible = false;
            //
            // itemLastBackup
            //
            this.itemLastBackup.Control = this.LblLastBackup;
            this.itemLastBackup.Location = new System.Drawing.Point(500, 34);
            this.itemLastBackup.MaxSize = new System.Drawing.Size(507, 34);
            this.itemLastBackup.MinSize = new System.Drawing.Size(507, 34);
            this.itemLastBackup.Name = "itemLastBackup";
            this.itemLastBackup.Size = new System.Drawing.Size(507, 34);
            this.itemLastBackup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.itemLastBackup.TextSize = new System.Drawing.Size(0, 0);
            this.itemLastBackup.TextVisible = false;
            //
            // itemSaveAs
            //
            this.itemSaveAs.Control = this.btnSaveAs;
            this.itemSaveAs.Location = new System.Drawing.Point(0, 68);
            this.itemSaveAs.MaxSize = new System.Drawing.Size(304, 34);
            this.itemSaveAs.MinSize = new System.Drawing.Size(304, 34);
            this.itemSaveAs.Name = "itemSaveAs";
            this.itemSaveAs.Size = new System.Drawing.Size(304, 34);
            this.itemSaveAs.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.itemSaveAs.TextSize = new System.Drawing.Size(0, 0);
            this.itemSaveAs.TextVisible = false;
            //
            // lblHint
            //
            this.lblHint.AllowHotTrack = false;
            this.lblHint.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.AppearanceItemCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.AppearanceItemCaption.Options.UseFont = true;
            this.lblHint.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblHint.Location = new System.Drawing.Point(0, 102);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(1007, 24);
            this.lblHint.Text = "A daily backup runs automatically the first time the app opens each day, once a folder is set here.";
            this.lblHint.TextSize = new System.Drawing.Size(0, 0);
            //
            // emptySpaceItem1
            //
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 126);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1007, 174);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            //
            // UcBackupSettings
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcBackupSettings";
            this.Size = new System.Drawing.Size(1007, 300);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TxtFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBackupNow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemLastBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSaveAs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl LblLastBackup;
        private DevExpress.XtraEditors.SimpleButton btnBackupNow;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.SimpleButton btnSaveAs;
        private DevExpress.XtraEditors.TextEdit TxtFolder;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblFolder;
        private DevExpress.XtraLayout.LayoutControlItem itemBrowse;
        private DevExpress.XtraLayout.LayoutControlItem itemSave;
        private DevExpress.XtraLayout.LayoutControlItem itemBackupNow;
        private DevExpress.XtraLayout.LayoutControlItem itemLastBackup;
        private DevExpress.XtraLayout.LayoutControlItem itemSaveAs;
        private DevExpress.XtraLayout.SimpleLabelItem lblHint;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
