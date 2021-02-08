using System;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmBan : Form
    {
        private ApiService _banService = new ApiService("users/user");
        private int _userId;
        public FrmBan(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var banRequest = new
                {
                    UserId = _userId,
                    TimeFrom = dateTimePickerFrom.Value,
                    TimeTo = dateTimePickerTo.Value,
                    Reason = txtReason.Text
                };

                _ = await _banService.Post<object>(banRequest, "ban");

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
