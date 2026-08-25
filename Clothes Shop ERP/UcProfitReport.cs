using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcProfitReport : DevExpress.XtraEditors.XtraUserControl
    {
        private DateEdit DtFrom, DtTo;
        private GridControl GridResult;
        private GridView GridViewResult;
        private LabelControl LblSummary;
        public UcProfitReport()
        {
            InitializeComponent();
        }
    }
}
