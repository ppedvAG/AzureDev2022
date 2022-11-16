using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LoggerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogInfo(object sender, RoutedEventArgs e)
        {
            Log.Information("Log Info {@OS}", Environment.OSVersion.Version);
        }

        private void LogFatal(object sender, RoutedEventArgs e)
        {
            throw new OutOfMemoryException();
        }

        private void LogError(object sender, RoutedEventArgs e)
        {
            try
            {
                File.OpenRead("b:\\plqöekfmweflknm.txt");
            }
            catch (Exception ex)
            {

                Log.Error("Log Error {@error}", ex);
            }
        }
    }
}
