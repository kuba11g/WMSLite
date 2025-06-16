using System;
using System.Collections.Generic;

namespace WMSLite.Api.Models;

public partial class DocumentItem
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string UnitOfMeasure { get; set; } = null!;

    public decimal Quantity { get; set; }

    public int GoodsReceiptDocumentId { get; set; }

    public virtual GoodsReceiptDocument GoodsReceiptDocument { get; set; } = null!;
}
