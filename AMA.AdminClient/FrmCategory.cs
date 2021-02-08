using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                if (string.IsNullOrEmpty(txtCategoryName.Text))
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

        //private void txtCategoryName_Validating(object sender, CancelEventArgs e)
        //{
        //    if(string.IsNullOrEmpty(txtCategoryName.Text))
        //    {
        //        e.Cancel = true;
        //        MessageBox.Show("Name can not be empty!");
        //        return;
        //    }

        //    if (!Regex.IsMatch(txtCategoryName.Text, "^[a-zA-Z ]$"))
        //    {
        //        e.Cancel = true;
        //        MessageBox.Show("Name can only contain letters!");
        //        return;
        //    }
        //}
    }
}
