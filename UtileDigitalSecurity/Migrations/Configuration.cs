using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtileDigitalSecurity.Data;
using UtileDigitalSecurity.Models;

namespace UtileDigitalSecurity.Migrations
{
    public class Configuration
    {
        private ApplicationDbContext _db;

        public Configuration(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task SaveRoles()
        {
            var data = _db.Roles.ToList();
            if (data.Count == 0)
            {
                _db.AddRange(_role);
                await _db.SaveChangesAsync();
            }
        }

        List<ApplicationRole> _role = new List<ApplicationRole>
        {
            new ApplicationRole()
            {
                Name = "User"
            },
            new ApplicationRole()
            {
                Name = "Admin"
            }
        };
    }
    }
