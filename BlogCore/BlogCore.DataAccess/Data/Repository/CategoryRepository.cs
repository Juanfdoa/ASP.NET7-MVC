using BlogCore.Data;
using BlogCore.DataAccess.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.DataAccess.Data.Repository
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListCategories()
        {
            return _db.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }
            );
        }

        public void update(Category category)
        {
            var objDb = _db.Category.FirstOrDefault(s => s.Id == category.Id);
            if (objDb != null)
            {
                objDb.Name = category.Name;
                objDb.Order = category.Order;
            
                _db.SaveChanges();
            }
        }
    }
}
