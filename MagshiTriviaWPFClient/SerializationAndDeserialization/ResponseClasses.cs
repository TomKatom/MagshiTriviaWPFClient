﻿using System;
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
        alreadyLoggedIn,
        signUpSuccess,
        signUpError,
        logoutSuccess,
        logoutError,
        getRoomsSuccess,
        getRoomsError,
        joinRoomSuccess,
        joinRoomError,
        createRoomSuccess,
        createRoomError,
        closeRoomSuccess,
        closeRoomError,
        startGameSuccess,
        startGameError,
        getRoomStateSuccess,
        getRoomStateError,
        leaveRoomSuccess,
        leaveRoomError,
        roomClosed,
        gameStarted,
        leaveGameSuccess,
        leaveGameError,
        getQuestionSuccess,
        getQuestionError,
        submitAnswerSuccess,
        submitAnswerError,
        getGameResultSuccess,
        getGameResultError,
        gameHasNotEnded
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
    public class GetRoomsResponse : Response
    {
        public GetRoomsResponse(ResponseStatus status, Room[] rooms) : base(status)
        {
            Rooms = rooms;
        }
        public Room[] Rooms { get; set; }
    }
    public class GetPlayersResponse : Response
    {
        public GetPlayersResponse(ResponseStatus status, string[] players) : base(status)
        {
            this.players = players;
        }

        public string[] players { get; set; }

    }
    public class GetRoomStateResponse : Response
    {
        public GetRoomStateResponse(ResponseStatus status, bool hasGameBegun, string[] players, int questionCount, int answerTimeout, int maxPlayers, string name, int id) : base(status)
        {
            this.hasGameBegun = hasGameBegun;
            this.players = players;
            this.questionCount = questionCount;
            this.answerTimeout = answerTimeout;
            this.maxPlayers = maxPlayers;
            this.name = name;
            this.id = id;
        }

        public bool hasGameBegun { get; set; }
        public string[] players { get; set; }
        public int questionCount { get; set; }
        public int answerTimeout { get; set; }
        public int maxPlayers { get; set; }
        public string name { get; set; }
        public int id { get; set; }

    }
    public class GetQuestionResponse : Response
    {
        public GetQuestionResponse(string question, string[] answers, ResponseStatus status) : base(status)
        {
            this.question = question;
            this.answers = answers;
        }
        public string question { get; set; }
        public string[] answers { get; set; }

    }
    public class SubmitAnswerResponse : Response
    {
        public SubmitAnswerResponse(int correctAnswerId, ResponseStatus status) : base(status)
        {
            this.correctAnswerId = correctAnswerId;
        }

        public int correctAnswerId { get; set; }
    }
    public class GetGameResultsResponse : Response
    {
        public GetGameResultsResponse(PlayerResults[] results, ResponseStatus status) : base(status)
        {
            this.results = results;
        }

        public PlayerResults[] results{ get; set; }
    }
    public class GetLeaderboardResponse : Response
    {
        public GetLeaderboardResponse(LeaderboardEntry[] players, ResponseStatus status) : base(status)
        {
            this.players = players;
        }
        public LeaderboardEntry[] players { get; set; }
    }
}
