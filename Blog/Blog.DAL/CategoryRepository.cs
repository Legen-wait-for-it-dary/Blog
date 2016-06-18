using Blog.Entities;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace Blog.DAL
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository() { }

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Category> GetAllCategories()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Category> categoryObjectSet = context.CreateObjectSet<Category>();
                return categoryObjectSet.ToList();
            }
        }

        public Category GetCategoryById(int categoryId) 
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Category> categoryObjectSet = context.CreateObjectSet<Category>();

                return categoryObjectSet.FirstOrDefault();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Category> categoryObjectSet = context.CreateObjectSet<Category>();
                var categoryForUpdate = categoryObjectSet.FirstOrDefault(
                    cat => cat.CategoryId == category.CategoryId);

                if (categoryForUpdate != null)
                {
                    if (categoryForUpdate.Name != category.Name)
                        categoryForUpdate.Name = category.Name;

                    context.SaveChanges();
                }
            }
        }

        public void AddCategory(Category category)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Category> categoryObjectSet = context.CreateObjectSet<Category>();
                categoryObjectSet.AddObject(category);
                context.SaveChanges();
            }
        }
    }
}
