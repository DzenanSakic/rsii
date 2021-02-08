using AMA.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IRepositoryCity _repositoryCity;
        public CitiesController(IRepositoryCity repositoryCity)
        {
            _repositoryCity = repositoryCity;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_repositoryCity.FindAll());
        }
    }
}