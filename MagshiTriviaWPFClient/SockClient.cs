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
            this.Send(SerializeRequest.SerializeRegister(request));
        }
        public ushort Login(LoginRequest request)
        {
            this.Send(SerializeRequest.SerializeLogin(request));
            return SerializationAndDeserialization.DeserializeResponse.DeserializeLogin(this.Receive()).status;
        }
    }
}
