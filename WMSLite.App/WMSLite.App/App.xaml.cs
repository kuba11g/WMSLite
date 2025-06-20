using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using WMSLite.App.Services;
using WMSLite.App.ViewModels;

namespace WMSLite.App
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            this.InitializeComponent();
            Services = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ApiClient>();
            services.AddTransient<ContractorsViewModel>();
            services.AddTransient<GoodsReceiptDocumentsViewModel>();
            services.AddTransient<GoodsReceiptDocumentDetailViewModel>();

            return services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}