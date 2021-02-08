using AMA.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
    }
}
