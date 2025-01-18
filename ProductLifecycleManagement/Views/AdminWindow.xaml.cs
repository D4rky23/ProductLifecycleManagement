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

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            // Deschide fereastra Dashboard și închide AdminWindow
            var dashboardWindow = new DashboardWindow();
            dashboardWindow.Show();
            this.Close();
        }

        private void ProductManagement_Click(object sender, RoutedEventArgs e)
        {
            // Deschide fereastra ProductManagement și închide AdminWindow
            var productManagementWindow = new ProductManagementWindow();
            productManagementWindow.Show();
            this.Close();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            // Deschide fereastra Reports și închide AdminWindow
            var reportsWindow = new ReportsWindow();
            reportsWindow.Show();
            this.Close();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // Deschide fereastra Settings și închide AdminWindow
            var settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            // Deschide fereastra Help și închide AdminWindow
            var helpWindow = new HelpWindow();
            helpWindow.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Execută logout și închide AdminWindow
            MessageBox.Show("Logged out successfully.", "Logout", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Shutdown();
        }
    }
}
