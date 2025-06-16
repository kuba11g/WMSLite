using System;
using System.Collections.Generic;

namespace WMSLite.Api.Models;

public partial class Contractor
{
    public int Id { get; set; }

    public string Symbol { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<GoodsReceiptDocument> GoodsReceiptDocuments { get; set; } = new List<GoodsReceiptDocument>();
}
