using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Util;
using WpfApplication1.View;

namespace WpfApplication1.ViewModel
{
    class CreateUserMV : EntityBase
    {
        private CreateUser user;
        private ConnexionDb connection = ConnexionDb.Instance();

        private List<Role> roles;

        public List<Role> Roles
        {
            get { return roles; }
            set {
                roles = value;
                
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                //NotifyPropertyChanged("IsChecked");
            }
        }



        public CreateUserMV()
        {
            this.user = new CreateUser();
            this.user.DataContext = this;
            this.user.btnClick.Click += BtnClick_Click;
            this.Roles = getRoles();

            this.user.comboBox.ItemsSource = Roles;

            //this.Roles = getRoles();
            Application.Current.Windows.OfType<Window>().FirstOrDefault().Content = this.user;

        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {

            
            // Insertion de l'utilisateur en base
            throw new NotImplementedException();
        }

        private List<Role> getRoles ()
        {
            return this.connection.getRoles();
        }
    }
}
