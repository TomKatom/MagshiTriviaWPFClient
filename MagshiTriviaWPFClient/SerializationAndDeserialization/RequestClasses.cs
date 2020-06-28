namespace MagshiTriviaWPFClient
{
    enum RequestCodes : ushort
    {
        loginRequestCode = 20,
        signupRequestCode,
        createRoomRequest,
        getRoomsRequest,
        getPlayersInRoomRequest,
        joinRoomRequest,
        getStatisticsRequest,
        logoutRequest,
        closeRoomRequest,
        startGameRequest,
        getRoomStateRequest,
        leaveRoomRequest
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
        public CreateRoomRequest(string roomName, int numberOfPlayers, int numberOfQuestions, double timePerQuestion)
        {
            this.roomName = roomName;
            this.maxUsers = numberOfPlayers;
            this.questionCount = numberOfQuestions;
            this.answerTimeout = timePerQuestion;
        }
        public string roomName { get; set; }
        public int maxUsers { get; set; }
        public int questionCount { get; set; }
        public double answerTimeout { get; set; }
    }
    public class StatisticsRequest
    {
        public StatisticsRequest(int status)
        {
            this.status = status;
        }

        public int status { get; set; }
    }
}
