using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private ApiService _usersService = new ApiService("users");
        public ObservableCollection<UsersResponse> UsersList { get; set; } = new ObservableCollection<UsersResponse>();
        public ICommand InitCommand { get; set; }

        public UsersViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        public async Task Init()
        {
            try
            {
                UsersList.Clear();
                var users = await _usersService.Get<IList<UsersResponse>>(null, "find");

                foreach (var item in users)
                {
                    UsersList.Add(item);
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task Filter(string Username)
        {
            try
            {
                var userSearch = await _usersService.Get<IList<UsersResponse>>(new { UserName = Username }, "find");
                UsersList.Clear();

                foreach (var item in userSearch)
                {
                    UsersList.Add(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
