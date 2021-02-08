using AMA.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IRepositoryCountry _repositoryCountry;
        public CountriesController(IRepositoryCountry repositoryCountry)
        {
            _repositoryCountry = repositoryCountry;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_repositoryCountry.FindAll());
        }
    }
}