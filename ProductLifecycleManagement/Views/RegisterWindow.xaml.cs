using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ProductLifecycleManagement.Services;
using ProductLifecycleManagement.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProductLifecycleManagement.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly UserService _userService = new UserService(); // Serviciu pentru utilizatori
        private readonly UserRoleService _userRoleService = new UserRoleService(); // Serviciu pentru roluri

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string name = NameTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string password = PasswordBox.Password;

            // Validările input-urilor
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Invalid email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_userService.EmailExists(email))
            {
                MessageBox.Show("Email already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(phoneNumber) || !IsValidPhoneNumber(phoneNumber))
            {
                MessageBox.Show("Invalid phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password) || !IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long and include a number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Hash-ul parolei
            string hashedPassword = HashPassword(password);

            // Crează obiectul User
            User newUser = new User
            {
                Email = email,
                Name = name,
                PhoneNumber = phoneNumber,
                Password = hashedPassword
            };

            // Adaugă utilizatorul în baza de date și obține ID-ul
            int userId = _userService.AddUser(newUser);

            // Setează rolul utilizatorului ca "User" (RoleId = 2)
            UserRole userRole = new UserRole
            {
                UserId = userId,
                RoleId = 2
            };
            _userRoleService.AddUserRole(userRole);

            MessageBox.Show("User registered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Redirecționează la LoginWindow
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string phonePattern = @"^[0-9]*$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 8 && Regex.IsMatch(password, @"\d");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Placeholder controls
        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailPlaceholder.Visibility = string.IsNullOrEmpty(EmailTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NamePlaceholder.Visibility = string.IsNullOrEmpty(NameTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhoneNumberPlaceholder.Visibility = string.IsNullOrEmpty(PhoneNumberTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
