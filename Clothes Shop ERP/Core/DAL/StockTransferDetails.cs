using System;
using System.Collections.Generic;

namespace Clothes_Shop_ERP.DAL
{
    public partial class StockTransferDetails
    {
        public int Id { get; set; }
        public int StockTransferId { get; set; }
        public int ProductVariantId { get; set; }
        public decimal Quantity { get; set; }

        public ProductVariants ProductVariant { get; set; }
        public StockTransfers StockTransfer { get; set; }
    }
}
