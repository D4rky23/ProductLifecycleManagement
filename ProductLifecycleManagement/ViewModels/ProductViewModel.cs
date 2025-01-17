using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ProductLifecycleManagement.Models;
using ProductLifecycleManagement.Services;

namespace ProductLifecycleManagement.ViewModels
{
    public class ProductViewModel
    {
        private readonly ProductService _productService;
        public ObservableCollection<Product> Products { get; set; }
        public ICommand LogoutCommand { get; }

        public ProductViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();
            LogoutCommand = new RelayCommand(Logout);
        }

        public void LoadAllProducts()
        {
            var products = _productService.GetAllProducts();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private void Logout()
        {
            Application.Current.Shutdown();
        }
    }
}
