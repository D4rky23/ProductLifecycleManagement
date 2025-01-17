using System.Windows;
using System.Windows.Controls;
using ProductLifecycleManagement.Services;

namespace ProductLifecycleManagement.Views
{
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService = new UserService(); // Serviciu pentru utilizatori

        public LoginWindow()
        {
            InitializeComponent();
        }

        // Controlează vizibilitatea placeholder-ului pentru Email
        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailPlaceholder.Visibility = string.IsNullOrEmpty(EmailTextBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        // Controlează vizibilitatea placeholder-ului pentru Password
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        // Logica pentru butonul de login
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // Verifică dacă utilizatorul există
            var user = _userService.AuthenticateUser(email, password);
            if (user != null)
            {
                // Verifică rolul utilizatorului și redirecționează
                if (_userService.IsAdmin(user.Id))
                {
                    var adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else
                {
                    var userWindow = new UserWindow();
                    userWindow.Show();
                }

                // Închide fereastra curentă
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Logica pentru butonul de exit
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(); // Instanțiază fereastra Register
            registerWindow.Show();
            this.Close(); // Închide fereastra curentă
        }
    }
}
