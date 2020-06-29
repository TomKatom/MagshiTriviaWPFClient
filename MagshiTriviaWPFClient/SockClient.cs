using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Threading;

namespace MagshiTriviaWPFClient
{

    public class StateObject 
    {  
        public Socket clientSock = null;  
        public byte[] buffer = null;  
    }
    public class SockClient
    {
        private Socket clientSock;
        private ManualResetEvent receiveDone; 
        public SockClient()
        {
            try
            {
                this.clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.clientSock.BeginConnect(new IPEndPoint(IPAddress.Loopback, 5000), new AsyncCallback(ConnectCallBack), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        ~SockClient()
        {
            clientSock.Shutdown(SocketShutdown.Both);
            clientSock.Close();
        }
        private void ConnectCallBack(IAsyncResult AR)
        {
            try
            {
                this.clientSock.EndConnect(AR);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Send(byte[] message)
        {
            try
            {
                this.clientSock.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (SocketException)
            {
                MessageBox.Show("Connection To Server Lost.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void SendCallback(IAsyncResult AR)
        {
            try
            {
                this.clientSock.EndSend(AR);
            }
            catch (SocketException)
            {
                MessageBox.Show("Connection To Server Lost.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private byte[] Receive()
        {
            receiveDone = new ManualResetEvent(false);
            StateObject state = new StateObject();
            state.clientSock = this.clientSock;
            state.buffer = new byte[this.clientSock.ReceiveBufferSize];
            this.clientSock.BeginReceive(state.buffer, 0, state.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            receiveDone.WaitOne();
            return state.buffer;
        }
        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                this.clientSock.EndReceive(AR);
            }
            catch (SocketException)
            {
                MessageBox.Show("Connection To Server Lost.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            receiveDone.Set();
        }
        public void Register(RegisterRequest request)
        {
            this.Send(SerializeRequests.SerializeRegister(request));
        }
        public ResponseStatus Login(LoginRequest request)
        {
            this.Send(SerializeRequests.SerializeLogin(request));
            return SerializationAndDeserialization.DeserializeResponses.DeserializeResponse<Response>(this.Receive()).status;
        }
        public ResponseStatus CreateRoom(CreateRoomRequest request)
        {
            this.Send(SerializeRequests.SerializeRequest<CreateRoomRequest>(request, RequestCodes.createRoomRequest));
            return SerializationAndDeserialization.DeserializeResponses.DeserializeResponse<Response>(this.Receive()).status;
        }
        public StatisticsResponse GetStatistics()
        {
            this.Send(SerializeRequests.SerializeStatistics());
            return SerializationAndDeserialization.DeserializeResponses.DeserializeResponse<StatisticsResponse>(this.Receive());
        }
        public GetRoomsResponse GetRooms()
        {
            this.Send(SerializeRequests.SerializeGetRooms());
            return SerializationAndDeserialization.DeserializeResponses.DeserializeResponse<GetRoomsResponse>(this.Receive());
        }
        public GetPlayersResponse GetPlayers(GetPlayersRequest request)
        {
            this.Send(SerializeRequests.SerializeRequest<GetPlayersRequest>(request, RequestCodes.getPlayersInRoomRequest));
            return SerializationAndDeserialization.DeserializeResponses.DeserializeResponse<GetPlayersResponse>(this.Receive());
        }
        public ResponseStatus JoinRoom(JoinRoomRequest request)
        {
            this.Send(SerializeRequests.SerializeRequest<JoinRoomRequest>(request, RequestCodes.joinRoomRequest));
            return SerializationAndDeserialization.DeserializeResponses.DeserializeResponse<Response>(this.Receive()).status;
        }

    }
}
