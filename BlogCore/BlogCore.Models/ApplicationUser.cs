using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogCore.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required(ErrorMessage ="Name is requiered")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adress is requiered")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "City is requiered")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is requiered")]
        public string Country { get; set; }
    }
}
