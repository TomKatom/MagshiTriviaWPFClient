using Newtonsoft.Json;
using System;
using System.Text;

namespace MagshiTriviaWPFClient
{
    static class SerializeRequests
    {
        public static byte[] SerializeRegister(RegisterRequest request)
        {
            string data = JsonConvert.SerializeObject(request);
            ushort code = 21;
            uint dataLength = (uint)data.Length;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes(code)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            for (int i = 5, j = 0; i < serialized.Length; i++, j++)
            {
                serialized[i] = ASCIIEncoding.ASCII.GetBytes(data)[j];
            }
            return serialized;
        }
        public static byte[] SerializeLogin(LoginRequest request)
        {
            string data = JsonConvert.SerializeObject(request);
            ushort code = 20;
            uint dataLength = (uint)data.Length;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes(code)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            for (int i = 5, j = 0; i < serialized.Length; i++, j++)
            {
                serialized[i] = ASCIIEncoding.ASCII.GetBytes(data)[j];
            }
            return serialized;
        }
        public static byte[] SerializeRequest<T>(T request, RequestCodes code)
        {
            string data = JsonConvert.SerializeObject(request);
            uint dataLength = (uint)data.Length;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)code)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            for (int i = 5, j = 0; i < serialized.Length; i++, j++)
            {
                serialized[i] = ASCIIEncoding.ASCII.GetBytes(data)[j];
            }
            return serialized;
        }
        public static byte[] SerializeStatistics()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.getStatisticsRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeGetRooms()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.getRoomsRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeLogout()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.logoutRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeGetRoomState()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.getRoomStateRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeLeaveRoom()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.leaveRoomRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeCloseRoom()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.closeRoomRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeStartGame()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.startGameRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeGetQuestion()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.getQuestionRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeGetGameResults()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.getGameResultRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeGetLeaderboard()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.getLeaderboardRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
        public static byte[] SerializeLeaveGame()
        {
            uint dataLength = (uint)0;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes((ushort)RequestCodes.leaveGameRequest)[0];
            for (int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i - 1];
            }
            return serialized;
        }
    }
}
