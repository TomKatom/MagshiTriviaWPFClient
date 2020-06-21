using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagshiTriviaWPFClient
{

    enum ResponseCodes
    {
        LoginSuccess = 0,
        LoignError
    }

    public class LoginResponse
    {
        public LoginResponse(ushort status)
        {
            this.status = status;
        }
        public ushort status { get; set; }
    }
}
