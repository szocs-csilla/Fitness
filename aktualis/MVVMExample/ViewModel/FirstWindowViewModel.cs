using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ViewModel
{
    public class FirstWindowViewModel : ViewModelBase
    {
        public FirstWindowViewModel(MainWindowViewModel mainWindowViewModel, LoginWindowViewModel loginWindowViewModel)
        {
            this.UserCommand = new RelayCommand(OpenMainWindow);
            this.LoginCommand = new RelayCommand(OpenLoginWindow);
            this.mainWindowViewModel = mainWindowViewModel;
            this.loginWindowViewModel = loginWindowViewModel;
        }
        public RelayCommand UserCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }

        private MainWindowViewModel mainWindowViewModel { get; set; }
        private LoginWindowViewModel loginWindowViewModel { get; set; }

        private void OpenMainWindow()
        {
            ViewService.CloseDialog(this);
            ViewService.ShowDialog(mainWindowViewModel);
        }

        private void OpenLoginWindow()
        {
            ViewService.CloseDialog(this);
            ViewService.ShowDialog(loginWindowViewModel);
        }

    }
}
