using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WMSLite.App.Models;
using WMSLite.App.Services;

namespace WMSLite.App.ViewModels
{
    public partial class ContractorsViewModel : ObservableObject
    {
        private readonly ApiClient _apiClient;

        [ObservableProperty]
        private ObservableCollection<Contractor> _contractors;

        public ContractorsViewModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
            Contractors = new ObservableCollection<Contractor>();
        }

        [RelayCommand]
        public async Task LoadContractorsAsync()
        {
            var contractorsList = await _apiClient.GetContractorsAsync();
            Contractors.Clear();
            foreach (var contractor in contractorsList)
            {
                Contractors.Add(contractor);
            }
        }

        [RelayCommand]
        private async Task AddContractorAsync(Contractor contractor)
        {
            var newContractor = await _apiClient.AddContractorAsync(contractor);
            Contractors.Add(newContractor);
        }

        [RelayCommand]
        private async Task UpdateContractorAsync(Contractor contractor)
        {
            await _apiClient.UpdateContractorAsync(contractor.Id, contractor);
            await LoadContractorsAsync();
        }
    }
}