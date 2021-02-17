using AMA.Common.Contracts;
using AMA.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmUser : Form
    {
        private ApiService _userService = new ApiService("users/user");
        private ApiService _citiesService = new ApiService("cities");

        private readonly int _userId;
        private Gender _gender;
        public FrmUser(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private async void FrmUser_Load(object sender, EventArgs e)
        {
            await LoadUser();
        }

        private async Task LoadUser()
        {
            try
            {
                var data = (await _userService.GetById<UsersResponse>(_userId));
                _gender = data.Gender;

                var cities = (await _citiesService.Get<List<CityResponse>>(null, "all")).ToArray();
                comboBox1.DataSource = cities;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";

                dateTimePicker1.Value = data.BirthDate.Date;
                comboBox1.SelectedValue = data.City.Id;
                txtFirstName.Text = data.FirstName;
                txtLastName.Text = data.LastName;
                txtMail.Text = data.Mail;
                txtUserName.Text = data.UserName;
                txtGender.Text = data.Gender.ToString();

                if(_userId == ApiService.UserId)
                {
                    txtUserName.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrWhiteSpace(txtUserName.Text)
                || string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text)
                || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text)
                || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrWhiteSpace(txtMail.Text))
                {
                    MessageBox.Show("First name, Last name, User name, Mail can not be empty!");
                    return;
                }

                if (!string.IsNullOrEmpty(txtFirstName.Text) && !Regex.IsMatch(txtFirstName.Text, "^[a-zA-Z]+$"))
                {
                    MessageBox.Show("First name can only contain letters!");
                    return;
                }

                if (!string.IsNullOrEmpty(txtLastName.Text) && !Regex.IsMatch(txtLastName.Text, "^[a-zA-Z ]+$"))
                {
                    MessageBox.Show("Last name can only contain letters!");
                    return;
                }

                var editRequest = new
                {
                    UserId = _userId,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Mail = txtMail.Text,
                    BirthDate = dateTimePicker1.Value.Date,
                    CityId = comboBox1.SelectedValue,
                    Gender = _gender
                };

                _ = await _userService.Post<object>(editRequest, "edit");

                MessageBox.Show("User edited!");

                await LoadUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
