using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for PersonalStatistics.xaml
    /// </summary>
    public partial class PersonalStatistics : Window, INotifyPropertyChanged
    {
        private SockClient client = null;
        private int _gameCount;
        public int gameCount
        {
            get { return _gameCount; }
            set
            {
                _gameCount = value;
                OnPropertyChanged();
            }
        }
        private int _correctAnswerCount;
        public int correctAnswersCount
        {
            get { return _correctAnswerCount; }
            set
            {
                _correctAnswerCount = value;
                OnPropertyChanged();
            }
        }
        private int _totalAnswersCount;
        public int totalAnswersCount
        {
            get { return _totalAnswersCount; }
            set
            {
                _totalAnswersCount = value;
                OnPropertyChanged();
            }
        }
        private double _averageTimeForAnswer;
        public double averageTimeForAnswer 
        {
            get { return _averageTimeForAnswer; }
            set
            {
                _averageTimeForAnswer = value;
                OnPropertyChanged();
            }
        }
        private StatisticsResponse response;

        public PersonalStatistics(SockClient client)
        {
            InitializeComponent();
            DataContext = this;
            this.client = client;
            this.response = this.client.GetStatistics();
            gameCount = this.response.numOfPlayerGames;
            correctAnswersCount = this.response.numOfCorrectAnswers;
            totalAnswersCount = this.response.numOfTotalAnswers;
            averageTimeForAnswer = this.response.averageTimeForAnswer;
        }    
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        private void OnClickBack(object sender, RoutedEventArgs e)
        {
            new MainMenu(this.client).Show();
            this.Close();
        }
    }
}
