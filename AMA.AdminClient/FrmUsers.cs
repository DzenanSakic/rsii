using AMA.AdminClient.Views;
using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmUsers : Form
    {
        private ApiService _usersService = new ApiService("users");
        private ApiService _userRoleService = new ApiService("users/user/role");
        public FrmUsers()
        {
            InitializeComponent();
        }

        private async void FrmUsers_Load(object sender, EventArgs e)
        {
            var data = (await _usersService.Get<List<UsersResponse>>(null, "find"))
                .Select(x => new UsersDataGrid
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Mail = x.Mail,
                    City = x.City.Name,
                    Gender = x.Gender,
                    Status = x.Status
                }).ToList();

            await LoadUsers(data);
            dataGridView1.MultiSelect = false;
        }

        private async Task LoadUsers(List<UsersDataGrid> data)
        {
            foreach (var user in data)
            {
                var role = await _userRoleService.Get<RoleResponse>(null, $"{user.Id}");
                user.Role = role.Role.ToString();
            }

            dataGridView1.DataSource = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.SelectedCells[0].RowIndex;
            var user = dataGridView1.Rows[index].DataBoundItem as UsersDataGrid;

            FrmUser frm = new FrmUser(user.Id);
            frm.FormClosed += frmUser_Clsoed;
            frm.Show();
        }

        private async void frmUser_Clsoed(object sender, FormClosedEventArgs e)
        {
            var data = (await _usersService.Get<List<UsersResponse>>(null, "find"))
                .Select(x => new UsersDataGrid
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Mail = x.Mail,
                    City = x.City.Name,
                    Gender = x.Gender
                }).ToList();

            await LoadUsers(data);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.SelectedCells[0].RowIndex;
            var user = dataGridView1.Rows[index].DataBoundItem as UsersDataGrid;

            if (user.Id == ApiService.UserId)
            {
                MessageBox.Show("You can not ban yourself!");
                return;
            }

            if(user.Role == "Admin")
            {
                MessageBox.Show("You can not ban admin!");
                return;
            }

            FrmBans frm = new FrmBans(user.Id, user.UserName);
            frm.Show();
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.SelectedCells[0].RowIndex;
            var user = dataGridView1.Rows[index].DataBoundItem as UsersDataGrid;

            if (user.Id == ApiService.UserId)
            {
                MessageBox.Show("You can not send message to yourself!");
                return;
            }

            FrmMsg frm = new FrmMsg(user.Id, user.UserName);
            frm.Show();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text) && !Regex.IsMatch(txtId.Text, "^[0-9]+$"))
            {
                MessageBox.Show("ID can only be number!");
                return;
            }

            if (!string.IsNullOrEmpty(txtFirstname.Text) && !Regex.IsMatch(txtFirstname.Text, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("First name can only contain letters!");
                return;
            }

            if (!string.IsNullOrEmpty(txtLastname.Text) && !Regex.IsMatch(txtLastname.Text, "^[a-zA-Z ]+$"))
            {
                MessageBox.Show("Last name can only contain letters!");
                return;
            }

            if (!string.IsNullOrEmpty(txtUsername.Text) && !Regex.IsMatch(txtUsername.Text, "^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Username can only contain letters and nubmers!");
                return;
            }

            var request = new
            {
                Id = txtId.Text,
                FirstName = txtFirstname.Text,
                LastName = txtLastname.Text,
                UserName = txtUsername.Text
            };

            var data = (await _usersService.Get<List<UsersResponse>>(request, "find"))
                .Select(x => new UsersDataGrid
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Mail = x.Mail,
                    City = x.City.Name,
                    Gender = x.Gender,
                    Status = x.Status
                }).ToList();

            await LoadUsers(data);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.SelectedCells[0].RowIndex;
            var user = dataGridView1.Rows[index].DataBoundItem as UsersDataGrid;

            if (user.Id == ApiService.UserId)
            {
                MessageBox.Show("You can not block yourself!");
                return;
            }

            if (user.Role == "Admin")
            {
                MessageBox.Show("You can not block admin!");
                return;
            }

            _ = await _usersService.Post<object>(null, $"user/{user.Id}/changestate");

            var data = (await _usersService.Get<List<UsersResponse>>(null, "find"))
                .Select(x => new UsersDataGrid
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate.ToShortDateString(),
                    Mail = x.Mail,
                    City = x.City.Name,
                    Gender = x.Gender,
                    Status = x.Status
                }).ToList();

            await LoadUsers(data);
        }
    }
}
