using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public class RolePermissionRow
    {
        public string ScreenName { get; set; }
        public string DisplayName { get; set; }
        public string PermissionLevel { get; set; }
    }

    public partial class FrmRoleEdit : DevExpress.XtraEditors.XtraForm
    {
        public string RoleName => TxtName.Text.Trim();
        public List<RolePermissionRow> ScreenPermissions => _rows;

        private TextEdit TxtName;
        private GridControl _grid;
        private GridView _gridView;
        private List<RolePermissionRow> _rows;

        public FrmRoleEdit()
        {
            InitializeComponent();
        }

        // existingLevels: ScreenName -> PermissionLevel for the role being edited (empty for a new role).
        // isProtected: true for the role that always has full access (see PermissionManager.Load) -
        // its permission grid is shown as all-Write and locked, since editing it wouldn't change anything.
        public FrmRoleEdit(string title, string name, IDictionary<string, string> existingLevels, bool isProtected = false)
        {
            this.Text = title;
            this.Width = 460;
            this.Height = 560;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblName = new LabelControl { Text = LocalizationManager.T("Shared_ColName"), Location = new Point(20, 20) };
            TxtName = new TextEdit { Text = name, Location = new Point(20, 40), Width = 400 };

            var lblPermissions = new LabelControl
            {
                Text = isProtected ? LocalizationManager.T("Roles_ProtectedAdminNote") : LocalizationManager.T("Roles_PermissionsGroupTitle"),
                Location = new Point(20, 75),
                Width = 400
            };

            _rows = PermissionManager.AllScreens.Select(kv => new RolePermissionRow
            {
                ScreenName = kv.Key,
                DisplayName = LocalizationManager.T(kv.Value),
                PermissionLevel = isProtected ? PermissionManager.LevelWrite
                    : (existingLevels.TryGetValue(kv.Key, out var level) ? level : PermissionManager.LevelNone)
            }).ToList();

            _grid = new GridControl { Location = new Point(20, 95), Size = new Size(400, 400) };
            _gridView = new GridView(_grid);
            _grid.MainView = _gridView;

            // The grid must already be parented (have a BindingContext) before
            // setting DataSource/PopulateColumns, otherwise no columns get
            // generated and Columns[...] below comes back null.
            this.Controls.Add(_grid);

            _grid.DataSource = _rows;

            var repoCombo = new RepositoryItemImageComboBox();
            repoCombo.Items.AddRange(new[]
            {
                new ImageComboBoxItem(LocalizationManager.T("Permission_None"), PermissionManager.LevelNone, -1),
                new ImageComboBoxItem(LocalizationManager.T("Permission_Read"), PermissionManager.LevelRead, -1),
                new ImageComboBoxItem(LocalizationManager.T("Permission_Write"), PermissionManager.LevelWrite, -1),
            });
            _grid.RepositoryItems.Add(repoCombo);

            _gridView.OptionsView.ShowGroupPanel = false;
            _gridView.OptionsCustomization.AllowSort = false;
            _gridView.PopulateColumns();
            Sett.CenterColumns(_gridView);
            _gridView.Columns["ScreenName"].Visible = false;
            _gridView.Columns["DisplayName"].Caption = LocalizationManager.T("Roles_ColScreen");
            _gridView.Columns["DisplayName"].OptionsColumn.AllowEdit = false;
            _gridView.Columns["DisplayName"].Width = 220;
            _gridView.Columns["PermissionLevel"].Caption = LocalizationManager.T("Roles_ColPermission");
            _gridView.Columns["PermissionLevel"].ColumnEdit = repoCombo;
            _gridView.OptionsBehavior.Editable = !isProtected;

            var btnSave = new SimpleButton { Text = LocalizationManager.T("Shared_BtnSave"), Location = new Point(240, 505), DialogResult = DialogResult.OK };
            btnSave.Click += (s, e) =>
            {
                _gridView.CloseEditor();
                _gridView.UpdateCurrentRow();

                if (string.IsNullOrWhiteSpace(TxtName.Text))
                {
                    XtraMessageBox.Show(LocalizationManager.T("Roles_NameRequired"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new Point(320, 505), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblName); this.Controls.Add(TxtName);
            this.Controls.Add(lblPermissions);
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}
