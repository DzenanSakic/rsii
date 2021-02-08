using AMA.Models.DTOS;
using AMA.Repositories.Interfaces;
using AMA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepositoryCategory _repositoryCategory;
        private readonly IRepositorySubCategory _repositorySubCategory;
        private readonly ICategoryService _categoryService;
        public CategoriesController(IRepositoryCategory repositoryCategory,
            ICategoryService categoryService,
            IRepositorySubCategory repositorySubCategory)
        {
            _repositoryCategory = repositoryCategory;
            _categoryService = categoryService;
            _repositorySubCategory = repositorySubCategory;
        }

        #region Category

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(InsertCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_repositoryCategory.TryFind(request.Name) != null)
                return BadRequest("Category already exist!");

            _categoryService.AddCategory(request);

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_repositoryCategory.FindAll());
        }

        [HttpGet("find")]
        public IActionResult Find([FromQuery] FindCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_categoryService.FindCategry(request));
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromQuery] FindCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _categoryService.TryDeleteCategory(request);

            return Ok();
        }

        #endregion

        #region SubCategory

        [HttpPost("sub-categories/add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(InsertSubCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_repositoryCategory.TryFind(request.CategoryId) == null)
                return BadRequest($"CategoryId: {request.CategoryId} does not exist!");

            if (_repositorySubCategory.TryFind(request.Name, request.CategoryId) != null)
                return BadRequest("Sub-category for that category already exist!");

            _categoryService.AddSubCategory(request);

            return Ok();
        }

        [HttpGet("sub-categories/{categoryId}")]
        public IActionResult FindAll([FromRoute] int categoryId)
        {
            return Ok(_repositorySubCategory.TryFindAll(categoryId));
        }

        [HttpDelete("sub-categories/delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromQuery] DeleteSubCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _categoryService.TryDeleteSubCategory(request);

            return Ok();
        }

        [HttpDelete("sub-categories/{categoryId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteByCategory(int categoryId)
        {
            _categoryService.TryDeleteSubCategories(categoryId);

            return Ok();
        }

        #endregion
    }
}