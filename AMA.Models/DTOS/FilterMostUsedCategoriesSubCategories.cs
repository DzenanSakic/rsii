using AMA.Common.Enumerations;

namespace AMA.Models.DTOS
{
    public class FilterMostUsedCategoriesSubCategories
    {
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? Gender { get; set; }
        public int? Year { get; set; }
    }
}
