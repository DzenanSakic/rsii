using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        private ApiService _usersService = new ApiService("users");
        private ApiService _countriesService = new ApiService("countries");
        private ApiService _citiesService = new ApiService("cities");
        public EditUserViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }

        private ApiService _userService = new ApiService("users");
        public ICommand InitCommand { get; set; }


        private ObservableCollection<CountryResponse> _countries = new ObservableCollection<CountryResponse>();
        public ObservableCollection<CountryResponse> Countries
        {
            get { return _countries; }
            set
            {
                if (_countries != value)
                {
                    _countries = value;
                    SetProperty(ref _countries, value);
                }
            }
        }
        public ObservableCollection<CityResponse> Cities { get; set; } = new ObservableCollection<CityResponse>();
        public IList<CityResponse> AllCities { get; set; }
        private CountryResponse _country;
        private CityResponse _city;
        public CountryResponse SelectedCountry
        {
            get
            {
                return _country;
            }
            set
            {
                SetProperty(ref _country, value);
            }
        }
        public CityResponse SelectedCity
        {
            get
            {
                return _city;
            }
            set
            {
                SetProperty(ref _city, value);
            }
        }


        #region Properties
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
            }
        }

        DateTime _birthdate = DateTime.UtcNow;
        public DateTime BirthDate
        {
            get { return _birthdate; }
            set { SetProperty(ref _birthdate, value); }
        }

        string _gender;
        public string Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private string _username;
        public string UserName
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                SetProperty(ref _firstName, value);
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        public string Role { get; set; }

        #endregion

        public async Task Init()
        {
            var countries = await _countriesService.Get<List<CountryResponse>>(null, "all");
            AllCities = await _citiesService.Get<IList<CityResponse>>(null, "all");

            Countries.Clear();
            Cities.Clear();

            foreach (var item in countries)
            {
                Countries.Add(item);
            }
            foreach (var item in AllCities)
            {
                Cities.Add(item);
            }

            var user = await _userService.Get<UsersResponse>(null, $"user/{ApiService.UserId}");
            UserName = user.UserName;
            Email = user.Mail;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Gender = user.Gender.ToString();
            var city = AllCities.Where(x => x.Id == user.City.Id).FirstOrDefault();
            SelectedCountry = Countries.Where(x => x.ID == city.Country.ID).FirstOrDefault();
            Role = ApiService.Permission;
            SelectedCity = city;
        }

        public void FilterCities()
        {
            if(SelectedCountry != null)
            {
                var countryId = SelectedCountry.ID;
                var cities = AllCities.Where(a => a.Country.ID == countryId).ToList();
                Cities.Clear();

                foreach (var item in cities)
                {
                    Cities.Add(item);
                };
            }
        }


        public async Task Save()
        {
            try
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName)
                || string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
                {
                    throw new Exception("Username, First name, Last name, User name, Mail can not be empty!");
                }

                if (!string.IsNullOrEmpty(FirstName) && !Regex.IsMatch(FirstName, "^[a-zA-Z]+$"))
                {
                    throw new Exception("First name can only contain letters!");
                }

                if (!string.IsNullOrEmpty(LastName) && !Regex.IsMatch(LastName, "^[a-zA-Z ]+$"))
                {
                    throw new Exception("Last name can only contain letters!");
                }

                int gender;
                if (Gender == "Other")
                    gender = 0;
                else if (Gender == "Male")
                    gender = 1;
                else gender = 2;

                var editRequest = new
                {
                    UserId = ApiService.UserId,
                    FirstName = FirstName,
                    LastName = LastName,
                    Mail = Email,
                    BirthDate = BirthDate,
                    CityId = SelectedCity.Id,
                    Gender = gender
                };

                _ = await _userService.Post<object>(editRequest, "user/edit");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
