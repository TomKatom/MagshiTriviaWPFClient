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
        private List<Participant> participants = null;
        private int roomId;
        public RoomInfo(SockClient client, bool isAdmin, int roomId, int timePerQuestion, int questionsCount, int maxPlayers, string name)
        {
            InitializeComponent();
            this.client = client;
            this.closeButton = (Button)this.FindName("CloseButton");
            this.startButton = (Button)this.FindName("StartButton");
            this.leaveButton = (Button)this.FindName("LeaveButton");
            this.participantList = (ListBox)this.FindName("ParticipantList");
            this.maxTxt = (TextBlock)this.FindName("MaxTxt");
            this.timeText = (TextBlock)this.FindName("TimeTxt");
            this.numTxt = (TextBlock)this.FindName("NumTxt");
            this.nameTxt = (TextBlock)this.FindName("NameTxt");
            this.numTxt.Text = questionsCount.ToString();
            this.maxTxt.Text = maxPlayers.ToString();
            this.timeText.Text = timePerQuestion.ToString();
            this.nameTxt.Text = name;
            this.roomId = roomId;
            if (isAdmin)
            {
                closeButton.Visibility = Visibility.Visible;
                startButton.Visibility = Visibility.Visible;
                leaveButton.Visibility = Visibility.Hidden;
            }
            else
            {
                closeButton.Visibility = Visibility.Hidden;
                startButton.Visibility = Visibility.Hidden;
                leaveButton.Visibility = Visibility.Visible;
            }
            this.participants = new List<Participant>();
            foreach (var player in this.client.GetPlayers(new GetPlayersRequest(roomId)).players)
            {
                participants.Add(new Participant(player));
            }
            this.participantList.ItemsSource = participants;
            BackgroundWorker participantUpdate = new BackgroundWorker
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
                Thread.Sleep(3000);
                this.participants.Clear();
                foreach (var player in this.client.GetPlayers(new GetPlayersRequest(roomId)).players)
                {
                    participants.Add(new Participant(player));
                }
                this.participantList.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                {
                    ParticipantList.ItemsSource = this.participants;
                    ParticipantList.Items.Refresh(); 
                }));

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
