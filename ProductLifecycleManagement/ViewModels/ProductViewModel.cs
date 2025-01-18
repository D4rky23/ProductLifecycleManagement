using ProductLifecycleManagement.Models;
using ProductLifecycleManagement.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using ProductLifecycleManagement.Views;

namespace ProductLifecycleManagement.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly ProductService _productService;
        private Product _selectedProduct;

        // Constructor
        public ProductViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();
            LoadAllProducts();

            // Inițializare comenzi
            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            DashboardCommand = new RelayCommand(OpenDashboard);
            ProductManagementCommand = new RelayCommand(OpenProductManagement);
            ReportsCommand = new RelayCommand(OpenReports);
            SettingsCommand = new RelayCommand(OpenSettings);
            HelpCommand = new RelayCommand(OpenHelp);
            LogoutCommand = new RelayCommand(Logout);
        }

        // Colecția de produse
        public ObservableCollection<Product> Products { get; set; }

        // Proprietatea SelectedProduct
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        // Comenzi
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
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

        private void AddProduct(object parameter)
        {
            if (parameter is Product newProduct) // Verificăm dacă parametrul este de tip Product
            {
                try
                {
                    _productService.AddProduct(newProduct); // Apelăm serviciul pentru a adăuga produsul în baza de date
                    Products.Add(newProduct); // Adăugăm produsul în colecția locală
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error adding product: {ex.Message}", ex); // Aruncăm eroarea pentru debugging
                }
            }
            else
            {
                throw new ArgumentException("Invalid parameter passed to AddProduct.");
            }
        }


        private void EditProduct(object parameter)
        {
            if (SelectedProduct != null)
            {
                _productService.UpdateProduct(SelectedProduct);
                LoadAllProducts(); // Reîncărcare produse pentru a reflecta modificările
            }
        }

        private void DeleteProduct(object parameter)
        {
            if (SelectedProduct != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete '{SelectedProduct.Name}'?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _productService.DeleteProduct(SelectedProduct.Id);
                    Products.Remove(SelectedProduct);
                }
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

        private bool IsProductUnique(Product product) 
        {
            foreach (var existingProduct in Products)
            {
                if (existingProduct.Name == product.Name || 
                    existingProduct.Description == product.Description ||
                    existingProduct.EstimatedHeight == product.EstimatedHeight ||
                    existingProduct.EstimatedWidth == product.EstimatedWidth ||
                    existingProduct.EstimatedWeight == product.EstimatedWeight || 
                    existingProduct.BOMId == product.BOMId)
                {
                    return false;
                } 
            } return true; 
        }
    }
}
