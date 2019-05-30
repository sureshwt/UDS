using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UtileDigitalSecurity.Models
{
    public class UserTokenDeviceDetails
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        [ForeignKey("UserTokenDetails")]
        public int UserTokenDetailsId { get; set; }
        public DateTime DateCreated { get; set; }

        public UserTokenDeviceDetails()
        {
            this.DateCreated = DateTime.UtcNow;
        }

        #region Navigation properties
        public virtual UserTokenDetails UserTokenDetails { get; set; }
        #endregion
    }
}
