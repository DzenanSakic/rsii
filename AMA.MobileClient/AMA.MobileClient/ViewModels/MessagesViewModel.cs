using AMA.Common.Contracts;
using AMA.MobileClient.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        private readonly ApiService _usersService = new ApiService("users");

        public ObservableCollection<UserMessagesModel> UserMessages { get; set; } = new ObservableCollection<UserMessagesModel>();
        public IList<MessagesResponse> AllMessages { get; set; }

        ICommand InitCommand;
        public MessagesViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public async Task Init()
        {
            try
            {
                UserMessages.Clear();


                AllMessages = await _usersService.Get<IList<MessagesResponse>>(null, $"user/messages");
                var allSent = AllMessages.Where(x => x.ToUserId != ApiService.UserId).Select(a => a.ToUserId).ToList();
                var allReceived = AllMessages.Where(x => x.FromUserId != ApiService.UserId).Select(a => a.FromUserId).ToList();
                allSent.AddRange(allReceived);
                var all = allSent.Distinct().ToList();

                foreach (var userId in all)
                {
                    var help = new ObservableCollection<MessagesResponse>();
                    foreach (var item in AllMessages.Where(x => x.ToUserId == userId || x.FromUserId == userId))
                    {
                        item.AlignRight = item.FromUserId == ApiService.UserId ? true : false;
                        item.AlignLeft = item.FromUserId != ApiService.UserId ? true : false;
                        help.Add(item);
                    }
                    UserMessages.Add(new UserMessagesModel
                    {
                        Messages = help,
                        User = await _usersService.Get<UsersResponse>(null, $"user/{userId}")
                        
                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
