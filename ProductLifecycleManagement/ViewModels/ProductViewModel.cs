using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ProductLifecycleManagement.Models;
using ProductLifecycleManagement.Services;
using ProductLifecycleManagement.Views;

namespace ProductLifecycleManagement.ViewModels
{
    public class ProductViewModel
    {
        private readonly ProductService _productService;

        // Constructor
        public ProductViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();

            // Inițializare comenzi
            DashboardCommand = new RelayCommand(OpenDashboard);
            ProductManagementCommand = new RelayCommand(OpenProductManagement);
            ReportsCommand = new RelayCommand(OpenReports);
            SettingsCommand = new RelayCommand(OpenSettings);
            HelpCommand = new RelayCommand(OpenHelp);
            LogoutCommand = new RelayCommand(Logout);
        }

        // Colecția de produse
        public ObservableCollection<Product> Products { get; set; }

        // Comenzi
        public ICommand DashboardCommand { get; }
        public ICommand ProductManagementCommand { get; }
        public ICommand ReportsCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand HelpCommand { get; }
        public ICommand LogoutCommand { get; }

        // Metodă pentru încărcarea produselor
        public void LoadAllProducts()
        {
            var products = _productService.GetAllProducts();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        // Metode pentru comenzi
        private void OpenDashboard(object parameter)
        {
            var dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
        }

        private void OpenProductManagement(object parameter)
        {
            var productManagementWindow = new ProductManagementWindow();
            productManagementWindow.Show();
        }

        private void OpenReports(object parameter)
        {
            var reportsWindow = new ReportsWindow();
            reportsWindow.Show();
        }

        private void OpenSettings(object parameter)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void OpenHelp(object parameter)
        {
            var helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void Logout(object parameter)
        {
            Application.Current.Shutdown(); // Închide aplicația
        }
    }
}
