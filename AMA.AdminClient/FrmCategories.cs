using AMA.AdminClient.Views;
using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmCategories : Form
    {
        private ApiService _categoriesService = new ApiService("categories");
        public FrmCategories()
        {
            InitializeComponent();
        }

        private async void FrmCategories_Load(object sender, EventArgs e)
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            var categories = await _categoriesService.Get<List<CategoryResponse>>(null, "all");

            dataGridView1.DataSource = categories;
            if (categories.Any())
            {
                await LoadSubCategories();
            }
        }

        private async Task LoadSubCategories()
        {
            var index = dataGridView1.SelectedCells[0].RowIndex;
            var category = dataGridView1.Rows[index].DataBoundItem as CategoryResponse;

            var subCategories = await _categoriesService.Get<List<SubCategoryGrid>>(null, $"sub-categories/{category.Id}");
            dataGridView2.DataSource = subCategories;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            frm.FormClosing += addCategory_FromClosing;
            frm.Show();
        }
        private async void addCategory_FromClosing(object sender, FormClosingEventArgs e)
        {
            await LoadCategories();
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            await LoadSubCategories();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = dataGridView1.SelectedCells[0].RowIndex;
                var category = dataGridView1.Rows[index].DataBoundItem as CategoryResponse;

                FrmSubCategory frm = new FrmSubCategory(category.Id);
                frm.FormClosing += addSubCategory_FromClosing;
                frm.Show();
            }
        }

        private async void addSubCategory_FromClosing(object sender, FormClosingEventArgs e)
        {
            await LoadSubCategories();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                var index = dataGridView2.SelectedCells[0].RowIndex;
                var subCategory = dataGridView2.Rows[index].DataBoundItem as SubCategoryResponse;

                var request = new { Id = subCategory.Id };

                _ = await _categoriesService.Delete<object>(request, "sub-categories/delete");

                await LoadSubCategories();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = dataGridView1.SelectedCells[0].RowIndex;
                var category = dataGridView1.Rows[index].DataBoundItem as CategoryResponse;

                var request = new { Id = category.Id };

                _ = await _categoriesService.Delete<object>(null, $"sub-categories/{request.Id}");
                _ = await _categoriesService.Delete<object>(request, "delete");

                await LoadCategories();
            }
        }
    }
}
