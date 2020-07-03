using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
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
    /// Interaction logic for JoinRoom.xaml
    /// </summary>
    public partial class JoinRoom : Window
    {
        SockClient client = null;
        ListBox roomList = null;
        BackgroundWorker roomUpdater = null;
        List<RoomData> roomLst = null;
        private TextBlock empty = null;
        public JoinRoom(SockClient client)
        {
            InitializeComponent();
            empty = (TextBlock)this.FindName("EmptyTxt");
            roomList = (ListBox)this.FindName("RoomList");
            this.client = client;
            Room[] rooms = this.client.GetRooms().Rooms;
            roomLst = new List<RoomData>();
            if(rooms.Length < 1)
            {
                ((TextBlock)this.FindName("EmptyTxt")).Visibility = Visibility.Visible;
            }
            for (int i = 0; i < rooms.Length; i++)
            {
                int playersInRoom = this.client.GetPlayers(new GetPlayersRequest(rooms[i].id)).players.Length;
                roomLst.Add(new RoomData(rooms[i].name + " ", playersInRoom, rooms[i].maxPlayers, rooms[i].id, rooms[i].timePerQuestion, rooms[i].questionsCount));
            }
            roomList.ItemsSource = roomLst;
            roomUpdater = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            roomUpdater.DoWork += UpdateRoomList;
            roomUpdater.RunWorkerAsync();
        }

        private void OnClickJoin(object sender, RoutedEventArgs e)
        {
            bool picked = true;
            int chosenRoomId = 0;
            try
            {
                chosenRoomId = (roomList.SelectedItem as RoomData).id;
            }
            catch
            {
                picked = false;
                ((Border)this.FindName("border")).Visibility = Visibility.Visible;
            }
            if (picked)
            {
                ((Border)this.FindName("border")).Visibility = Visibility.Hidden;
            }
            RoomData room = (RoomData)roomList.SelectedItem;
            if(picked && this.client.JoinRoom(new JoinRoomRequest(chosenRoomId)) != ResponseStatus.joinRoomSuccess)
            {
                MessageBox.Show("Error Joining Room");
            }
            else if(picked)
            {
                new RoomInfo(this.client, false).Show();
                this.roomUpdater.CancelAsync(); 
                this.Close();
            }
        }
        private void UpdateRoomList(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (roomUpdater.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(3000);
                if (roomUpdater.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (!roomUpdater.CancellationPending)
                    {

                        Room[] rooms = this.client.GetRooms().Rooms;
                        if(rooms.Length < 1)
                        {
                            empty.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                            {
                                EmptyTxt.Visibility = Visibility.Visible;
                            }));
                        }
                        else
                        {
    
                            empty.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                            {
                                EmptyTxt.Visibility = Visibility.Hidden;
                            }));
                        }
                        this.roomLst.Clear();
                        for (int i = 0; i < rooms.Length; i++)
                        {
                            int playersInRoom = this.client.GetPlayers(new GetPlayersRequest(rooms[i].id)).players.Length;
                        this.roomLst.Add(new RoomData(rooms[i].name + " ", playersInRoom, rooms[i].maxPlayers, rooms[i].id, rooms[i].timePerQuestion, rooms[i].questionsCount));
                        }
                        this.roomList.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate ()
                        {
                            RoomList.ItemsSource = this.roomLst;
                            RoomList.Items.Refresh(); 
                        }));
                    }
                }
            }
        }
    }
    public class RoomData
    {
        public RoomData(string roomName, int playerCount, int maxPlayers, int id, int timePerQuestion, int questionsCount)
        {
            this.RoomName = roomName;
            this.PlayerCount = playerCount;
            this.MaxPlayers = maxPlayers;
            this.id = id;
            this.timerPerQuestion = timerPerQuestion;
            this.questionsCount = questionsCount;
        }

        public int id { get; set; }
        public string RoomName { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayers { get; set; }
        public int timerPerQuestion { get; set; }
        public int questionsCount { get; set; }
        
    }
    public class Room
    {
        public int id { get; set; }
        public string name { get; set; }
        public int maxPlayers { get; set; }
        public int timePerQuestion { get; set; }
        public int questionsCount { get; set; }

    }
                
}
