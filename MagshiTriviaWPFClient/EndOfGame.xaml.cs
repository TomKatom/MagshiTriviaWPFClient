using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MagshiTriviaWPFClient
{
    /// <summary>
    /// Interaction logic for EndOfGame.xaml
    /// </summary>
    public partial class EndOfGame : Window
    {
        private SockClient client = null;
        private DataGrid leaderboard = null;
        private TextBlock txt = null;
        private Border border = null;
        private BackgroundWorker resultsWorker = null;
        private PlayerResults[] players;
        
        public EndOfGame(SockClient client)
        {
            InitializeComponent();
            this.client = client;
            this.leaderboard = (DataGrid)this.FindName("Leaderboard");
            this.border = (Border)this.FindName("Border");
            this.txt = (TextBlock)this.FindName("Txt");
            this.resultsWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            this.resultsWorker.DoWork += UpdateLeaderboard;
            this.resultsWorker.RunWorkerAsync();

        }
        private void UpdateLeaderboard(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (this.resultsWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(3000);
                if (this.resultsWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (!this.resultsWorker.CancellationPending)
                {
                    var res = this.client.GetGameResult();
                    if(res.status != ResponseStatus.gameHasNotEnded)
                    {
                        this.players = res.results;
                        Array.Sort(this.players);
                        Array.Reverse(this.players);

                        this.txt.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                        {
                            Txt.Visibility = Visibility.Hidden;
                        }));
                        this.border.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                        {
                            Border.Visibility = Visibility.Visible;
                        }));
                        this.leaderboard.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                        {
                            Leaderboard.ItemsSource = this.players;
                            Leaderboard.Items.Refresh();
                        }));
                        e.Cancel = true;
                        return;

                    }
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void OnClickReturn(object sender, RoutedEventArgs e)
        {
            this.resultsWorker.CancelAsync();
            new MainMenu(this.client).Show();
            this.Close();
        }
    }
    public class PlayerResults : IComparable
    {
        public PlayerResults(double averageAnswerTime, int correctAnswerCount, string username, int wrongAnswerCount)
        {
            this.averageAnswerTime = averageAnswerTime;
            this.correctAnswerCount = correctAnswerCount;
            this.username = username;
            this.wrongAnswerCount = wrongAnswerCount;
        }

        public double averageAnswerTime { get; set; }
        public int correctAnswerCount { get; set; }
        public string username { get; set; }
        public int wrongAnswerCount { get; set; }

        public int CompareTo(object obj)
        {
            PlayerResults other = (PlayerResults)obj;
            double thisValue = (this.correctAnswerCount - this.wrongAnswerCount) / this.averageAnswerTime;
            double otherValue = (other.correctAnswerCount - other.wrongAnswerCount) / other.averageAnswerTime;
            return thisValue.CompareTo(otherValue);
        }

        public static bool operator <(PlayerResults a, PlayerResults b)
        {
            return (double)a.correctAnswerCount / a.averageAnswerTime < (double)b.correctAnswerCount / b.averageAnswerTime;
        }
        public static bool operator >(PlayerResults a, PlayerResults b)
        {
            return (double)a.correctAnswerCount / a.averageAnswerTime > (double)b.correctAnswerCount / b.averageAnswerTime;
        }
    }
}
