using System.Windows;
using ProductLifecycleManagement.Services;

namespace ProductLifecycleManagement.Views
{
    public partial class UserWindow : Window
    {
        private readonly ProductService _productService = new ProductService();

        public UserWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            UserProductsGrid.ItemsSource = _productService.GetAllProducts();
        }

        private void AdvancePhase_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic for advancing the phase of a selected product.
            MessageBox.Show("Advance phase functionality is under development.");
        }
    }
}
