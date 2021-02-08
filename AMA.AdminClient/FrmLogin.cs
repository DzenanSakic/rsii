using AMA.Common.Contracts;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmLogin : Form
    {
        private readonly ApiService _authService = new ApiService("auth");

        public FrmLogin()
        {
            InitializeComponent();
            txtUsername.Text = "Alfica";
            txtPassword.Text = "Alfica123";
        }

        private void FrmHome_Closing(object sender, FormClosingEventArgs e)
        {
            Show();
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            ApiService.Token = string.Empty;
            btnLogin.Enabled = true;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;

                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Username and password required!");
                    btnLogin.Enabled = true;
                    return;
                }

                if (!Regex.IsMatch(txtUsername.Text, "^[a-zA-Z0-9]+$"))
                {
                    MessageBox.Show("Username can only contain letters and numbers!");
                    return;
                }

                var LoginRequest = new
                {
                    username = txtUsername.Text,
                    password = txtPassword.Text
                };

                LoginResponse loggedUser;

                try
                {
                    loggedUser = await _authService.Post<LoginResponse>(LoginRequest, "login");
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Incorrect username or password", "Error", MessageBoxButtons.OK);
                    btnLogin.Enabled = true;
                    return;
                }

                if (loggedUser == null)
                {
                    MessageBox.Show("Incorrect username or password", "Error", MessageBoxButtons.OK);
                    btnLogin.Enabled = true;
                    return;
                }

                if(loggedUser.Role != "Admin")
                {
                    MessageBox.Show("Invalid user role!", "Error", MessageBoxButtons.OK);
                    btnLogin.Enabled = true;
                    return;
                }

                ApiService.Token = loggedUser.AccessToken;
                ApiService.UserId = loggedUser.Id;
                ApiService.Permission = loggedUser.Role;

                FrmHome home = new FrmHome();
                home.Show();
                Hide();
                home.FormClosing += FrmHome_Closing;
            }
            catch (Exception ex)
            {
                btnLogin.Enabled = false;

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
