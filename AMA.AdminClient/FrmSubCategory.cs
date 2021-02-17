using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmSubCategory : Form
    {
        private ApiService _subCategoriesService = new ApiService("categories/sub-categories");
        private int _categoryId;
        public FrmSubCategory(int categoryId)
        {
            InitializeComponent();
            _categoryId = categoryId;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCategoryName.Text) || string.IsNullOrWhiteSpace(txtSubCategoryName.Text))
                {
                    MessageBox.Show("Name can not be empty!");
                    return;
                }

                if (!Regex.IsMatch(txtSubCategoryName.Text, "^[a-zA-Z ]+$"))
                {
                    MessageBox.Show("Name can only contain letters!");
                    return;
                }

                var subCategoryRequest = new
                {
                    CategoryId = _categoryId,
                    Name = txtSubCategoryName.Text
                };

                _ = await _subCategoriesService.Post<object>(subCategoryRequest, "add");

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
