using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is requiered")]
        public string Name { get; set; }

        [Required]
        public bool Status { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Slider Image")]
        public string ImageUrl { get; set; }
    }
}
