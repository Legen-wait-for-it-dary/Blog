using Blog.Entities;
using System.Collections.Generic;

namespace Blog.DAL
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        void UpdateCategory(Category category);
        void AddCategory(Category category);
    }
}
