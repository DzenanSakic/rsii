using AMA.Models.DTOS;
using AMA.Models.Entities;
using AMA.Repositories.Interfaces;
using System.Linq;

namespace AMA.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryCategory _repositoryCategory;
        private readonly IRepositorySubCategory _repositorySubCategory;

        public CategoryService(IRepositoryCategory repositoryCategory, IRepositorySubCategory repositorySubCategory)
        {
            _repositoryCategory = repositoryCategory;
            _repositorySubCategory = repositorySubCategory;
        }

        #region Category
        public void AddCategory(InsertCategoryRequest category)
        {
            var newCategory = new Category
            {
                Name = category.Name
            };

            _repositoryCategory.Insert(newCategory);
        }

        public void TryDeleteCategory(FindCategoryRequest request)
        {
            Category category = null;

            if (request.Id.HasValue)
                category = _repositoryCategory.TryFind(request.Id.Value);
            else
                category = _repositoryCategory.TryFind(request.Name);

            if(category != null)
                _repositoryCategory.Delete(category); 
        }

        public Category FindCategry(FindCategoryRequest request)
        {
            if (request.Id.HasValue)
                return _repositoryCategory.TryFind(request.Id.Value);
            else
                return _repositoryCategory.TryFind(request.Name);
        }
        #endregion

        #region SubCategory
        public void AddSubCategory(InsertSubCategoryRequest request)
        {
            var subCategory = new SubCategory
            {
                CategoryId = request.CategoryId,
                Name = request.Name
            };

            _repositorySubCategory.Insert(subCategory);
        }

        public void TryDeleteSubCategory(DeleteSubCategoryRequest request)
        {
            SubCategory subCategory = null;

            if (request.Id.HasValue)
                subCategory = _repositorySubCategory.TryFind(request.Id.Value);
            else
                subCategory = _repositorySubCategory.TryFind(request.Name);

            if (subCategory != null)
                _repositorySubCategory.Delete(subCategory);
        }

        public void TryDeleteSubCategories(int categoryId)
        {
            var subCategories = _repositorySubCategory.TryFindAll(categoryId);

            if(subCategories.Any())
            {
                foreach (var subCategory in subCategories)
                {
                    _repositorySubCategory.Delete(subCategory);
                }
            }
        }
        #endregion
    }
}
