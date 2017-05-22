using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel 
{
    public class LoginWindowViewModel : ViewModelBase
    {

        public LoginWindowViewModel()
        {
            this.LoginCommand = new RelayCommand(Login);
        }

        public String userName { get; set; }
       public String password { get; set; }
        public RelayCommand LoginCommand { get; set; }

        MyDBContext dc = new MyDBContext();

        private void Login()
        {
            var myUser = dc.Login.FirstOrDefault(u => u.Username == this.userName
                 && u.Password == this.password);

            if(myUser != null)
            {
                ViewService.CloseDialog(this);
                ViewService.ShowDialog(new MainWindowViewModel(true));
                // sikerult belogolni
            }
            else
            {
                System.Windows.MessageBox.Show("Login nem sikerult");
                //nem sikerutl
            }
        }
    }
}
