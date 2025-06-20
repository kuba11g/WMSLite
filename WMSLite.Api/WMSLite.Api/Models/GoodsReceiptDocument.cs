using System;
using System.Collections.Generic;

namespace WMSLite.Api.Models;

public partial class GoodsReceiptDocument
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Symbol { get; set; } = null!;

    public int ContractorId { get; set; }

    public virtual ICollection<DocumentItem> DocumentItems { get; set; } = new List<DocumentItem>();
}
