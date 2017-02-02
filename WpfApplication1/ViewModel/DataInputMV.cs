using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Util;

namespace WpfApplication1.ViewModel
{
    class DataInputMV
    {
        UserData userData;
        User user;
        ConnexionDb connexion = ConnexionDb.Instance();

        public DataInputMV(User user)
        {
            this.user = user;
            this.userData = new UserData();
            this.userData.DataContext = this;
            this.userData.BtnClick.Click += BtnClick_Click;

            Application.Current.Windows.OfType<Window>().FirstOrDefault().Content = this.userData;
        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            string input = this.userData.txtInput.Text;
            Data data = new Data();
            data.DataJson = input;
            data.UserId = user.Id;

            if (JsonUtils.IsValidJson(input))
            {
                this.connexion.insertData(data);
            }
        }
    }
}
