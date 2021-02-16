using AMA.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMA.AdminClient.Views
{
    public class UsersDataGrid
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; }
    }
}
