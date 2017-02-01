using Classes;
using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;
using Util;

namespace WpfApplication1
{
    /// <summary>
    /// Logique d'interaction pour DataInput.xaml
    /// </summary>
    public partial class DataInput : Window
    {
        User user;
        ConnexionDb connexion = ConnexionDb.Instance();

        public DataInput(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = this.txtInput.Text;

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
