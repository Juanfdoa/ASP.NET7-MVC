using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is requiered")]
        [Display(Name = "Article Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is requiered")]
        public string Description { get; set; }

        [Display(Name = "Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
