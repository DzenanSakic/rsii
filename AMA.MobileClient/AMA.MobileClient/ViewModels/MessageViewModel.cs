using AMA.Common.Contracts;
using System.Threading.Tasks;

namespace AMA.MobileClient.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private ApiService _usersService = new ApiService("users");
        public UsersResponse User;
        public MessageViewModel(UsersResponse user)
        {
            User = user;
        }

        public string UserName
        {
            get { return User.UserName; }
        }

        string _title = string.Empty;
        public new string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        string _body = string.Empty;
        public string Body
        {
            get { return _body; }
            set { SetProperty(ref _body, value); }
        }

        internal async Task SendMessage(object request)
        {
            _ = await _usersService.Post<object>(request,"user/message");
        }
    }
}
