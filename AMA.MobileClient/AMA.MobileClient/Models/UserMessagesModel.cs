using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AMA.MobileClient.Models
{
    public class UserMessagesModel
    {
        public UsersResponse User { get; set; }
        public ObservableCollection<MessagesResponse> Messages { get; set; }
    }
}
