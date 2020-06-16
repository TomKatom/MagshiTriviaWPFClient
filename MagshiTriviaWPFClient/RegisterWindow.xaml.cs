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

namespace MagshiTriviaWPFClient
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private MainWindow mainWindow;
        public RegisterWindow()
        {
            InitializeComponent();
        }
        public void MoveToLogin(object sender, RoutedEventArgs e)
        {
            
            mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
