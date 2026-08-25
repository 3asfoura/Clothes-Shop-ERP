namespace Clothes_Shop_ERP.modlestore
{
    partial class UcTreasuryTransactions
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
            this.ColBranch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColTransactionType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(554, 488);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColBranch,
            this.ColTransactionType,
            this.ColAmount,
            this.ColDescription,
            this.ColCreatedAt});
            this.gridView1.DetailHeight = 458;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // ColBranch
            // 
            this.ColBranch.Caption = "Branch";
            this.ColBranch.FieldName = "Branch";
            this.ColBranch.MinWidth = 23;
            this.ColBranch.Name = "ColBranch";
            this.ColBranch.Visible = true;
            this.ColBranch.VisibleIndex = 0;
            this.ColBranch.Width = 87;
            // 
            // ColTransactionType
            // 
            this.ColTransactionType.Caption = "TransactionType";
            this.ColTransactionType.FieldName = "TransactionType";
            this.ColTransactionType.MinWidth = 23;
            this.ColTransactionType.Name = "ColTransactionType";
            this.ColTransactionType.Visible = true;
            this.ColTransactionType.VisibleIndex = 1;
            this.ColTransactionType.Width = 87;
            // 
            // ColAmount
            // 
            this.ColAmount.Caption = "Amount";
            this.ColAmount.FieldName = "Amount";
            this.ColAmount.MinWidth = 23;
            this.ColAmount.Name = "ColAmount";
            this.ColAmount.Visible = true;
            this.ColAmount.VisibleIndex = 2;
            this.ColAmount.Width = 87;
            // 
            // ColDescription
            // 
            this.ColDescription.Caption = "Description";
            this.ColDescription.FieldName = "Description";
            this.ColDescription.MinWidth = 23;
            this.ColDescription.Name = "ColDescription";
            this.ColDescription.Visible = true;
            this.ColDescription.VisibleIndex = 3;
            this.ColDescription.Width = 87;
            // 
            // ColCreatedAt
            // 
            this.ColCreatedAt.Caption = "CreatedAt";
            this.ColCreatedAt.FieldName = "CreatedAt";
            this.ColCreatedAt.MinWidth = 23;
            this.ColCreatedAt.Name = "ColCreatedAt";
            this.ColCreatedAt.Visible = true;
            this.ColCreatedAt.VisibleIndex = 4;
            this.ColCreatedAt.Width = 87;
            // 
            // UcTreasuryTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UcTreasuryTransactions";
            this.Size = new System.Drawing.Size(554, 488);
            this.Load += new System.EventHandler(this.UcTreasuryTransactions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ColBranch;
        private DevExpress.XtraGrid.Columns.GridColumn ColTransactionType;
        private DevExpress.XtraGrid.Columns.GridColumn ColAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ColDescription;
        private DevExpress.XtraGrid.Columns.GridColumn ColCreatedAt;
    }
}
