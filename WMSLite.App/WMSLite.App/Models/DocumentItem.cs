using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSLite.App.Models
{
    public class DocumentItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public int GoodsReceiptDocumentId { get; set; }
    }
}
