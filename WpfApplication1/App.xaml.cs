using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.ViewModel;

namespace WpfApplication1
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Window MainWindow = new Window();
            MainWindow.Content = new UserLog();
            MainWindow.Loaded += MainWindow_Loaded;
            MainWindow.Show();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UserLogMV viewModel = new UserLogMV();
        }
    }
}
