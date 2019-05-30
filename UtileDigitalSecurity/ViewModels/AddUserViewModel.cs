using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UtileDigitalSecurity.ViewModels
{
    public class AddUserViewModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { set; get; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { set; get; }
        [Required(ErrorMessage = "Role is required")]

        public string Role { set; get; }
    }
}
