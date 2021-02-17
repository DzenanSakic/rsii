using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmCategory : Form
    {
        private ApiService _categoriesService = new ApiService("categories");
        public FrmCategory()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCategoryName.Text) || string.IsNullOrWhiteSpace(txtCategoryName.Text))
                {
                    MessageBox.Show("Name can not be empty!");
                    return;
                }

                if (!Regex.IsMatch(txtCategoryName.Text, "^[a-zA-Z ]+$"))
                {
                    MessageBox.Show("Name can only contain letters!");
                    return;
                }

                var newCategory = new
                {
                    Name = txtCategoryName.Text
                };

                _ = await _categoriesService.Post<object>(newCategory, "add");

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
