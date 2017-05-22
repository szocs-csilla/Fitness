using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Common;
using View;
using Model;
using ViewModel;

namespace MVVMExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            registerViews();
            OpenMainWindow();
        }
        private void registerViews()
        {
            ViewService.RegisterView(typeof(MainWindowViewModel), typeof(MainWindow));
            ViewService.RegisterView(typeof(FirstWindowViewModel), typeof(FirstWindow));
            ViewService.RegisterView(typeof(LoginWindowViewModel), typeof(LoginWindow));
        }

        private void OpenMainWindow()
        {

            FirstWindowViewModel fwvm = new FirstWindowViewModel(OpentMainWindowViewModel(), new LoginWindowViewModel());
            FirstWindow fw = new FirstWindow();
            ViewService.AddMainWindowToOpened(fwvm, fw);
            ViewService.ShowDialog(fwvm);
        }

        public MainWindowViewModel OpentMainWindowViewModel()
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel mainWindowVievModel = new MainWindowViewModel(false);
            ViewService.AddMainWindowToOpened(mainWindowVievModel, mainWindow);
            return mainWindowVievModel;
        }
    }

}
