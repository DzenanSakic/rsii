using AMA.Common.Contracts;
using System;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmAnswer : Form
    {
        private readonly ApiService _answersService = new ApiService("answers");
        private readonly QuestionResponse _question;
        public FrmAnswer(QuestionResponse question)
        {
            _question = question;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnAnswer_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(richAnswer.Text) || string.IsNullOrWhiteSpace(richAnswer.Text))
            {
                MessageBox.Show("Replay can not be empty!");
                return;
            }

            var request = new
            {
                Text = richAnswer.Text,
                QuestionId = _question.ID
            };

            _ = await _answersService.Post<object>(request, "add");

            Close();
        }
    }
}
