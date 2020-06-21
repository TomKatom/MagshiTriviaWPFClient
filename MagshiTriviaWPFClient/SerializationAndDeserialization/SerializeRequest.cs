using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MagshiTriviaWPFClient
{
    static class SerializeRequest
    {
        public static byte[] SerializeRegister(RegisterRequest request)
        {
            string data = JsonConvert.SerializeObject(request);
            ushort code = 21;
            uint dataLength = (uint)data.Length;
            byte[] serialized = new byte[1 + 4 + dataLength];
            serialized[0] = BitConverter.GetBytes(code)[0];
            for(int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i-1];
            }
            for(int i = 5, j = 0; i < serialized.Length; i++, j++)
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
            for(int i = 1; i < 5; i++)
            {
                serialized[i] = BitConverter.GetBytes(dataLength)[i-1];
            }
            for(int i = 5, j = 0; i < serialized.Length; i++, j++)
            {
                serialized[i] = ASCIIEncoding.ASCII.GetBytes(data)[j];
            }
            return serialized;
        }
    }
}
