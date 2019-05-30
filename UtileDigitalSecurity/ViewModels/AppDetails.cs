using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UtileDigitalSecurity.ViewModels
{
    public class AppDetails
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "App Name is required")]
        [Display(Name = "App Name")]
        public string AppName { set; get; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name is required")]
        public string UserName { set; get; }
        [Display(Name = "Generate Token")]
        [Required(ErrorMessage = "Token Id is required")]
        public string TokenId { set; get; }
        [Display(Name = "Enable / Disable")]
        public bool IsDisabled { set; get; }
    }
}
