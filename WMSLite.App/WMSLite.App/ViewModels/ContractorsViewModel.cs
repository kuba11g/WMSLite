using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSLite.App.Models;
using WMSLite.App.Services;

namespace WMSLite.App.ViewModels
{
    public partial class ContractorsViewModel : ObservableObject
    {
        private readonly ApiService apiService;

        [ObservableProperty]
        private ObservableCollection<Contractor> contractors;

        public ContractorsViewModel()
        {
            apiService = new ApiService();
            LoadContractorsCommand = new AsyncRelayCommand(LoadContractorsAsync);
        }

        public IAsyncRelayCommand LoadContractorsCommand { get; private set; }

        private async Task LoadContractorsAsync()
        {
            var contractorsList = await apiService.GetContractorsAsync();
            Contractors = new ObservableCollection<Contractor>(contractorsList);
        }

        [RelayCommand]
        private async Task AddContractorAsync(Contractor contractor)
        {
            await apiService.AddContractorAsync(contractor);
            await LoadContractorsAsync();
        }

        [RelayCommand]
        private async Task UpdateContractorAsync(Contractor contractor)
        {
            await apiService.UpdateContractorAsync(contractor);
            await LoadContractorsAsync();
        }
    }
}
