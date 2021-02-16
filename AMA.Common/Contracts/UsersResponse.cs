using AMA.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class UsersResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Mail { get; set; }
        public CityResponse City { get; set; }
        public UserStatus Status { get; set; }
    }
}
