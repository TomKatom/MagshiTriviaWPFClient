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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private SockClient client = null;
        private ContextMenu cm = null;
        public MainMenu(SockClient client)
        {
            InitializeComponent();
            this.client = client;
        }
        private void OnClickCreateRoom(object sender, RoutedEventArgs e)
        {
            new CreateRoomWindow(this.client).Show();
            this.Close();
        }
        public void OnClickStatistics(object sender, RoutedEventArgs e)
        {
            cm = this.FindResource("cmStatistics") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        public void OnClickExit(object sender, RoutedEventArgs e)
        {
            cm.IsOpen = false;
            this.Close();
        }

        private void OnClickPersonal(object sender, RoutedEventArgs e)
        {
            cm.IsOpen = false;
            new PersonalStatistics(this.client).Show();
            this.Close();
        }

        private void OnClickJoinRoom(object sender, RoutedEventArgs e)
        {
            new JoinRoom(client).Show();
            this.Close();
        }
    }
}
