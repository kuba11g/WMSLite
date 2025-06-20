using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WMSLite.App.Views
{
    public sealed partial class MainDashboardPage : Page
    {
        public MainDashboardPage()
        {
            this.InitializeComponent();
        }

        private void GoToDocuments_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GoodsReceiptDocumentsPage));
        }

        private void GoToContractors_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ContractorsPage));
        }
    }
}