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
            TextBlock errTxt = (TextBlock)this.FindName("ErrTxt");
            int result = 0;
            bool validForm = true;
            if(roomName.Text.Length <= 0)
            {
                validForm = false;
                errTxt.Text = "Room Name Cant Be Empty.";
            }
            else if(!int.TryParse(maxPlayers.Text, out result))
            {
                validForm = false;
                errTxt.Text = "Max Players Has To Be A Valid Number";
            }
            else if(!int.TryParse(questionCount.Text, out result))
            {
                validForm = false;
                errTxt.Text = "Question Count Has To Be A Valid Number";
            }
            else if(!int.TryParse(answerTimeout.Text, out result))
            {
                validForm = false;
                errTxt.Text = "Answer Timeout Has To Be A Valid Number";
            }
            if (validForm)
            {
                ((Border)this.FindName("border")).Visibility = Visibility.Hidden;
            }
            else
            {

                ((Border)this.FindName("border")).Visibility = Visibility.Visible;
            }
            if(validForm && this.client.CreateRoom(new CreateRoomRequest(roomName.Text, int.Parse(MaxPlayers.Text), int.Parse(questionCount.Text), int.Parse(answerTimeout.Text))) != ResponseStatus.createRoomSuccess) // fix their shitty design
            {
                MessageBox.Show("Cannot Create Room.");
            }
            else if(validForm)
            {

                new RoomInfo(this.client, true).Show();
                this.Close();
            }
        }
    }
}
