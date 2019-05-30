using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UtileDigitalSecurity.ViewModels
{
    public class AppLogHistory
    {
        public string AppName { set; get; }
        public string UserName { set; get; }
        [DisplayName("Device ID")]
        public string DeviceId { set; get; }
        public string TokenId { set; get; }

        public string Date { set; get; }
        public string Time { set; get; }
    }
}
