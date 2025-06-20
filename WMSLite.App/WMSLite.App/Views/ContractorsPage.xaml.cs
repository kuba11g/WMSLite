using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using WMSLite.App.Models;
using WMSLite.App.ViewModels;

namespace WMSLite.App.Views
{
    public sealed partial class ContractorsPage : Page
    {
        public ContractorsViewModel ViewModel { get; }

        public ContractorsPage()
        {
            this.InitializeComponent();
            ViewModel = App.Current.Services.GetService(typeof(ContractorsViewModel)) as ContractorsViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadContractorsAsync();
        }

        private async void AddContractor_Click(object sender, RoutedEventArgs e)
        {
            await ShowEditContractorDialog(new Contractor());
        }

        private async void Contractor_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (((ListView)sender).SelectedItem is Contractor selected)
            {
                await ShowEditContractorDialog(selected);
            }
        }

        private async Task ShowEditContractorDialog(Contractor contractor)
        {
            bool isNew = contractor.Id == 0;

            var symbolTextBox = new TextBox { Header = "Symbol", Text = contractor.Symbol ?? "" };
            var nameTextBox = new TextBox { Header = "Nazwa", Text = contractor.Name ?? "" };

            var content = new StackPanel { Spacing = 12 };
            content.Children.Add(symbolTextBox);
            content.Children.Add(nameTextBox);

            var dialog = new ContentDialog
            {
                Title = isNew ? "Dodaj nowego kontrahenta" : "Edytuj kontrahenta",
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

                if (isNew)
                {
                    await ViewModel.AddContractorCommand.ExecuteAsync(contractor);
                }
                else
                {
                    await ViewModel.UpdateContractorCommand.ExecuteAsync(contractor);
                }
            }
        }
    }
}