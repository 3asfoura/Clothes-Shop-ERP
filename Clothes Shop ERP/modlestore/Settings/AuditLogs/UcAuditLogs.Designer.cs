namespace Clothes_Shop_ERP.modlestore
{
    partial class UcAuditLogs
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
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.GridResult = new DevExpress.XtraGrid.GridControl();
            this.GridViewResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.CmbTable = new DevExpress.XtraEditors.ComboBoxEdit();
            this.DtFrom = new DevExpress.XtraEditors.DateEdit();
            this.DtTo = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTable = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            //
            // layoutControl2
            //
            this.layoutControl2.Controls.Add(this.GridResult);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 86);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(1007, 460);
            this.layoutControl2.TabIndex = 1;
            this.layoutControl2.Text = "layoutControl2";
            //
            // GridResult
            //
            this.GridResult.Location = new System.Drawing.Point(12, 12);
            this.GridResult.MainView = this.GridViewResult;
            this.GridResult.Name = "GridResult";
            this.GridResult.Size = new System.Drawing.Size(983, 436);
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
            this.GridViewResult.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridViewResult_RowCellStyle);
            //
            // layoutControlGroup2
            //
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1007, 460);
            this.layoutControlGroup2.TextVisible = false;
            //
            // layoutControlItem5
            //
            this.layoutControlItem5.Control = this.GridResult;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(987, 440);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            //
            // layoutControl1
            //
            this.layoutControl1.Controls.Add(this.btnRun);
            this.layoutControl1.Controls.Add(this.CmbTable);
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
            this.btnRun.Location = new System.Drawing.Point(752, 44);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(243, 22);
            this.btnRun.StyleController = this.layoutControl1;
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Refresh";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            //
            // CmbTable
            //
            this.CmbTable.Location = new System.Drawing.Point(465, 44);
            this.CmbTable.Name = "CmbTable";
            this.CmbTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbTable.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CmbTable.Size = new System.Drawing.Size(273, 20);
            this.CmbTable.StyleController = this.layoutControl1;
            this.CmbTable.TabIndex = 2;
            //
            // DtFrom
            //
            this.DtFrom.EditValue = null;
            this.DtFrom.Location = new System.Drawing.Point(12, 44);
            this.DtFrom.Name = "DtFrom";
            this.DtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtFrom.Size = new System.Drawing.Size(206, 20);
            this.DtFrom.StyleController = this.layoutControl1;
            this.DtFrom.TabIndex = 0;
            //
            // DtTo
            //
            this.DtTo.EditValue = null;
            this.DtTo.Location = new System.Drawing.Point(242, 44);
            this.DtTo.Name = "DtTo";
            this.DtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtTo.Size = new System.Drawing.Size(199, 20);
            this.DtTo.StyleController = this.layoutControl1;
            this.DtTo.TabIndex = 1;
            //
            // Root
            //
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblFrom,
            this.lblTo,
            this.lblTable,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1007, 86);
            this.Root.TextVisible = false;
            //
            // lblFrom
            //
            this.lblFrom.Control = this.DtFrom;
            this.lblFrom.Location = new System.Drawing.Point(0, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(230, 66);
            this.lblFrom.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 15, 0);
            this.lblFrom.Text = "From:";
            this.lblFrom.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblFrom.TextSize = new System.Drawing.Size(28, 13);
            //
            // lblTo
            //
            this.lblTo.Control = this.DtTo;
            this.lblTo.Location = new System.Drawing.Point(230, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(223, 66);
            this.lblTo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 15, 0);
            this.lblTo.Text = "To:";
            this.lblTo.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblTo.TextSize = new System.Drawing.Size(28, 13);
            //
            // lblTable
            //
            this.lblTable.Control = this.CmbTable;
            this.lblTable.Location = new System.Drawing.Point(453, 0);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(297, 66);
            this.lblTable.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 15, 0);
            this.lblTable.Text = "Table:";
            this.lblTable.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblTable.TextSize = new System.Drawing.Size(35, 13);
            //
            // layoutControlItem1
            //
            this.layoutControlItem1.Control = this.btnRun;
            this.layoutControlItem1.Location = new System.Drawing.Point(750, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(257, 66);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            //
            // UcAuditLogs
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl2);
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcAuditLogs";
            this.Size = new System.Drawing.Size(1007, 546);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CmbTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraGrid.GridControl GridResult;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewResult;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.XtraEditors.ComboBoxEdit CmbTable;
        private DevExpress.XtraEditors.DateEdit DtFrom;
        private DevExpress.XtraEditors.DateEdit DtTo;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblTo;
        private DevExpress.XtraLayout.LayoutControlItem lblTable;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
