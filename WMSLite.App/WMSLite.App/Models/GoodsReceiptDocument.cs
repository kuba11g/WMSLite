using System;
using System.Collections.Generic;

namespace WMSLite.App.Models
{
    public class GoodsReceiptDocument
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Symbol { get; set; }
        public int ContractorId { get; set; }
        public Contractor? Contractor { get; set; }
        public List<DocumentItem> DocumentItems { get; set; } = new List<DocumentItem>();
    }
}