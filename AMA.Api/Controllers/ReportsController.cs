using AMA.Models.DTOS;
using AMA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("users/activity")]
        public IActionResult UsersActivityReport([FromQuery] FilterUsersActivityReport filter)
        {
            if (filter.CategoryId == 0)
                filter.CategoryId = null;

            if (filter.CityId == 0)
                filter.CityId = null;

            if (filter.SubCategoryId == 0)
                filter.SubCategoryId = null;

            if (filter.CountryId == 0)
                filter.CountryId = null;

            return Ok(_reportService.GetUsersActivityReport(filter));
        }

        [HttpGet("users/performance")]
        public IActionResult UsersPerformanceReport([FromQuery] FilterUsersActivityReport filter)
        {
            if (filter.CategoryId == 0)
                filter.CategoryId = null;

            if (filter.CityId == 0)
                filter.CityId = null;

            if (filter.SubCategoryId == 0)
                filter.SubCategoryId = null;

            if (filter.CountryId == 0)
                filter.CountryId = null;

            return Ok(_reportService.GetUsersPerformanceReport(filter));
        }
    }
}