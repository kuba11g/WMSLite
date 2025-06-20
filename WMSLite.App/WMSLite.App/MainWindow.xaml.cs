using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using WMSLite.App.Views;

namespace WMSLite.App
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            Title = "WMSLite";
            ContentFrame.Navigate(typeof(MainDashboardPage));
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                Type? pageType = null;

                switch (navItemTag)
                {
                    case "Home":
                        pageType = typeof(MainDashboardPage);
                        break;
                    case "Documents":
                        pageType = typeof(GoodsReceiptDocumentsPage);
                        break;
                    case "Contractors":
                        pageType = typeof(ContractorsPage);
                        break;
                }

                if (pageType != null && pageType != ContentFrame.CurrentSourcePageType)
                {
                    ContentFrame.Navigate(pageType);
                }
            }
        }

        private void ContentFrame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(MainDashboardPage))
            {
                NavView.SelectedItem = NavView.MenuItems[0];
            }
            else if (ContentFrame.SourcePageType == typeof(GoodsReceiptDocumentsPage))
            {
                NavView.SelectedItem = NavView.MenuItems[1];
            }
            else if (ContentFrame.SourcePageType == typeof(ContractorsPage))
            {
                NavView.SelectedItem = NavView.MenuItems[2];
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }
    }
}