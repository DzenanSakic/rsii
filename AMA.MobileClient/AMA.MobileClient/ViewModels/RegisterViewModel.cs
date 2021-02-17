using AMA.Common.Contracts;
using AMA.Common.Enumerations;
using AMA.MobileClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private ApiService _usersService = new ApiService("users");
        private ApiService _countriesService = new ApiService("countries");
        private ApiService _citiesService = new ApiService("cities");

        public RegisterViewModel()
        {
            Submit = new Command(async () => await Register());
            InitCommand = new Command(async () => await Init());
        }

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

        public async Task Init()
        {
            try
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
            }
            catch
            {
                throw;
            }
        }


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

        public void FilterCities()
        {
            var countryId = SelectedCountry.ID;
            var cities = AllCities.Where(a => a.Country.ID == countryId).ToList();
            Cities.Clear();

            foreach (var item in cities)
            {
                Cities.Add(item);
            };
        }


        string _username = string.Empty;
        public string UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }


        string _firstname = string.Empty;
        public string FirstName
        {
            get { return _firstname; }
            set { SetProperty(ref _firstname, value); }
        }

        string _lastname = string.Empty;
        public string LastName
        {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }

        string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
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

        public ICommand Submit { get; set; }
        public ICommand InitCommand { get; set; }

        async Task Register()
        {
            try
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName)
                       || string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName)
                       || string.IsNullOrEmpty(Gender)
                       || string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email)
                       || string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName)
                       || string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password)
                       || string.IsNullOrEmpty(ConfirmPassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    throw new Exception("All fields are required!");
                }

                if (ConfirmPassword != Password)
                {
                    throw new Exception("Passwords do not match.");
                }

                if (!string.IsNullOrEmpty(FirstName) && !Regex.IsMatch(FirstName, "^[a-zA-Z]+$"))
                {
                    throw new Exception("First name can only contain letters!");
                }
                
                if (!string.IsNullOrEmpty(LastName) && !Regex.IsMatch(LastName, "^[a-zA-Z]+$"))
                {
                    throw new Exception("First name can only contain letters!");
                }


                if (!string.IsNullOrEmpty(UserName) && !Regex.IsMatch(UserName, "^[a-zA-Z0-9]+$"))
                {
                    throw new Exception("Username can only contain letters and nubmers!");
                }

                int gender;
                if (Gender == "Other")
                    gender = 0;
                else if (Gender == "Male")
                    gender = 1;
                else gender = 2;

                var request = new
                {
                    Mail = Email,
                    FirstName = FirstName,
                    Password = Password,
                    LastName = LastName,
                    UserName = UserName,
                    BirthDate = BirthDate,
                    CityId = SelectedCity.Id,
                    Gender = gender
                };

                await _usersService.Post<object>(request, "user/register");

                Application.Current.MainPage = new LoginPage();
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
