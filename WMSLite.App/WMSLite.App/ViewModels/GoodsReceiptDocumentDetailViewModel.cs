using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WMSLite.App.Models;
using WMSLite.App.Services;

namespace WMSLite.App.ViewModels
{
    public partial class GoodsReceiptDocumentDetailViewModel : ObservableObject
    {
        private readonly ApiClient _apiClient;

        [ObservableProperty]
        private GoodsReceiptDocument _document;

        [ObservableProperty]
        private ObservableCollection<DocumentItem> _documentItems;

        [ObservableProperty]
        private ObservableCollection<Contractor> _availableContractors;

        [ObservableProperty]
        private bool _canAddItems;

        public Contractor? SelectedContractor
        {
            get => Document != null && AvailableContractors != null ? AvailableContractors.FirstOrDefault(c => c.Id == Document.ContractorId) : null;
            set
            {
                if (Document != null && value != null && Document.ContractorId != value.Id)
                {
                    Document.ContractorId = value.Id;
                    Document.Contractor = value;
                    OnPropertyChanged(nameof(SelectedContractor));
                }
            }
        }

        public GoodsReceiptDocumentDetailViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            DocumentItems = new ObservableCollection<DocumentItem>();
            AvailableContractors = new ObservableCollection<Contractor>();
            _document = new GoodsReceiptDocument();
            CanAddItems = false;
        }

        public async Task LoadDocumentAsync(int documentId)
        {
            await LoadContractorsAsync();
            Document = await _apiClient.GetGoodsReceiptDocumentAsync(documentId);
            DocumentItems.Clear();
            if (Document?.DocumentItems != null)
            {
                foreach (var item in Document.DocumentItems)
                {
                    DocumentItems.Add(item);
                }
            }
            OnPropertyChanged(nameof(SelectedContractor));
            CanAddItems = Document.Id > 0;
        }

        public async Task CreateNewDocumentAsync()
        {
            Document = new GoodsReceiptDocument();
            DocumentItems.Clear();
            await LoadContractorsAsync();
            CanAddItems = false;
        }

        private async Task LoadContractorsAsync()
        {
            var contractorsList = await _apiClient.GetContractorsAsync();
            AvailableContractors.Clear();
            foreach (var contractor in contractorsList)
            {
                AvailableContractors.Add(contractor);
            }
        }

        [RelayCommand]
        public async Task SaveDocumentAsync(XamlRoot xamlRoot)
        {
            if (Document == null || xamlRoot == null) return;

            if (string.IsNullOrWhiteSpace(Document.Symbol))
            {
                await ShowDialogAsync("Błąd walidacji", "Pole 'Symbol' nie może być puste.", xamlRoot);
                return;
            }
            if (Document.ContractorId == 0)
            {
                await ShowDialogAsync("Błąd walidacji", "Proszę wybrać kontrahenta z listy.", xamlRoot);
                return;
            }

            try
            {
                bool isNewDocument = (Document.Id == 0);
                if (isNewDocument)
                {
                    var newDoc = await _apiClient.AddGoodsReceiptDocumentAsync(Document);
                    await LoadDocumentAsync(newDoc.Id);
                }
                else
                {
                    await _apiClient.UpdateGoodsReceiptDocumentAsync(Document.Id, Document);
                }

                CanAddItems = true;
                await ShowDialogAsync("Sukces", "Dokument został pomyślnie zapisany.", xamlRoot);
            }
            catch (Exception ex)
            {
                await ShowDialogAsync("Błąd zapisu", $"Wystąpił nieoczekiwany błąd: {ex.Message}", xamlRoot);
            }
        }

        [RelayCommand]
        public async Task AddItemAsync(DocumentItem item)
        {
            if (Document == null || Document.Id == 0) return;

            item.GoodsReceiptDocumentId = Document.Id;
            var newItem = await _apiClient.AddDocumentItemAsync(item);
            DocumentItems.Add(newItem);
        }

        [RelayCommand]
        public async Task UpdateItemAsync(DocumentItem item)
        {
            if (Document == null) return;
            await _apiClient.UpdateDocumentItemAsync(item.Id, item);
            await LoadDocumentAsync(Document.Id);
        }

        public async Task RefreshContractors()
        {
            var currentId = SelectedContractor?.Id;
            await LoadContractorsAsync();
            if (currentId.HasValue)
            {
                OnPropertyChanged(nameof(SelectedContractor));
            }
        }

        private async Task ShowDialogAsync(string title, string content, XamlRoot xamlRoot)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                XamlRoot = xamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}