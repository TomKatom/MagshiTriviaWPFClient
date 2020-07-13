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
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : Window
    {
        private SockClient client = null;
        private DataGrid grid = null;
        public Leaderboard(SockClient client)
        {
            InitializeComponent();
            this.client = client;
            this.grid = (DataGrid)this.FindName("Grid");
            var res = this.client.GetLeaderboard();
            this.grid.ItemsSource = res.players;
        }

        private void OnClickBack(object sender, RoutedEventArgs e)
        {
            new MainMenu(this.client).Show();
            this.Close();
        }
    }

    public class LeaderboardEntry
    {
        public LeaderboardEntry(string username, int numOfGames, int totalWins, int totalLosses)
        {
            this.username = username;
            this.numOfGames = numOfGames;
            this.totalWins = totalWins;
            this.totalLosses = totalLosses;
        }
        public string username { get; set; }
        public int numOfGames { get; set; }
        public int totalWins { get; set; }
        public int totalLosses { get; set; }
    }
}
