using Microsoft.Extensions.DependencyInjection;

namespace WMSLite.App.ViewModels
{
    public class ViewModelLocator
    {
        public ContractorsViewModel ContractorsViewModel => App.Current.Services.GetRequiredService<ContractorsViewModel>();
        public GoodsReceiptDocumentsViewModel GoodsReceiptDocumentsViewModel => App.Current.Services.GetRequiredService<GoodsReceiptDocumentsViewModel>();
        public GoodsReceiptDocumentDetailViewModel GoodsReceiptDocumentDetailViewModel => App.Current.Services.GetRequiredService<GoodsReceiptDocumentDetailViewModel>();
    }
}