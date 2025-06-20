using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WMSLite.App.Models;
using WMSLite.App.Services;

namespace WMSLite.App.ViewModels
{
    public partial class GoodsReceiptDocumentsViewModel : ObservableObject
    {
        private readonly ApiClient _apiClient;

        [ObservableProperty]
        private ObservableCollection<GoodsReceiptDocument> _documents;

        public GoodsReceiptDocumentsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            Documents = new ObservableCollection<GoodsReceiptDocument>();
        }

        [RelayCommand]
        public async Task LoadDocumentsAsync()
        {
            var documentList = await _apiClient.GetGoodsReceiptDocumentsAsync();
            Documents.Clear();
            foreach (var doc in documentList)
            {
                Documents.Add(doc);
            }
        }
    }
}