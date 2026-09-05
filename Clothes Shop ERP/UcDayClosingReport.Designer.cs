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
            this.layoutControlSummary = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroupSummary = new DevExpress.XtraLayout.LayoutControlGroup();
            this.LblSummary = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.DtDate = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRun = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrid)).BeginInit();
            this.layoutControlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LblSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRun)).BeginInit();
            this.SuspendLayout();
            //
            // layoutControlGrid
            //
            this.layoutControlGrid.Controls.Add(this.GridResult);
            this.layoutControlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlGrid.Location = new System.Drawing.Point(0, 226);
            this.layoutControlGrid.Name = "layoutControlGrid";
            this.layoutControlGrid.Root = this.layoutControlGroupGrid;
            this.layoutControlGrid.Size = new System.Drawing.Size(1007, 260);
            this.layoutControlGrid.TabIndex = 2;
            this.layoutControlGrid.Text = "layoutControlGrid";
            //
            // GridResult
            //
            this.GridResult.Location = new System.Drawing.Point(12, 12);
            this.GridResult.MainView = this.GridViewResult;
            this.GridResult.Name = "GridResult";
            this.GridResult.Size = new System.Drawing.Size(983, 236);
            this.GridResult.TabIndex = 0;
            this.GridResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewResult});
            //
            // GridViewResult
            //
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
            this.layoutControlGroupGrid.Size = new System.Drawing.Size(1007, 260);
            this.layoutControlGroupGrid.TextVisible = false;
            //
            // layoutControlItemGrid
            //
            this.layoutControlItemGrid.Control = this.GridResult;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(987, 240);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            //
            // layoutControlSummary
            //
            this.layoutControlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControlSummary.Location = new System.Drawing.Point(0, 86);
            this.layoutControlSummary.Name = "layoutControlSummary";
            this.layoutControlSummary.Root = this.layoutControlGroupSummary;
            this.layoutControlSummary.Size = new System.Drawing.Size(1007, 140);
            this.layoutControlSummary.TabIndex = 1;
            this.layoutControlSummary.Text = "layoutControlSummary";
            //
            // layoutControlGroupSummary
            //
            this.layoutControlGroupSummary.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupSummary.GroupBordersVisible = false;
            this.layoutControlGroupSummary.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.LblSummary});
            this.layoutControlGroupSummary.Name = "layoutControlGroupSummary";
            this.layoutControlGroupSummary.Size = new System.Drawing.Size(1007, 140);
            this.layoutControlGroupSummary.TextVisible = false;
            //
            // LblSummary
            //
            this.LblSummary.AllowHotTrack = false;
            this.LblSummary.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSummary.AppearanceItemCaption.Options.UseFont = true;
            this.LblSummary.Location = new System.Drawing.Point(0, 0);
            this.LblSummary.Name = "LblSummary";
            this.LblSummary.Size = new System.Drawing.Size(987, 120);
            this.LblSummary.Text = "Invoices: 0   |   Total Sales: 0.00";
            this.LblSummary.TextSize = new System.Drawing.Size(0, 0);
            //
            // layoutControl1
            //
            this.layoutControl1.Controls.Add(this.btnRun);
            this.layoutControl1.Controls.Add(this.DtDate);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1007, 86);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            //
            // btnRun
            //
            this.btnRun.Location = new System.Drawing.Point(262, 44);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(200, 22);
            this.btnRun.StyleController = this.layoutControl1;
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Generate Report";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            //
            // DtDate
            //
            this.DtDate.EditValue = null;
            this.DtDate.Location = new System.Drawing.Point(12, 44);
            this.DtDate.Name = "DtDate";
            this.DtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtDate.Size = new System.Drawing.Size(226, 20);
            this.DtDate.StyleController = this.layoutControl1;
            this.DtDate.TabIndex = 0;
            //
            // Root
            //
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblDate,
            this.layoutControlItemRun});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1007, 86);
            this.Root.TextVisible = false;
            //
            // lblDate
            //
            this.lblDate.Control = this.DtDate;
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(230, 66);
            this.lblDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 20, 0);
            this.lblDate.Text = "Date:";
            this.lblDate.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblDate.TextSize = new System.Drawing.Size(35, 13);
            //
            // layoutControlItemRun
            //
            this.layoutControlItemRun.Control = this.btnRun;
            this.layoutControlItemRun.Location = new System.Drawing.Point(230, 0);
            this.layoutControlItemRun.Name = "layoutControlItemRun";
            this.layoutControlItemRun.Size = new System.Drawing.Size(220, 66);
            this.layoutControlItemRun.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRun.TextVisible = false;
            //
            // UcDayClosingReport
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlGrid);
            this.Controls.Add(this.layoutControlSummary);
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcDayClosingReport";
            this.Size = new System.Drawing.Size(1007, 486);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrid)).EndInit();
            this.layoutControlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LblSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRun)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlGrid;
        private DevExpress.XtraGrid.GridControl GridResult;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewResult;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControl layoutControlSummary;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupSummary;
        private DevExpress.XtraLayout.SimpleLabelItem LblSummary;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.XtraEditors.DateEdit DtDate;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRun;
    }
}
