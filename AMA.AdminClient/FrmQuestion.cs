using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmQuestion : Form
    {
        private ApiService _questionsService = new ApiService("questions");
        private ApiService _categoriesService = new ApiService("categories");

        private QuestionResponse _questionResponse;
        private int _subCategoryId;
        public FrmQuestion(QuestionResponse question)
        {
            InitializeComponent();
            _questionResponse = question;
        }

        private async void FrmQuestion_Load(object sender, EventArgs e)
        {
            lblTitle.Text = _questionResponse.Title;
            lblBody.Text = _questionResponse.Body;

            var questionCategory = (await _questionsService.Get<List<QuestionSubCategoryResponse>>(null, $"category/{_questionResponse.ID}")).FirstOrDefault();
            var subCategories = (await _categoriesService.Get<List<SubCategoryResponse>>(null, $"sub-categories/{questionCategory.SubCategory.Category.Id}"));
            var categories = (await _categoriesService.Get<List<CategoryResponse>>(null, "all"));
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";
            cmbCategory.SelectedValue = questionCategory.SubCategory.Category.Id;


            cmbSubCategory.DataSource = subCategories;
            cmbSubCategory.DisplayMember = "Name";
            cmbSubCategory.ValueMember = "Id";
            cmbSubCategory.SelectedValue = questionCategory.SubCategoryId;

            _subCategoryId = questionCategory.SubCategoryId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int subcategoryId;
            _ = int.TryParse(cmbSubCategory.SelectedValue?.ToString(), out subcategoryId);

            if(subcategoryId == 0)
            {
                MessageBox.Show("Must select sub-category!");
                return;
            }

            var request = new
            {
                QuestionId = _questionResponse.ID,
                Title = lblTitle.Text,
                Body = lblBody.Text,
                SubCategoryId = subcategoryId
            };

            _ = _questionsService.Post<object>(request, "edit");

            Close();
        }

        private async void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int categoryId;
                bool result = int.TryParse(cmbCategory.SelectedValue.ToString(), out categoryId);
                if (result && categoryId > 0)
                {
                    var subCategories = (await _categoriesService.Get<List<SubCategoryResponse>>(null, $"sub-categories/{categoryId}"));
                    subCategories.Insert(0, new SubCategoryResponse { Id = 0, Name = "Select sub-category" });
                    cmbSubCategory.DataSource = subCategories;
                    cmbSubCategory.DisplayMember = "Name";
                    cmbSubCategory.ValueMember = "Id";
                    cmbSubCategory.SelectedValue = _subCategoryId;
                }
                else
                    cmbSubCategory.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
