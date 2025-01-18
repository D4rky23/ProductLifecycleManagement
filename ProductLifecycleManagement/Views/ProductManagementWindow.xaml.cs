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
                    viewModel.SelectedProduct.BOMId = int.Parse(ProductBOMIdTextBox.Text);

                    if (_productService.IsBOMIdValid(viewModel.SelectedProduct.BOMId))
                    {
                        try
                        {
                            viewModel.EditProductCommand.Execute(viewModel.SelectedProduct);
                            ClearForm();
                        }
                        catch (Exception ex) when (ex.Message.Contains("There is already a product with the same BOM Id."))
                        {
                            MessageBox.Show("A product with the same BOM Id already exists. Please use a different BOM Id.", "Duplicate BOM Id", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid BOM Id. Please enter a valid BOM Id.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
    }
}
