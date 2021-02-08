using AMA.Models.DTOS;
using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersSubCategoriesController : ControllerBase
    {
        private readonly IRepositoryUserSubCategory _repositoryUserSubCategory;
        private readonly IRepositorySubCategory _repositorySubCategory;
        private readonly IRepositoryUser _repositoryUser;

        public UsersSubCategoriesController(IRepositoryUserSubCategory userSubCategory,
            IRepositorySubCategory repositorySubCategory,
            IRepositoryUser repositoryUser)
        {
            _repositoryUserSubCategory = userSubCategory;
            _repositorySubCategory = repositorySubCategory;
            _repositoryUser = repositoryUser;
        }

        [HttpGet("find/user/{userId}")]
        public IActionResult Find(int userId)
        {
            return Ok(_repositoryUserSubCategory.FindByUserId(userId));
        }

        [HttpPost("add")]
        public IActionResult Insert(InsertUserSubCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_repositoryUser.TryFind(request.UserId) == null)
                return BadRequest($"User with id {request.UserId} does not exist!");

            if (_repositorySubCategory.TryFind(request.SubCategoryId) == null)
                return BadRequest($"Sub-category with id {request.SubCategoryId} does not exist!");

            if (_repositoryUserSubCategory.Find(request.UserId, request.SubCategoryId) != null)
                return BadRequest($"User {request.UserId} already has sub-category {request.SubCategoryId}!");

            var userSubCategory = new UserSubCategory
            {
                UserId = request.UserId,
                SubCategoryId = request.SubCategoryId
            };

            _repositoryUserSubCategory.Insert(userSubCategory);

            return Ok();
        }

        [HttpDelete("delete/sub-category/{subCategoryId}")]
        public IActionResult Delete(int subCategoryId)
        {
            var userId = Convert.ToInt32(User.Claims.Where(x => x.Type == "Id").FirstOrDefault().Value);

            var userSubCategory = _repositoryUserSubCategory.Find(userId, subCategoryId);

            if(userSubCategory != null)
                _repositoryUserSubCategory.Delete(userSubCategory);

            return Ok();
        }
    }
}