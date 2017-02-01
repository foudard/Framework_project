using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Util;

namespace WpfApplication1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConnexionDb connection = ConnexionDb.Instance();

        public MainWindow()
        {
            InitializeComponent();
        }

        private int searchInDb(string login, string pwd)
        {
            int exist = this.connection.checkConnection(login, pwd);
            return exist;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int idUser = this.searchInDb(this.txtLogin.Text.ToString(), this.txtPwd.Password.ToString());
            if (idUser != -1 )
            {
                User user = this.connection.getUser(idUser);

                var dataInput = new DataInput(user);
                dataInput.Show();
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Login ou mot de passe invalide !");
            }
        }
    }
}
