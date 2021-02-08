using System;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmMsg : Form
    {
        private ApiService _msgsService = new ApiService("users/user/message");
        private int _userId;
        public FrmMsg(int id, string userName)
        {
            InitializeComponent();
            label2.Text = userName;
            _userId = id;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new 
                {
                    ToUserId = _userId,
                    Body = txtMsg.Text,
                    Title = txtTitle.Text
                };

                _ = _msgsService.Post<object>(request,null);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
