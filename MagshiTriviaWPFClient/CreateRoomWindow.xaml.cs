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
    /// Interaction logic for CreateRoomWindow.xaml
    /// </summary>
    public partial class CreateRoomWindow : Window
    {
        private SockClient client = null;
        public CreateRoomWindow(SockClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void OnClickCreate(object sender, RoutedEventArgs e)
        {
            TextBox roomName = (TextBox)this.FindName("RoomName");
            TextBox maxPlayers = (TextBox)this.FindName("MaxPlayers");
            TextBox questionCount = (TextBox)this.FindName("QuestionCount");
            TextBox answerTimeout = (TextBox)this.FindName("AnswerTimeout");
            if(this.client.CreateRoom(new CreateRoomRequest(roomName.Text, int.Parse(MaxPlayers.Text), int.Parse(questionCount.Text), double.Parse(answerTimeout.Text))) != ResponseStatus.createRoomSuccess)
            {
                MessageBox.Show("Cannot Create Room.");
            }
            else
            {
                // Move to room info window
            }
        }
    }
}
