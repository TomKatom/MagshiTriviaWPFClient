using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
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
    /// Interaction logic for RoomInfo.xaml
    /// </summary>
    public partial class RoomInfo : Window
    {
        private SockClient client = null;
        private Button closeButton = null, startButton = null, leaveButton = null;
        private ListBox participantList = null;
        private TextBlock maxTxt = null, timeText = null, numTxt = null, nameTxt = null;
        private Window thisWindow = null;

        private void OnClickLeave(object sender, RoutedEventArgs e)
        {
            this.client.LeaveRoom();
            new MainMenu(this.client).Show();
            participantUpdate.CancelAsync();
            this.Close();

        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            this.client.CloseRoom();
            new MainMenu(this.client).Show();
            participantUpdate.CancelAsync();
            this.Close();
        }

        private void OnClickStart(object sender, RoutedEventArgs e)
        {
            this.client.StartGame();
            participantUpdate.CancelAsync();
            new Game(this.client, int.Parse(this.numTxt.Text), int.Parse(this.timeText.Text)).Show();
            this.Close();
        }

        private List<Participant> participants = null;
        private int roomId;
        private BackgroundWorker participantUpdate = null;
        public RoomInfo(SockClient client, bool isAdmin)
        {
            InitializeComponent();
            this.client = client;
            this.thisWindow = (Window)this.FindName("this");
            this.closeButton = (Button)this.FindName("CloseButton");
            this.startButton = (Button)this.FindName("StartButton");
            this.leaveButton = (Button)this.FindName("LeaveButton");
            this.participantList = (ListBox)this.FindName("ParticipantList");
            this.maxTxt = (TextBlock)this.FindName("MaxTxt");
            this.timeText = (TextBlock)this.FindName("TimeTxt");
            this.numTxt = (TextBlock)this.FindName("NumTxt");
            this.nameTxt = (TextBlock)this.FindName("NameTxt");
            GetRoomStateResponse state = this.client.GetRoomState();
            this.numTxt.Text = state.questionCount.ToString();
            this.maxTxt.Text = state.maxPlayers.ToString();
            this.timeText.Text = state.answerTimeout.ToString();
            this.nameTxt.Text = state.name;
            this.roomId = state.id;
            if (isAdmin)
            {
                closeButton.Visibility = Visibility.Visible;
                startButton.Visibility = Visibility.Visible;
                leaveButton.Visibility = Visibility.Hidden;
                leaveButton.IsEnabled = false;
            }
            else
            {
                closeButton.Visibility = Visibility.Hidden;
                closeButton.IsEnabled = false;
                startButton.Visibility = Visibility.Hidden;
                startButton.IsEnabled = false;
                leaveButton.Visibility = Visibility.Visible;
            }
            this.participants = new List<Participant>();
            foreach (var player in this.client.GetRoomState().players)
            {
                participants.Add(new Participant(player));
            }
            this.participantList.ItemsSource = participants;
            this.participantUpdate = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            participantUpdate.DoWork += UpdateParticipants;
            participantUpdate.RunWorkerAsync();
        }
        private void UpdateParticipants(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (participantUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(3000);
                if (participantUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (!participantUpdate.CancellationPending)
                {
                    this.participants.Clear();
                    GetRoomStateResponse resp = this.client.GetRoomState();
                    if(resp.status == ResponseStatus.roomClosed)
                    {
                        this.participantUpdate.CancelAsync();
                        MessageBox.Show("Room Closed.");
                        this.thisWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                        {
                            new MainMenu(this.client).Show();
                            this.Close();
                        }));
                        e.Cancel = true;
                        break;
                    }
                    else if(resp.status == ResponseStatus.gameStarted)
                    {
                        this.participantUpdate.CancelAsync();
                        this.thisWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                        { 
                            new Game(this.client, int.Parse(this.numTxt.Text), int.Parse(this.timeText.Text)).Show();
                            this.Close();
                        }));
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (!participantUpdate.CancellationPending)
                        {

                            foreach (var player in resp.players)
                            {
                                participants.Add(new Participant(player));
                            }
                            this.participantList.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                            {
                                ParticipantList.ItemsSource = this.participants;
                                ParticipantList.Items.Refresh(); 
                            }));
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

    }
    public class Participant
    {
        public Participant(string name)
        {
            this.name = name;
        }
        public string name { get; set; }
    }
}
