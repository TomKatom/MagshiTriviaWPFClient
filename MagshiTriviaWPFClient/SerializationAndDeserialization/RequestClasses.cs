using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagshiTriviaWPFClient
{
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
}
