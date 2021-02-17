using AMA.Common.Contracts;
using AMA.Models.DTOS;
using System.Collections.Generic;

namespace AMA.Services
{
    public interface IReportService
    {
        IList<UserActivityReportResponse> GetUsersActivityReport(FilterUsersActivityReport filter);
        IList<UserPerformanceReportResponse> GetUsersPerformanceReport(FilterUsersActivityReport filter);
        IList<CategoryUsageReport> GetCategorySubCategoryUsage(FilterMostUsedCategoriesSubCategories filter);
    }
}
