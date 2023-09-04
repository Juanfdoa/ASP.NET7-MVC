using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter a category name")]
        [Display(Name="Category name")]
        public string Name { get; set; }
        [Display(Name = "View order")]
        public int? Order { get; set; }
    }
}
