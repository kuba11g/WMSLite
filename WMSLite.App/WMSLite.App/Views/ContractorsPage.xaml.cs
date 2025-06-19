using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WMSLite.App.Dialogs;
using WMSLite.App.Models;
using WMSLite.App.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WMSLite.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContractorsPage : Page
    {
        public ContractorsViewModel ViewModel { get; }

        public ContractorsPage()
        {
            this.InitializeComponent();
            ViewModel = new ContractorsViewModel();
            this.Loaded += async (s, e) => await ViewModel.LoadContractorsCommand.ExecuteAsync(null);
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newContractor = new Contractor();
            var dialog = new ContractorDialog(newContractor)
            {
                XamlRoot = this.XamlRoot,
                RequestedTheme = this.ActualTheme
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await ViewModel.AddContractorCommand.ExecuteAsync(dialog.Contractor);
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContractorsGrid.SelectedItem is Contractor selectedContractor)
            {
                var contractorToEdit = new Contractor
                {
                    Id = selectedContractor.Id,
                    Name = selectedContractor.Name,
                    Symbol = selectedContractor.Symbol
                };

                var dialog = new ContractorDialog(contractorToEdit)
                {
                    XamlRoot = this.XamlRoot,
                    RequestedTheme = this.ActualTheme
                };
                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    await ViewModel.UpdateContractorCommand.ExecuteAsync(dialog.Contractor);
                }
            }
        }
    }
}
