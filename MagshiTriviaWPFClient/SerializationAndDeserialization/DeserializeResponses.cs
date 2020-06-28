using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MagshiTriviaWPFClient.SerializationAndDeserialization
{
    public static class DeserializeResponses
    {
        public static T DeserializeResponse<T>(byte[] data)
        {
           
            int dataLength = ((data[1] << 8 | data[2]) << 8 | data[3]) << 8 | data[4];
            string jsonData = Encoding.ASCII.GetString(data, 5, dataLength);
            T result = JsonConvert.DeserializeObject<T>(jsonData);
            return result;
        }
    }
}
