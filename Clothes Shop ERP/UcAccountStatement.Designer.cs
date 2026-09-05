namespace Clothes_Shop_ERP.modlestore
{
    partial class UcAccountStatement
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
            this.CmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CmbParty = new DevExpress.XtraEditors.ComboBoxEdit();
            this.DtFrom = new DevExpress.XtraEditors.DateEdit();
            this.DtTo = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblParty = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTo = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.CmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbParty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRun)).BeginInit();
            this.SuspendLayout();
            //
            // layoutControlGrid
            //
            this.layoutControlGrid.Controls.Add(this.GridResult);
            this.layoutControlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlGrid.Location = new System.Drawing.Point(0, 86);
            this.layoutControlGrid.Name = "layoutControlGrid";
            this.layoutControlGrid.Root = this.layoutControlGroupGrid;
            this.layoutControlGrid.Size = new System.Drawing.Size(1007, 400);
            this.layoutControlGrid.TabIndex = 1;
            this.layoutControlGrid.Text = "layoutControlGrid";
            //
            // GridResult
            //
            this.GridResult.Location = new System.Drawing.Point(12, 12);
            this.GridResult.MainView = this.GridViewResult;
            this.GridResult.Name = "GridResult";
            this.GridResult.Size = new System.Drawing.Size(983, 376);
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
            this.layoutControlGroupGrid.Size = new System.Drawing.Size(1007, 400);
            this.layoutControlGroupGrid.TextVisible = false;
            //
            // layoutControlItemGrid
            //
            this.layoutControlItemGrid.Control = this.GridResult;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(987, 380);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            //
            // layoutControlSummary
            //
            this.layoutControlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControlSummary.Location = new System.Drawing.Point(0, 486);
            this.layoutControlSummary.Name = "layoutControlSummary";
            this.layoutControlSummary.Root = this.layoutControlGroupSummary;
            this.layoutControlSummary.Size = new System.Drawing.Size(1007, 60);
            this.layoutControlSummary.TabIndex = 2;
            this.layoutControlSummary.Text = "layoutControlSummary";
            //
            // layoutControlGroupSummary
            //
            this.layoutControlGroupSummary.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupSummary.GroupBordersVisible = false;
            this.layoutControlGroupSummary.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.LblSummary});
            this.layoutControlGroupSummary.Name = "layoutControlGroupSummary";
            this.layoutControlGroupSummary.Size = new System.Drawing.Size(1007, 60);
            this.layoutControlGroupSummary.TextVisible = false;
            //
            // LblSummary
            //
            this.LblSummary.AllowHotTrack = false;
            this.LblSummary.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSummary.AppearanceItemCaption.Options.UseFont = true;
            this.LblSummary.Location = new System.Drawing.Point(0, 0);
            this.LblSummary.Name = "LblSummary";
            this.LblSummary.Size = new System.Drawing.Size(987, 40);
            this.LblSummary.Text = "Total Invoiced: 0.00  |  Total Paid: 0.00  |  Total Due: 0.00";
            this.LblSummary.TextSize = new System.Drawing.Size(0, 0);
            //
            // layoutControl1
            //
            this.layoutControl1.Controls.Add(this.btnRun);
            this.layoutControl1.Controls.Add(this.CmbType);
            this.layoutControl1.Controls.Add(this.CmbParty);
            this.layoutControl1.Controls.Add(this.DtFrom);
            this.layoutControl1.Controls.Add(this.DtTo);
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
            this.btnRun.Location = new System.Drawing.Point(882, 44);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(113, 22);
            this.btnRun.StyleController = this.layoutControl1;
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Generate Report";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            //
            // CmbType
            //
            this.CmbType.Location = new System.Drawing.Point(12, 44);
            this.CmbType.Name = "CmbType";
            this.CmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CmbType.Size = new System.Drawing.Size(146, 20);
            this.CmbType.StyleController = this.layoutControl1;
            this.CmbType.TabIndex = 0;
            this.CmbType.SelectedIndexChanged += new System.EventHandler(this.CmbType_SelectedIndexChanged);
            //
            // CmbParty
            //
            this.CmbParty.Location = new System.Drawing.Point(174, 44);
            this.CmbParty.Name = "CmbParty";
            this.CmbParty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbParty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CmbParty.Size = new System.Drawing.Size(291, 20);
            this.CmbParty.StyleController = this.layoutControl1;
            this.CmbParty.TabIndex = 1;
            //
            // DtFrom
            //
            this.DtFrom.EditValue = null;
            this.DtFrom.Location = new System.Drawing.Point(485, 44);
            this.DtFrom.Name = "DtFrom";
            this.DtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtFrom.Size = new System.Drawing.Size(186, 20);
            this.DtFrom.StyleController = this.layoutControl1;
            this.DtFrom.TabIndex = 2;
            //
            // DtTo
            //
            this.DtTo.EditValue = null;
            this.DtTo.Location = new System.Drawing.Point(683, 44);
            this.DtTo.Name = "DtTo";
            this.DtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtTo.Size = new System.Drawing.Size(187, 20);
            this.DtTo.StyleController = this.layoutControl1;
            this.DtTo.TabIndex = 3;
            //
            // Root
            //
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblType,
            this.lblParty,
            this.lblFrom,
            this.lblTo,
            this.layoutControlItemRun});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1007, 86);
            this.Root.TextVisible = false;
            //
            // lblType
            //
            this.lblType.Control = this.CmbType;
            this.lblType.Location = new System.Drawing.Point(0, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(150, 66);
            this.lblType.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 12, 0);
            this.lblType.Text = "Type:";
            this.lblType.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblType.TextSize = new System.Drawing.Size(28, 13);
            //
            // lblParty
            //
            this.lblParty.Control = this.CmbParty;
            this.lblParty.Location = new System.Drawing.Point(150, 0);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(295, 66);
            this.lblParty.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 12, 0);
            this.lblParty.Text = "Customer:";
            this.lblParty.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblParty.TextSize = new System.Drawing.Size(28, 13);
            //
            // lblFrom
            //
            this.lblFrom.Control = this.DtFrom;
            this.lblFrom.Location = new System.Drawing.Point(445, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(190, 66);
            this.lblFrom.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 12, 0);
            this.lblFrom.Text = "From:";
            this.lblFrom.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblFrom.TextSize = new System.Drawing.Size(28, 13);
            //
            // lblTo
            //
            this.lblTo.Control = this.DtTo;
            this.lblTo.Location = new System.Drawing.Point(635, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(191, 66);
            this.lblTo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 12, 0);
            this.lblTo.Text = "To:";
            this.lblTo.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblTo.TextSize = new System.Drawing.Size(28, 13);
            //
            // layoutControlItemRun
            //
            this.layoutControlItemRun.Control = this.btnRun;
            this.layoutControlItemRun.Location = new System.Drawing.Point(826, 0);
            this.layoutControlItemRun.Name = "layoutControlItemRun";
            this.layoutControlItemRun.Size = new System.Drawing.Size(181, 66);
            this.layoutControlItemRun.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRun.TextVisible = false;
            //
            // UcAccountStatement
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlGrid);
            this.Controls.Add(this.layoutControlSummary);
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcAccountStatement";
            this.Size = new System.Drawing.Size(1007, 546);
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
            ((System.ComponentModel.ISupportInitialize)(this.CmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbParty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).EndInit();
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
        private DevExpress.XtraEditors.ComboBoxEdit CmbType;
        private DevExpress.XtraEditors.ComboBoxEdit CmbParty;
        private DevExpress.XtraEditors.DateEdit DtFrom;
        private DevExpress.XtraEditors.DateEdit DtTo;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblType;
        private DevExpress.XtraLayout.LayoutControlItem lblParty;
        private DevExpress.XtraLayout.LayoutControlItem lblFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRun;
    }
}
