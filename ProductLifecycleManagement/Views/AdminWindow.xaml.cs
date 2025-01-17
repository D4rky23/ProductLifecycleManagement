using System.Windows;
using ProductLifecycleManagement.Services;

namespace ProductLifecycleManagement.Views
{
    public partial class AdminWindow : Window
    {
        private readonly ProductService _productService = new ProductService();
        private readonly ProductStageHistoryService _historyService = new ProductStageHistoryService();

        public AdminWindow()
        {
            InitializeComponent();
            LoadProducts();
            LoadHistory();
        }

        private void LoadProducts()
        {
            ProductsGrid.ItemsSource = _productService.GetAllProducts();
        }

        private void LoadHistory()
        {
            HistoryGrid.ItemsSource = _historyService.GetAllHistories();
        }

     /*   private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
            LoadProducts();
        } */

    /*    private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsGrid.SelectedItem as Models.Product;
            if (selectedProduct != null)
            {
                var editProductWindow = new EditProductWindow(selectedProduct);
                editProductWindow.ShowDialog();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to edit.");
            }
        } */

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsGrid.SelectedItem as Models.Product;
            if (selectedProduct != null)
            {
                _productService.DeleteProduct(selectedProduct.Id);
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }
    }
}
