using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagshiTriviaWPFClient
{

    public enum ResponseStatus : ushort
    {
        loginSuccess = 0,
        loginError,
        signUpSuccess,
        signUpError,
        logoutSuccess,
        logoutError,
        getRoomsSuccess,
        getRoomsError,
        joinRoomSuccess,
        joinRoomError,
        createRoomSuccess,
        createRoomError
    };

    public class Response
    {
        public Response(ResponseStatus status)
        {
            this.status = status;
        }
        public ResponseStatus status { get; set; }
    }
    public class StatisticsResponse : Response
    {
        public StatisticsResponse(ResponseStatus status, int numOfPlayerGames, int numOfTotalAnswers, int numOfCorrectAnswers, double averageTimeForAnswer) : base(status)
        {
            this.numOfPlayerGames = numOfPlayerGames;
            this.numOfTotalAnswers = numOfTotalAnswers;
            this.numOfCorrectAnswers = numOfCorrectAnswers;
            this.averageTimeForAnswer = averageTimeForAnswer;
        }

        public int numOfPlayerGames { get; set; }
        public int numOfTotalAnswers { get; set; }
        public int numOfCorrectAnswers { get; set; }
        public double averageTimeForAnswer { get; set; }
    }
}
