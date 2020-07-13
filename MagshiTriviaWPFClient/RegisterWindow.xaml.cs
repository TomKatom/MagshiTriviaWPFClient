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
        public Button registerButton = null;
        public TextBox userBox = null, emailTextBox = null ;
        public PasswordBox passBox = null;
        private SockClient client = null;
        public RegisterWindow(SockClient client)
        {
            InitializeComponent();
            this.registerBtn = (Button)this.FindName("registerBtn");
            this.userBox = (TextBox)this.FindName("usernameBox");
            this.passBox = (PasswordBox)this.FindName("passwordBox");
            this.emailTextBox = (TextBox)this.FindName("emailBox");
            this.client = client;
        }
        public void MoveToLogin(object sender, RoutedEventArgs e)
        {
            new MainWindow(this.client).Show();
            this.Close();
        }
        public void OnRegister(object sender, RoutedEventArgs e)
        {
            if(client.Register(new RegisterRequest(this.userBox.Text, this.emailTextBox.Text, this.passBox.Password)) == ResponseStatus.signUpSuccess)
            {
                MessageBox.Show("Account Created Successfully.");
                new MainWindow(this.client).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Creating Account.");
            }
        }
    }
}
