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
using System.Windows.Threading;

namespace MagshiTriviaWPFClient
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private SockClient client = null;
        public int questionCount = 0;
        public int timePerQuestion = 0;
        private TextBlock currQuestion = null;
        private TextBlock questionNum = null;
        private TextBlock timer = null;
        private TextBlock question = null;
        private Button op1 = null, op2 = null, op3 = null, op4 = null;
        private DispatcherTimer questionTimer = null;
        public int correctAnswers = 0;
        public int questionsAnswered = 0;
        public Game(SockClient client, int questionCount, int timePerQuestion)
        {
            InitializeComponent();
            this.client = client;
            this.questionCount = questionCount;
            this.timePerQuestion = timePerQuestion;
            this.questionNum = (TextBlock)this.FindName("QuestionNum");
            this.currQuestion = (TextBlock)this.FindName("CurrQuestion");
            this.timer = (TextBlock)this.FindName("Timer");
            this.question = (TextBlock)this.FindName("Question");
            this.op1 = (Button)this.FindName("Op1");
            this.op2 = (Button)this.FindName("Op2");
            this.op3 = (Button)this.FindName("Op3");
            this.op4 = (Button)this.FindName("Op4");
            this.questionNum.Text = questionCount.ToString();
            this.currQuestion.Text = "1";
            timer.Text = timePerQuestion.ToString();
            this.questionTimer = new DispatcherTimer();
            this.questionTimer.Tick += new EventHandler(TimerTick);
            this.questionTimer.Interval = new TimeSpan(0, 0, 1);
            var currQuestion = this.client.GetQuestion();
            this.question.Text = currQuestion.question;
            this.op1.Content = currQuestion.answers[0];
            this.op2.Content = currQuestion.answers[1];
            this.op3.Content = currQuestion.answers[2];
            this.op4.Content = currQuestion.answers[3];
            this.questionTimer.Start();
        }

        private void OnClickExit(object sender, RoutedEventArgs e)
        {
            this.questionTimer.Stop();
            this.client.LeaveGame();
            new MainMenu(this.client).Show();
            this.Close();
        }

        public void TimerTick(object Sender, EventArgs e) {
            int time = int.Parse(this.timer.Text);
            time--;
            if(time <= 0)
            {
                this.client.SubmitAnswer(new SubmitAnswerRequest(-1));
                this.questionsAnswered++;
                this.timer.Text = this.timePerQuestion.ToString();
                if(this.questionsAnswered == this.questionCount)
                {
                    new EndOfGame(this.client).Show();
                    this.Close();
                    this.questionTimer.Stop();
                }
                else
                {
                    GetNextQuestion();
                }
            }
            else
            {
                this.timer.Text = time.ToString();
            }
        }
        public void OnClickOption(object sender, EventArgs e)
        {
            int answerId = int.Parse((sender as Button).Uid);
            var res = this.client.SubmitAnswer(new SubmitAnswerRequest(answerId));
            this.questionsAnswered++;
            if(res.correctAnswerId == answerId)
            {
                correctAnswers++;
            }
            if(this.questionsAnswered == this.questionCount)
            {
                new EndOfGame(this.client).Show();
                this.questionTimer.Stop();
                this.Close();
            }
            else
            {
                GetNextQuestion();
            }
        }
        private void GetNextQuestion()
        {
            this.questionTimer.Stop();
            this.timer.Text = this.timePerQuestion.ToString();
            this.currQuestion.Text = (int.Parse(this.currQuestion.Text) + 1).ToString();
            var newQuestion = this.client.GetQuestion();
            this.question.Text = newQuestion.question;
            this.op1.Content = newQuestion.answers[0];
            this.op2.Content = newQuestion.answers[1];
            this.op3.Content = newQuestion.answers[2];
            this.op4.Content = newQuestion.answers[3];
            this.questionTimer.Start();
        }
    }
}
