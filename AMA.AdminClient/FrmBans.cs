using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmBans : Form
    {
        private ApiService _bansService = new ApiService("users/user");
        private readonly int _userId;
        public FrmBans(int id, string userName)
        {
            InitializeComponent();
            label4.Text = userName;
            _userId = id;
        }

        private async void FrmBans_Load(object sender, EventArgs e)
        {
            await LoadBans();
        }

        private async Task LoadBans()
        {
            try
            {
                var data = (await _bansService.Get<List<BanResponse>>(null, $"{_userId}/bans"));

                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBan frm = new FrmBan(_userId);
            frm.FormClosing += banFrm_FormClosing;
            frm.Show();
        }

        private async void banFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await LoadBans();
        }
    }
}
