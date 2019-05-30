using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtileDigitalSecurity.Models
{
    public class UserTokenDetails
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        public string UserName { get; set; }
        public string TokenId { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public UserTokenDetails()
        {
            this.DateUpdated = DateTime.UtcNow;
        }
    }
}
