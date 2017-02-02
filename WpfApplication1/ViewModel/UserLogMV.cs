using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Util;
using WpfApplication1;

namespace WpfApplication1.ViewModel
{
    public class UserLogMV
    {
        private ConnexionDb connection = ConnexionDb.Instance();
        private UserLog userLog;
        public UserLogMV()
        {
            this.userLog = new UserLog();
            this.userLog.DataContext = this;
            this.userLog.BtnClick.Click += BtnClick_Click;

            Application.Current.Windows.OfType<Window>().FirstOrDefault().Content = this.userLog;
        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            int idUser = this.searchInDb(this.userLog.txtLogin.Text.ToString(), this.userLog.txtPwd.Password.ToString());
            if (idUser != "admin")
            {
                User user = this.connection.getUser(idUser);
                DataInputMV viewModel = new DataInputMV(user);
            }
            else
            {
                MessageBox.Show("Login ou mot de passe invalide !");
            }
        }

        private string searchInDb(string login, string pwd)
        {
            string result = "";
            List<Role> roles = this.connection.checkConnection(login, pwd);
            foreach (Role item in roles)
            {
                if (item.Id == 1)
                {
                    result = "admin";
                }
                else if (result != "admin")
                {
                    result = "user";
                }
            }
            return result;
        }
    }
}
