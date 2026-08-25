namespace Clothes_Shop_ERP
{
    partial class UcPointOfSale
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.TxtBarcode = new DevExpress.XtraEditors.TextEdit();
            this.CmbVariant = new DevExpress.XtraEditors.ComboBoxEdit();
            this.SpinManualQty = new DevExpress.XtraEditors.SpinEdit();
            this.btnAddManual = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.SpinManualQty2222 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRemoveLine = new DevExpress.XtraEditors.SimpleButton();
            this.CmbCustomer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.CmbPaymentMethod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.SimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.SpinDiscount = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblPayment = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDiscount = new DevExpress.XtraLayout.LayoutControlItem();
            this.LblTotal = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.GridCart = new DevExpress.XtraGrid.GridControl();
            this.GridViewCart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Loop = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbVariant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinManualQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinManualQty2222)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbPaymentMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.TxtBarcode);
            this.layoutControl1.Controls.Add(this.CmbVariant);
            this.layoutControl1.Controls.Add(this.SpinManualQty);
            this.layoutControl1.Controls.Add(this.btnAddManual);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1133, 0, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1068, 86);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // TxtBarcode
            // 
            this.TxtBarcode.Location = new System.Drawing.Point(12, 44);
            this.TxtBarcode.Name = "TxtBarcode";
            this.TxtBarcode.Size = new System.Drawing.Size(299, 22);
            this.TxtBarcode.StyleController = this.layoutControl1;
            this.TxtBarcode.TabIndex = 4;
            this.TxtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBarcode_KeyDown);
            // 
            // CmbVariant
            // 
            this.CmbVariant.Location = new System.Drawing.Point(315, 44);
            this.CmbVariant.Name = "CmbVariant";
            this.CmbVariant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbVariant.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CmbVariant.Size = new System.Drawing.Size(481, 22);
            this.CmbVariant.StyleController = this.layoutControl1;
            this.CmbVariant.TabIndex = 5;
            // 
            // SpinManualQty
            // 
            this.SpinManualQty.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SpinManualQty.Location = new System.Drawing.Point(800, 44);
            this.SpinManualQty.Name = "SpinManualQty";
            this.SpinManualQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinManualQty.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.SpinManualQty.Properties.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SpinManualQty.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SpinManualQty.Size = new System.Drawing.Size(151, 22);
            this.SpinManualQty.StyleController = this.layoutControl1;
            this.SpinManualQty.TabIndex = 5;
            // 
            // btnAddManual
            // 
            this.btnAddManual.Location = new System.Drawing.Point(955, 44);
            this.btnAddManual.Name = "btnAddManual";
            this.btnAddManual.Size = new System.Drawing.Size(101, 22);
            this.btnAddManual.StyleController = this.layoutControl1;
            this.btnAddManual.TabIndex = 6;
            this.btnAddManual.Text = "Add";
            this.btnAddManual.Click += new System.EventHandler(this.btnAddManual_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.SpinManualQty2222,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1068, 86);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.CmbVariant;
            this.layoutControlItem2.Location = new System.Drawing.Point(303, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(485, 66);
            this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 15, 0);
            this.layoutControlItem2.Text = "Or pick manually:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(82, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.TxtBarcode;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(303, 66);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 15, 0);
            this.layoutControlItem1.Text = "Scan barcode :";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(82, 13);
            // 
            // SpinManualQty2222
            // 
            this.SpinManualQty2222.Control = this.SpinManualQty;
            this.SpinManualQty2222.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.SpinManualQty2222.CustomizationFormText = "layoutControlItem2";
            this.SpinManualQty2222.Location = new System.Drawing.Point(788, 0);
            this.SpinManualQty2222.Name = "SpinManualQty2222";
            this.SpinManualQty2222.Size = new System.Drawing.Size(155, 66);
            this.SpinManualQty2222.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 32, 0);
            this.SpinManualQty2222.Text = "layoutControlItem2";
            this.SpinManualQty2222.TextLocation = DevExpress.Utils.Locations.Top;
            this.SpinManualQty2222.TextSize = new System.Drawing.Size(0, 0);
            this.SpinManualQty2222.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnAddManual;
            this.layoutControlItem3.Location = new System.Drawing.Point(943, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(105, 66);
            this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 32, 0);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.btnRemoveLine);
            this.layoutControl2.Controls.Add(this.CmbCustomer);
            this.layoutControl2.Controls.Add(this.CmbPaymentMethod);
            this.layoutControl2.Controls.Add(this.SimpleButton);
            this.layoutControl2.Controls.Add(this.SpinDiscount);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControl2.Location = new System.Drawing.Point(0, 514);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1068, 153);
            this.layoutControl2.TabIndex = 1;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // btnRemoveLine
            // 
            this.btnRemoveLine.Location = new System.Drawing.Point(12, 17);
            this.btnRemoveLine.Name = "btnRemoveLine";
            this.btnRemoveLine.Size = new System.Drawing.Size(173, 22);
            this.btnRemoveLine.StyleController = this.layoutControl2;
            this.btnRemoveLine.TabIndex = 6;
            this.btnRemoveLine.Text = "Remove Selected Item";
            this.btnRemoveLine.Click += new System.EventHandler(this.btnRemoveLine_Click);
            // 
            // CmbCustomer
            // 
            this.CmbCustomer.Location = new System.Drawing.Point(12, 60);
            this.CmbCustomer.Name = "CmbCustomer";
            this.CmbCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CmbCustomer.Size = new System.Drawing.Size(285, 22);
            this.CmbCustomer.StyleController = this.layoutControl2;
            this.CmbCustomer.TabIndex = 7;
            // 
            // CmbPaymentMethod
            // 
            this.CmbPaymentMethod.Location = new System.Drawing.Point(301, 60);
            this.CmbPaymentMethod.Name = "CmbPaymentMethod";
            this.CmbPaymentMethod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbPaymentMethod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CmbPaymentMethod.Size = new System.Drawing.Size(556, 22);
            this.CmbPaymentMethod.StyleController = this.layoutControl2;
            this.CmbPaymentMethod.TabIndex = 7;
            // 
            // SimpleButton
            // 
            this.SimpleButton.Location = new System.Drawing.Point(781, 91);
            this.SimpleButton.Name = "SimpleButton";
            this.SimpleButton.Padding = new System.Windows.Forms.Padding(8);
            this.SimpleButton.Size = new System.Drawing.Size(275, 38);
            this.SimpleButton.StyleController = this.layoutControl2;
            this.SimpleButton.TabIndex = 6;
            this.SimpleButton.Text = "Checkout";
            this.SimpleButton.Click += new System.EventHandler(this.SimpleButton_Click);
            // 
            // SpinDiscount
            // 
            this.SpinDiscount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpinDiscount.Location = new System.Drawing.Point(861, 60);
            this.SpinDiscount.Name = "SpinDiscount";
            this.SpinDiscount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinDiscount.Properties.DisplayFormat.FormatString = "n2";
            this.SpinDiscount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.SpinDiscount.Size = new System.Drawing.Size(195, 22);
            this.SpinDiscount.StyleController = this.layoutControl2;
            this.SpinDiscount.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.lblCustomer,
            this.lblPayment,
            this.lblDiscount,
            this.LblTotal,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1068, 153);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnRemoveLine;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(177, 31);
            this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 0);
            this.layoutControlItem4.Text = "layoutControlItem3";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(177, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(871, 31);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Control = this.CmbCustomer;
            this.lblCustomer.Location = new System.Drawing.Point(0, 31);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(289, 43);
            this.lblCustomer.Text = "Customer:";
            this.lblCustomer.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblCustomer.TextSize = new System.Drawing.Size(93, 13);
            // 
            // lblPayment
            // 
            this.lblPayment.Control = this.CmbPaymentMethod;
            this.lblPayment.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lblPayment.CustomizationFormText = "Payment Method:";
            this.lblPayment.Location = new System.Drawing.Point(289, 31);
            this.lblPayment.Name = "lblPayment";
            this.lblPayment.Size = new System.Drawing.Size(560, 43);
            this.lblPayment.Text = "Payment Method:";
            this.lblPayment.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblPayment.TextSize = new System.Drawing.Size(93, 13);
            // 
            // lblDiscount
            // 
            this.lblDiscount.Control = this.SpinDiscount;
            this.lblDiscount.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lblDiscount.CustomizationFormText = "Discount:";
            this.lblDiscount.Location = new System.Drawing.Point(849, 31);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(199, 43);
            this.lblDiscount.Text = "Discount:";
            this.lblDiscount.TextLocation = DevExpress.Utils.Locations.Top;
            this.lblDiscount.TextSize = new System.Drawing.Size(93, 13);
            // 
            // LblTotal
            // 
            this.LblTotal.AllowHotTrack = false;
            this.LblTotal.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.AppearanceItemCaption.Options.UseFont = true;
            this.LblTotal.CustomizationFormText = "Total: 0.00";
            this.LblTotal.Location = new System.Drawing.Point(0, 74);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.LblTotal.Size = new System.Drawing.Size(769, 59);
            this.LblTotal.Text = "Total: 0.00";
            this.LblTotal.TextSize = new System.Drawing.Size(93, 25);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.SimpleButton;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem5.Location = new System.Drawing.Point(769, 74);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(279, 59);
            this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 0);
            this.layoutControlItem5.Text = "layoutControlItem3";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.GridCart);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 86);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(1068, 428);
            this.layoutControl3.TabIndex = 2;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // GridCart
            // 
            this.GridCart.Location = new System.Drawing.Point(12, 12);
            this.GridCart.MainView = this.GridViewCart;
            this.GridCart.Name = "GridCart";
            this.GridCart.Size = new System.Drawing.Size(1044, 404);
            this.GridCart.TabIndex = 4;
            this.GridCart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewCart});
            // 
            // GridViewCart
            // 
            this.GridViewCart.GridControl = this.GridCart;
            this.GridViewCart.Name = "GridViewCart";
            this.GridViewCart.OptionsBehavior.Editable = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1068, 428);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.GridCart;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1048, 408);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // Loop
            // 
            this.Loop.Tick += new System.EventHandler(this.Loop_Tick);
            // 
            // UcPointOfSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl3);
            this.Controls.Add(this.layoutControl2);
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcPointOfSale";
            this.Size = new System.Drawing.Size(1068, 667);
            this.Load += new System.EventHandler(this.UcPointOfSale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TxtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbVariant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinManualQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinManualQty2222)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CmbCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbPaymentMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit TxtBarcode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem SpinManualQty2222;
        private DevExpress.XtraEditors.ComboBoxEdit CmbVariant;
        private DevExpress.XtraEditors.SpinEdit SpinManualQty;
        private DevExpress.XtraEditors.SimpleButton btnAddManual;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnRemoveLine;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblCustomer;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraGrid.GridControl GridCart;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewCart;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.ComboBoxEdit CmbCustomer;
        private DevExpress.XtraEditors.ComboBoxEdit CmbPaymentMethod;
        private DevExpress.XtraLayout.LayoutControlItem lblPayment;
        private DevExpress.XtraLayout.LayoutControlItem lblDiscount;
        private DevExpress.XtraEditors.SimpleButton SimpleButton;
        private DevExpress.XtraLayout.SimpleLabelItem LblTotal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SpinEdit SpinDiscount;
        private System.Windows.Forms.Timer Loop;
    }
}
