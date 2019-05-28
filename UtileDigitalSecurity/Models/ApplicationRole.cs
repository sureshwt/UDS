using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtileDigitalSecurity.Models
{
    public class ApplicationRole : IdentityRole<long>
    {
        public string Description { get; set; }
    }
}
