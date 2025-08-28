using System.Configuration;
using System.Data;
using System.Windows;
using NewStarTicket.Views;

namespace NewStarTicket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    loginView.Close();
                }
            };
        }
    }

}
