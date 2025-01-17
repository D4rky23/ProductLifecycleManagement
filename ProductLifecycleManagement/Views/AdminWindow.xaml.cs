using System.Windows;
using System.Windows.Input;
using ProductLifecycleManagement.ViewModels;

namespace ProductLifecycleManagement.Views
{
    public partial class AdminWindow : Window
    {
        private readonly ProductViewModel _viewModel;

        public AdminWindow()
        {
            InitializeComponent();
            _viewModel = new ProductViewModel();
            DataContext = _viewModel;
            LoadProducts();
        }

        private void LoadProducts()
        {
            // Obține lista de produse și actualizează ViewModel-ul
            _viewModel.LoadAllProducts();
        }
        private void LogoutCommand_Executed(object sender, ExecutedRoutedEventArgs e) 
        { 
            Application.Current.Shutdown(); 
        }
    }
}
