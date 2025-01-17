using System.Windows;

namespace ProductLifecycleManagement
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Exemplu de inițializare
            var loginWindow = new Views.LoginWindow();
            loginWindow.Show();
        }
    }
}
