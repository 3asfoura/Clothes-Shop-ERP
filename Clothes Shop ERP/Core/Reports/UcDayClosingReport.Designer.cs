namespace Clothes_Shop_ERP.modlestore
{
    partial class UcDayClosingReport
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
            this.layoutControlGrid = new DevExpress.XtraLayout.LayoutControl();
            this.GridResult = new DevExpress.XtraGrid.GridControl();
            this.GridViewResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroupGrid = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.pnlSummaryHost = new System.Windows.Forms.Panel();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.DtDate = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemToolbar = new DevExpress.XtraLayout.EmptySpaceItem();
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.btnRun1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrid)).BeginInit();
            this.layoutControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGrid
            // 
            this.layoutControlGrid.Controls.Add(this.GridResult);
            this.layoutControlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlGrid.Location = new System.Drawing.Point(0, 387);
            this.layoutControlGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControlGrid.Name = "layoutControlGrid";
            this.layoutControlGrid.Root = this.layoutControlGroupGrid;
            this.layoutControlGrid.Size = new System.Drawing.Size(1175, 249);
            this.layoutControlGrid.TabIndex = 2;
            this.layoutControlGrid.Text = "layoutControlGrid";
            // 
            // GridResult
            // 
            this.GridResult.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.GridResult.Location = new System.Drawing.Point(14, 16);
            this.GridResult.MainView = this.GridViewResult;
            this.GridResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GridResult.Name = "GridResult";
            this.GridResult.Size = new System.Drawing.Size(1147, 217);
            this.GridResult.TabIndex = 0;
            this.GridResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewResult});
            // 
            // GridViewResult
            // 
            this.GridViewResult.DetailHeight = 458;
            this.GridViewResult.GridControl = this.GridResult;
            this.GridViewResult.Name = "GridViewResult";
            this.GridViewResult.OptionsBehavior.Editable = false;
            this.GridViewResult.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroupGrid
            // 
            this.layoutControlGroupGrid.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupGrid.GroupBordersVisible = false;
            this.layoutControlGroupGrid.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrid});
            this.layoutControlGroupGrid.Name = "layoutControlGroupGrid";
            this.layoutControlGroupGrid.Size = new System.Drawing.Size(1175, 249);
            this.layoutControlGroupGrid.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.GridResult;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(1151, 223);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // pnlSummaryHost
            // 
            this.pnlSummaryHost.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummaryHost.Location = new System.Drawing.Point(0, 112);
            this.pnlSummaryHost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSummaryHost.Name = "pnlSummaryHost";
            this.pnlSummaryHost.Size = new System.Drawing.Size(1175, 275);
            this.pnlSummaryHost.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.DtDate);
            this.layoutControl1.Controls.Add(this.btnRun);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1175, 112);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // DtDate
            // 
            this.DtDate.EditValue = null;
            this.DtDate.Location = new System.Drawing.Point(14, 64);
            this.DtDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DtDate.Name = "DtDate";
            this.DtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtDate.Size = new System.Drawing.Size(264, 26);
            this.DtDate.StyleController = this.layoutControl1;
            this.DtDate.TabIndex = 0;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblDate,
            this.emptySpaceItemToolbar,
            this.btnRun1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1175, 112);
            this.Root.TextVisible = false;
            // 
            // lblDate
            // 
            this.lblDate.Control = this.DtDate;
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.MaxSize = new System.Drawing.Size(268, 86);
            this.lblDate.MinSize = new System.Drawing.Size(268, 86);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(268, 86);
            this.lblDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 26, 0);
            this.lblDate.Text = "Date:";
            this.lblDate.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblDate.TextSize = new System.Drawing.Size(30, 17);
            // 
            // emptySpaceItemToolbar
            // 
            this.emptySpaceItemToolbar.AllowHotTrack = false;
            this.emptySpaceItemToolbar.Location = new System.Drawing.Point(456, 0);
            this.emptySpaceItemToolbar.Name = "emptySpaceItemToolbar";
            this.emptySpaceItemToolbar.Size = new System.Drawing.Size(695, 86);
            this.emptySpaceItemToolbar.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(282, 64);
            this.btnRun.Name = "btnRun";
            this.btnRun.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnRun.Size = new System.Drawing.Size(184, 24);
            this.btnRun.StyleController = this.layoutControl1;
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Generate Report";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click_1);
            // 
            // btnRun1
            // 
            this.btnRun1.Control = this.btnRun;
            this.btnRun1.Location = new System.Drawing.Point(268, 0);
            this.btnRun1.Name = "btnRun1";
            this.btnRun1.Size = new System.Drawing.Size(188, 86);
            this.btnRun1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 48, 0);
            this.btnRun1.Text = "Generate Report";
            this.btnRun1.TextSize = new System.Drawing.Size(0, 0);
            this.btnRun1.TextVisible = false;
            // 
            // UcDayClosingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlGrid);
            this.Controls.Add(this.pnlSummaryHost);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UcDayClosingReport";
            this.Size = new System.Drawing.Size(1175, 636);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrid)).EndInit();
            this.layoutControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRun1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlGrid;
        private DevExpress.XtraGrid.GridControl GridResult;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewResult;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private System.Windows.Forms.Panel pnlSummaryHost;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit DtDate;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemToolbar;
        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.XtraLayout.LayoutControlItem btnRun1;
    }
}
