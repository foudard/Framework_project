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
    public class CreateUserMV
    {
        private CreateUser user;
        private ConnexionDb connection = ConnexionDb.Instance();

        public class RoleList :  EntityBase
        {

            public RoleList(Role role)              
            {
                Role = role;
            }

            private Role role;

            public Role Role
            {
                get { return role; }
                set { role = value; }
            }

            private bool isChecked = false;
            public bool IsChecked
            {
                get
                {
                    return isChecked;
                }
                set
                {
                    isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }


        private List<RoleList> listRoles = new List<RoleList>();

        public List<RoleList> ListRoles
        {
            get { return listRoles; }
            set { listRoles = value; }
        }

        public CreateUserMV()
        {
            this.user = new CreateUser();
            this.user.DataContext = this;
            this.user.btnClick.Click += BtnClick_Click;
            initListRoles(getRoles());

            this.user.comboBox.ItemsSource = ListRoles;

            //this.Roles = getRoles();
            Application.Current.Windows.OfType<Window>().FirstOrDefault().Content = this.user;

        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les information de l'utilisateur à créer
            List<Role> selectedData = ListRoles.Where(d => d.IsChecked).Select(d => d.Role).ToList();

            // Insertion de l'utilisateur en base
            throw new NotImplementedException();
        }

        private List<Role> getRoles ()
        {
            return this.connection.getRoles();
        }

        private void initListRoles(List<Role> roles)
        {
            foreach(Role role in roles)
            {
                RoleList rl = new RoleList(role);
                ListRoles.Add(rl);
            }
        }
    }
}
