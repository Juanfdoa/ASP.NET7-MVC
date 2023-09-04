using BlogCore.Data;
using BlogCore.DataAccess.Data.Repository.IRepository;
using BlogCore.Models;
using System.Web.Mvc;

namespace BlogCore.DataAccess.Data.Repository
{
    internal class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Article article)
        {
            var objDb = _db.Articles.FirstOrDefault(s => s.Id == article.Id);
            if (objDb != null)
            {
                objDb.Name = article.Name;
                objDb.Description= article.Description;
                objDb.ImageUrl= article.ImageUrl;
                objDb.CategoryId= article.CategoryId;
            
                //_db.SaveChanges();
            }
        }
    }
}
