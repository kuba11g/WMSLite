using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSLite.App.Models
{
    public class GoodsReceiptDocument
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        public ICollection<DocumentItem> DocumentItems { get; set; } = new List<DocumentItem>();
    }
}
