using System.ComponentModel.DataAnnotations;

namespace CrudNet7MVC.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="The name field is requiered")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The Telephone field is requiered")]
        public string Telephone { get; set; }
        
        [Required(ErrorMessage = "The phone field is requiered")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "The Email field is requiered")]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
