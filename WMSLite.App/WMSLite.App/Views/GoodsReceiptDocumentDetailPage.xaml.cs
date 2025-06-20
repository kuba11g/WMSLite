using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Globalization;
using System.Threading.Tasks;
using WMSLite.App.Models;
using WMSLite.App.ViewModels;

namespace WMSLite.App.Views
{
    public sealed partial class GoodsReceiptDocumentDetailPage : Page
    {
        public GoodsReceiptDocumentDetailViewModel ViewModel { get; }

        public GoodsReceiptDocumentDetailPage()
        {
            this.InitializeComponent();
            ViewModel = App.Current.Services.GetService(typeof(GoodsReceiptDocumentDetailViewModel)) as GoodsReceiptDocumentDetailViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is int documentId && documentId > 0)
            {
                await ViewModel.LoadDocumentAsync(documentId);
            }
            else
            {
                await ViewModel.CreateNewDocumentAsync();
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                await ViewModel.SaveDocumentCommand.ExecuteAsync(element.XamlRoot);
            }
        }

        private async void AddItem_Click(object sender, RoutedEventArgs e)
        {
            await ShowEditItemDialog(new DocumentItem());
        }

        private async void Item_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (((ListView)sender).SelectedItem is DocumentItem selected)
            {
                await ShowEditItemDialog(selected);
            }
        }

        private async Task ShowEditItemDialog(DocumentItem item)
        {
            bool isNew = item.Id == 0;

            var nameTextBox = new TextBox { Header = "Nazwa towaru", Text = item.ProductName ?? "" };
            var quantityTextBox = new TextBox { Header = "Iloœæ", Text = item.Quantity.ToString(CultureInfo.InvariantCulture) };
            var unitTextBox = new TextBox { Header = "Jednostka miary", Text = item.UnitOfMeasure ?? "" };

            var content = new StackPanel { Spacing = 12 };
            content.Children.Add(nameTextBox);
            content.Children.Add(quantityTextBox);
            content.Children.Add(unitTextBox);

            var dialog = new ContentDialog
            {
                Title = isNew ? "Dodaj pozycjê" : "Edytuj pozycjê",
                PrimaryButtonText = "Zapisz",
                CloseButtonText = "Anuluj",
                Content = content,
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                item.ProductName = nameTextBox.Text;
                item.UnitOfMeasure = unitTextBox.Text;
                if (decimal.TryParse(quantityTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal quantity))
                {
                    item.Quantity = quantity;
                }

                if (isNew)
                {
                    await ViewModel.AddItemCommand.ExecuteAsync(item);
                }
                else
                {
                    await ViewModel.UpdateItemCommand.ExecuteAsync(item);
                }
            }
        }

        private async void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            var contractor = new Contractor();
            var symbolTextBox = new TextBox { Header = "Symbol", Text = contractor.Symbol ?? "" };
            var nameTextBox = new TextBox { Header = "Nazwa", Text = contractor.Name ?? "" };
            var content = new StackPanel { Spacing = 12 };
            content.Children.Add(symbolTextBox);
            content.Children.Add(nameTextBox);

            var dialog = new ContentDialog
            {
                Title = "Dodaj nowego kontrahenta",
                PrimaryButtonText = "Zapisz",
                CloseButtonText = "Anuluj",
                Content = content,
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                contractor.Symbol = symbolTextBox.Text;
                contractor.Name = nameTextBox.Text;
                var contractorVM = App.Current.Services.GetService(typeof(ContractorsViewModel)) as ContractorsViewModel;
                await contractorVM.AddContractorCommand.ExecuteAsync(contractor);
                await ViewModel.RefreshContractors();
            }
        }
    }

    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is int i && i > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dt)
            {
                return new DateTimeOffset(dt);
            }
            return DateTimeOffset.MinValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dto)
            {
                return dto.DateTime;
            }
            return DateTime.MinValue;
        }
    }
}