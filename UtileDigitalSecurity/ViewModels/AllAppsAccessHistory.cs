using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UtileDigitalSecurity.ViewModels
{
    public class AllAppsAccessHistory
    {
        [DisplayName("Device ID")]
        public string DeviceId { set; get; }
        [DisplayName("#")]
        public int AppId { set; get; }
        [DisplayName("App Name")]
        public string AppName { set; get; }
        [DisplayName("User Name")]
        public string UserName { set; get; }
        [DisplayName("Token ID")]
        public string TokenId { set; get; }

        public bool isDisabled { set; get; }
        [DisplayName("Date")]
        public string DateCreated { set; get; }
        public string Time { get; set; }
    }
}
