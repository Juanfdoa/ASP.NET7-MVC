using Microsoft.AspNetCore.Mvc.Rendering; // Instalar Microsoft.AspNetCore.Mvc.ViewFeatures

namespace BlogCore.Models.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Article> ArticleList { get; set; }
    }
}
