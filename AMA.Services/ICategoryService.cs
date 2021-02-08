using AMA.Models.DTOS;
using AMA.Models.Entities;

namespace AMA.Services
{
    public interface ICategoryService
    {
        #region Category
        void AddCategory(InsertCategoryRequest request);
        Category FindCategry(FindCategoryRequest request);
        void TryDeleteCategory(FindCategoryRequest request);
        #endregion

        #region SubCategory
        void AddSubCategory(InsertSubCategoryRequest request);
        void TryDeleteSubCategory(DeleteSubCategoryRequest request);
        void TryDeleteSubCategories(int categoryId);

        #endregion
    }
}
