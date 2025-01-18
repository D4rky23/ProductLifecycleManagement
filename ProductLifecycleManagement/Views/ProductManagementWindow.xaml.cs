using System;
using System.Windows;
using ProductLifecycleManagement.ViewModels;
using ProductLifecycleManagement.Models;
using ProductLifecycleManagement.Services;

namespace ProductLifecycleManagement.Views
{
    public partial class ProductManagementWindow : Window
    {
        private readonly ProductService _productService;

        public ProductManagementWindow()
        {
            InitializeComponent();
            DataContext = new ProductViewModel();
            _productService = new ProductService();

            // Event for selecting a product from DataGrid
            ((ProductViewModel)DataContext).PropertyChanged += OnSelectedProductChanged;
        }

        private void OnSelectedProductChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductViewModel.SelectedProduct))
            {
                var viewModel = (ProductViewModel)DataContext;
                var selectedProduct = viewModel.SelectedProduct;

                if (selectedProduct != null)
                {
                    ProductNameTextBox.Text = selectedProduct.Name;
                    ProductDescriptionTextBox.Text = selectedProduct.Description;
                    ProductHeightTextBox.Text = selectedProduct.EstimatedHeight.ToString();
                    ProductWidthTextBox.Text = selectedProduct.EstimatedWidth.ToString();
                    ProductWeightTextBox.Text = selectedProduct.EstimatedWeight.ToString();
                    ProductBOMIdTextBox.Text = selectedProduct.BOMId.ToString();
                }
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                var viewModel = DataContext as ProductViewModel;
                if (viewModel != null)
                {
                    var newProduct = new Product
                    {
                        Name = ProductNameTextBox.Text,
                        Description = ProductDescriptionTextBox.Text,
                        EstimatedHeight = float.Parse(ProductHeightTextBox.Text),
                        EstimatedWidth = float.Parse(ProductWidthTextBox.Text),
                        EstimatedWeight = float.Parse(ProductWeightTextBox.Text),
                        BOMId = int.Parse(ProductBOMIdTextBox.Text)
                    };

                    if (_productService.IsBOMIdValid(newProduct.BOMId))
                    {
                        try
                        {
                            viewModel.AddProductCommand.Execute(newProduct);
                            ClearForm();
                            MessageBox.Show("Product added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex) when (ex.Message.Contains("There is already a product with the same BOM Id."))
                        {
                            MessageBox.Show("A product with the same BOM Id already exists. Please use a different BOM Id.", "Duplicate BOM Id", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid BOM Id. Please enter a valid BOM Id.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                var viewModel = DataContext as ProductViewModel;
                if (viewModel != null && viewModel.SelectedProduct != null)
                {
                    viewModel.SelectedProduct.Name = ProductNameTextBox.Text;
                    viewModel.SelectedProduct.Description = ProductDescriptionTextBox.Text;
                    viewModel.SelectedProduct.EstimatedHeight = float.Parse(ProductHeightTextBox.Text);
                    viewModel.SelectedProduct.EstimatedWidth = float.Parse(ProductWidthTextBox.Text);
                    viewModel.SelectedProduct.EstimatedWeight = float.Parse(ProductWeightTextBox.Text);
                    // BOM Id is not changed during editing
                    viewModel.EditProductCommand.Execute(viewModel.SelectedProduct);
                    ClearForm();
                    MessageBox.Show("Product updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ProductViewModel;
            if (viewModel != null && viewModel.SelectedProduct != null)
            {
                viewModel.DeleteProductCommand.Execute(viewModel.SelectedProduct);
                ClearForm();
                MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsInputValid()
        {
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text))
            {
                MessageBox.Show("Product Name is required.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ProductDescriptionTextBox.Text))
            {
                MessageBox.Show("Product Description is required.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!float.TryParse(ProductHeightTextBox.Text, out float height) || height <= 0)
            {
                MessageBox.Show("Estimated Height must be a positive number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!float.TryParse(ProductWidthTextBox.Text, out float width) || width <= 0)
            {
                MessageBox.Show("Estimated Width must be a positive number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!float.TryParse(ProductWeightTextBox.Text, out float weight) || weight <= 0)
            {
                MessageBox.Show("Estimated Weight must be a positive number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(ProductBOMIdTextBox.Text, out int bomId) || bomId <= 0)
            {
                MessageBox.Show("BOM Id must be a positive integer.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            ProductNameTextBox.Clear();
            ProductDescriptionTextBox.Clear();
            ProductHeightTextBox.Clear();
            ProductWidthTextBox.Clear();
            ProductWeightTextBox.Clear();
            ProductBOMIdTextBox.Clear();
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var viewModel = (ProductViewModel)DataContext;
            if (viewModel != null && viewModel.SelectedProduct != null)
            {
                ProductNameTextBox.Text = viewModel.SelectedProduct.Name;
                ProductDescriptionTextBox.Text = viewModel.SelectedProduct.Description;
                ProductHeightTextBox.Text = viewModel.SelectedProduct.EstimatedHeight.ToString();
                ProductWidthTextBox.Text = viewModel.SelectedProduct.EstimatedWidth.ToString();
                ProductWeightTextBox.Text = viewModel.SelectedProduct.EstimatedWeight.ToString();
                ProductBOMIdTextBox.Text = viewModel.SelectedProduct.BOMId.ToString();
            }
        }
    }
}
