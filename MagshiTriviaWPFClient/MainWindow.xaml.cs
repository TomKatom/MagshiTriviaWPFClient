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

namespace MagshiTriviaWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RegisterWindow registerWindow;
        private MainMenu menuWindow;
        private SockClient client = null;
        private TextBox usernameBox = null;
        private PasswordBox passwordBox = null;
        private Border errorBox = null;
        public MainWindow()
        {
            InitializeComponent();
            client = new SockClient();
        }
        public MainWindow(SockClient client)
        {
            InitializeComponent();
            this.client = client;
        }
        public void OnClickLogin(object sender, RoutedEventArgs e)
        {
            usernameBox = (TextBox)this.FindName("username");
            passwordBox = (PasswordBox)this.FindName("password");
            errorBox = (Border)this.FindName("border");
            if(this.client.Login(new LoginRequest(usernameBox.Text, passwordBox.Password)) != ResponseStatus.loginSuccess)
            {
                errorBox.Visibility = Visibility.Visible;
            }
            else
            {
                menuWindow = new MainMenu(this.client);
                menuWindow.Show();
                this.Close();
            }
        }
        public void MoveToRegister(object sender, RoutedEventArgs e)
        {
            this.registerWindow = new RegisterWindow(this.client);
            this.registerWindow.Show();
            this.Close();
        }

    }
}
