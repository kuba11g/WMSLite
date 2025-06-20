namespace WMSLite.App.Models
{
    public class DocumentItem
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal Quantity { get; set; }
        public int GoodsReceiptDocumentId { get; set; }
    }
}