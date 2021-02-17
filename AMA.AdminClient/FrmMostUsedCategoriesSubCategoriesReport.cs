using AMA.AdminClient.Views;
using AMA.Common.Contracts;
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
    public partial class FrmMostUsedCategoriesSubCategoriesReport : Form
    {
        private ApiService _countriesService = new ApiService("countries");
        private ApiService _citiesService = new ApiService("cities");
        private ApiService _reportsService = new ApiService("reports");
        public List<CategoryUsageReport> result = new List<CategoryUsageReport>();
        public FrmMostUsedCategoriesSubCategoriesReport()
        {
            InitializeComponent();
        }

        private async void FrmMostUsedCategoriesSubCategoriesReport_Load(object sender, EventArgs e)
        {
            var countries = await _countriesService.Get<List<CountryResponse>>(null, "all");
            countries.Insert(0, new CountryResponse { ID = 0, Name = "Select country" });
            cmbCountry.DataSource = countries;
            cmbCountry.DisplayMember = "Name";
            cmbCountry.ValueMember = "ID";

            var genders = new List<object>();
            genders.Insert(0, new { ID = -1, Name = "Select gender" });
            genders.Insert(1, new { ID = 1, Name = "Male" });
            genders.Insert(2, new { ID = 2, Name = "Female" });
            genders.Insert(2, new { ID = 0, Name = "Other" });
            cmbGender.DataSource = genders;
            cmbGender.DisplayMember = "Name";
            cmbGender.ValueMember = "ID";
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

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtYear.Text) && !Regex.IsMatch(txtYear.Text, "^[0-9]+$"))
                {
                    MessageBox.Show("Year input can only be number!");
                    return;
                }

                int countryId;
                bool resultCountry = int.TryParse(cmbCountry.SelectedIndex.ToString(), out countryId);

                int cityId;
                bool resultCity = int.TryParse(cmbCity.SelectedIndex.ToString(), out cityId);
                if (cityId == -1)
                    cityId = 0;

                int gender;
                bool resultGender = int.TryParse(cmbGender.SelectedValue.ToString(), out gender);

                var request = new
                {
                    CountryId = countryId,
                    CityId = cityId,
                    Year = txtYear.Text,
                    Gender = gender
                };

                var response = await _reportsService.Get<List<CategoryUsageReport>>(request, "categories-subcategories/usage");

                var categoryGrid = new List<CategoryUsageReportGrid>();

                foreach (var item in response)
                {
                    var newGridItem = new CategoryUsageReportGrid 
                    { 
                        Id = item.Id,
                        Name = item.Name,
                        NumberOfAnswers = item.NumberOfAnswers,
                        NumberOfQuestions = item.NumberOfQuestions
                    };
                    categoryGrid.Add(newGridItem);
                }

                result = response;
                dataGridView1.DataSource = categoryGrid;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = dataGridView1.SelectedCells[0].RowIndex;
            var category = dataGridView1.Rows[index].DataBoundItem as CategoryUsageReportGrid;


            var subCategoryGrid = new List<SubCategoryUsageReportGrid>();

            foreach (var item in result)
            {
                if(item.SubCategoryUsageReport.All(x => x.CategoryId == category.Id))
                {
                    foreach (var sc in item.SubCategoryUsageReport)
                    {
                        var newGridItem = new SubCategoryUsageReportGrid
                        {
                            Id = sc.Id,
                            Name = sc.Name,
                            NumberOfAnswers = sc.NumberOfAnswers,
                            NumberOfQuestions = sc.NumberOfQuestions
                        };
                        subCategoryGrid.Add(newGridItem);
                    }
                }
            }

            dataGridView2.DataSource = subCategoryGrid;
        }
    }
}
