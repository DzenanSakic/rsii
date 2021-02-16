using AMA.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMA.AdminClient
{
    public partial class FrmUsersPerformanceReport : Form
    {
        private ApiService _categoriesService = new ApiService("categories");
        private ApiService _countriesService = new ApiService("countries");
        private ApiService _citiesService = new ApiService("cities");
        private ApiService _reportsService = new ApiService("reports");

        public FrmUsersPerformanceReport()
        {
            InitializeComponent();
        }

        private async void FrmUsersPerformanceReport_Load(object sender, EventArgs e)
        {
            var categories = (await _categoriesService.Get<List<CategoryResponse>>(null, "all"));
            categories.Insert(0, new CategoryResponse { Id = 0, Name = "Select category" });
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";

            var countries = await _countriesService.Get<List<CountryResponse>>(null, "all");
            countries.Insert(0, new CountryResponse { ID = 0, Name = "Select country" });
            cmbCountry.DataSource = countries;
            cmbCountry.DisplayMember = "Name";
            cmbCategory.ValueMember = "ID";
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
                }
                else
                    cmbSubCategory.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int countryId;
                bool result = int.TryParse(cmbCountry.SelectedIndex.ToString(), out countryId);
                if (result && countryId > 0)
                {
                    var cities = await _citiesService.Get<List<CityResponse>>(null, $"all");
                    var filteredCities = cities.Where(x => x.Country.ID == countryId).ToList();
                    filteredCities.Insert(0, new CityResponse { Id = 0, Name = "Select city" });
                    cmbCity.DataSource = filteredCities;
                    cmbCity.DisplayMember = "Name";
                    cmbCity.ValueMember = "Id";
                }
                else
                    cmbCity.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            int countryId;
            bool resultCountry = int.TryParse(cmbCountry.SelectedIndex.ToString(), out countryId);

            int cityId;
            bool resultCity = int.TryParse(cmbCity.SelectedIndex.ToString(), out cityId);
            if (cityId == -1)
                cityId = 0;

            int categoryId;
            bool resultCategotry = int.TryParse(cmbCategory.SelectedValue.ToString(), out categoryId);

            int subCategoryId;
            bool resultSubCategory = int.TryParse(cmbSubCategory.SelectedIndex.ToString(), out subCategoryId);
            if (subCategoryId == -1)
                subCategoryId = 0;

            var request = new
            {
                CountryId = countryId,
                CityId = cityId,
                CategoryId = categoryId,
                SubCategoryId = subCategoryId
            };

            var response = await _reportsService.Get<List<UserPerformanceReportResponse>>(request, "users/performance");

            dataGridView1.DataSource = response;
        }
    }
}
