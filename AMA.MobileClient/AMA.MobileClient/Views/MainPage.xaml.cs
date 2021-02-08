using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AMA.MobileClient.Models;

namespace AMA.MobileClient.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Questions, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            var Id = (MenuItemType)id;

            if (!MenuPages.ContainsKey(id))
            {
                switch (Id)
                {
                    //case MenuItemType.RecommendedQuestions:
                    //    MenuPages.Add(id, new NavigationPage(new RecommendedPostsPage()));
                    //    break;
                    case MenuItemType.Questions:
                        MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case MenuItemType.Users:
                        MenuPages.Add(id, new NavigationPage(new UsersPage()));
                        break;
                    case MenuItemType.EditProfile:
                        MenuPages.Add(id, new NavigationPage(new EditUserPage()));
                        break;
                    case MenuItemType.Logout:
                        ApiService.Token = null;
                        ApiService.UserId = -1;
                        ApiService.Permission = "";
                        break;
                    case MenuItemType.Messages:
                        MenuPages.Add(id, new NavigationPage(new MessagesPage()));
                        break;
                }
            }

            if(id == 4) // logout
                Application.Current.MainPage = new LoginPage();

            else
            {
                var newPage = MenuPages[id];

                if (newPage != null && Detail != newPage)
                {
                    Detail = newPage;

                    if (Device.RuntimePlatform == Device.Android)
                        await Task.Delay(100);

                    IsPresented = false;
                }
            }
        }
    }
}