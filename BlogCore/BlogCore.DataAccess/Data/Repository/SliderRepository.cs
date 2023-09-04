using BlogCore.Data;
using BlogCore.DataAccess.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.DataAccess.Data.Repository
{
    internal class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slider slider)
        {
            var objDb = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            if (objDb != null)
            {
                objDb.Name = slider.Name;
                objDb.Status = slider.Status;
                objDb.ImageUrl = slider.ImageUrl;
            
                _db.SaveChanges();
            }
        }
    }
}
