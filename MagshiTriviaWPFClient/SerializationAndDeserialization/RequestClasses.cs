namespace MagshiTriviaWPFClient
{
    public enum RequestCodes : ushort
    {
        loginRequestCode = 20,
        signupRequestCode,
        createRoomRequest,
        getRoomsRequest,
        getPlayersInRoomRequest,
        joinRoomRequest,
        getStatisticsRequest, 
        getLeaderboardRequest,
        logoutRequest,
        closeRoomRequest,
        startGameRequest,
        getRoomStateRequest,
        leaveRoomRequest,
        leaveGameRequest,
        getQuestionRequest,
        submitAnswerRequest,
        getGameResultRequest
    };

    public class RegisterRequest
    {
        public RegisterRequest(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    public class LoginRequest
    {
        public LoginRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class CreateRoomRequest
    {
        public CreateRoomRequest(string roomName, int numberOfPlayers, int numberOfQuestions, int timePerQuestion)
        {
            this.roomName = roomName;
            this.maxUsers = numberOfPlayers;
            this.questionCount = numberOfQuestions;
            this.answerTimeout = timePerQuestion;
        }
        public string roomName { get; set; }
        public int maxUsers { get; set; }
        public int questionCount { get; set; }
        public int answerTimeout { get; set; }
    }
    public class StatisticsRequest
    {
        public StatisticsRequest(int status)
        {
            this.status = status;
        }

        public int status { get; set; }
    }
    public class GetPlayersRequest
    {
        public GetPlayersRequest(int roomId)
        {
            this.roomId = roomId;
        }
        public int roomId { get; set; }
    }
    public class JoinRoomRequest
    {
        public JoinRoomRequest(int roomId)
        {
            this.roomId = roomId;
        }
        public int roomId { get; set; }
    }
    public class SubmitAnswerRequest
    {
        public SubmitAnswerRequest(int answerId)
        {
            this.answerId = answerId;
        }
        public int answerId { get; set; }
    }
}
