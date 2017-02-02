using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.View;

namespace WpfApplication1.ViewModel
{
    class CreateUserMV
    {
        CreateUser user;
        public CreateUserMV()
        {
            this.user = new CreateUser();
            this.user.DataContext = this;
            this.user.btnClick.Click += BtnClick_Click;

            Application.Current.Windows.OfType<Window>().FirstOrDefault().Content = this.user;
        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
