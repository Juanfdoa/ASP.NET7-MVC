using Microsoft.AspNetCore.Mvc.Rendering; // Instalar Microsoft.AspNetCore.Mvc.ViewFeatures

namespace BlogCore.Models.ViewModels
{
    public class ArticleVM
    {
        public Article Article { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
