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
            User user = Login.Log(this.userLog.txtLogin.Text.ToString(), this.userLog.txtPwd.Password.ToString());

            if (user.Id != 0)
            {
                if (Login.checkRole(user, "admin"))
                {
                    CreateUserMV createUserMV = new CreateUserMV();
                }
                else if (Login.checkRole(user, "user"))
                {
                    DataInputMV viewModel = new DataInputMV(user);
                }
            }
        }

       
    }
}
