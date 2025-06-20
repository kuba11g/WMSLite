using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using WMSLite.App.Models;
using WMSLite.App.ViewModels;

namespace WMSLite.App.Views
{
    public sealed partial class GoodsReceiptDocumentsPage : Page
    {
        public GoodsReceiptDocumentsViewModel ViewModel { get; }

        public GoodsReceiptDocumentsPage()
        {
            this.InitializeComponent();
            ViewModel = App.Current.Services.GetService(typeof(GoodsReceiptDocumentsViewModel)) as GoodsReceiptDocumentsViewModel;
            this.Resources.Add("DateTimeToShortDateStringConverter", new DateTimeToShortDateStringConverter());
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadDocumentsAsync();
        }

        private void AddDocument_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GoodsReceiptDocumentDetailPage), 0);
        }

        private void Document_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is GoodsReceiptDocument selected)
            {
                Frame.Navigate(typeof(GoodsReceiptDocumentDetailPage), selected.Id);
            }
        }
    }

    public class DateTimeToShortDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToShortDateString();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}