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
        public JoinRoom(SockClient client)
        {
            InitializeComponent();
            roomList = (ListBox)this.FindName("RoomList");
            this.client = client;
            Room[] rooms = this.client.GetRooms().Rooms;
            roomLst = new List<RoomData>();
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
            int chosenRoomId = (roomList.SelectedItem as RoomData).id;
            RoomData room = (RoomData)roomList.SelectedItem;
            if(this.client.JoinRoom(new JoinRoomRequest(chosenRoomId)) != ResponseStatus.joinRoomSuccess)
            {
                MessageBox.Show("Error Joining Room");
            }
            else
            {
                new RoomInfo(this.client, false, chosenRoomId, room.timerPerQuestion, room.questionsCount, room.MaxPlayers, room.RoomName).Show();
                this.Close();
            }
        }
        private void UpdateRoomList(object sender, DoWorkEventArgs e)
        {
            while (true)
            {

                Thread.Sleep(3000);
                Room[] rooms = this.client.GetRooms().Rooms;
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
