using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UtileDigitalSecurity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [Table("Users")]
    public class ApplicationUser : IdentityUser<long>
    {
    }
}
