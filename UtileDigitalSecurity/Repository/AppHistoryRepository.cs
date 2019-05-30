using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtileDigitalSecurity.Models;
using Microsoft.EntityFrameworkCore;
using UtileDigitalSecurity.Data;
using UtileDigitalSecurity.ViewModels;

namespace UtileDigitalSecurity.Repository
{
    public class AppHistoryRepository
    {

    private ApplicationDbContext db;
    private DbSet<UserTokenDeviceDetails> Set => db.UserTokenDeviceDetails;
    private DbSet<UserTokenDetails> appdata => db.UserTokenDetails;

    public AppHistoryRepository(ApplicationDbContext db)
    {
      this.db = db;
    }

    public UserTokenDetails getUserInfo(UserDetails data)
    {
      return appdata.FirstOrDefault(t => (t.AppName == data.AppName) && (t.TokenId == data.TokenId));
    }

    public UserTokenDetails getAppId(string token, string appName)
    {
      return appdata.FirstOrDefault(t => (t.AppName == appName) && (t.TokenId == token));
    }

    public async Task<IEnumerable<AppLogHistory>> GetAppLogHistory(int appId)
    {
      return await (from i in db.UserTokenDeviceDetails
                    join j in db.UserTokenDetails
                    on i.UserTokenDetailsId equals j.Id
                    where i.UserTokenDetailsId == appId
                    select new AppLogHistory
                    {
                      AppName = j.AppName,
                      Date = i.DateCreated.ToLocalTime().ToString("dd/MM/yyyy"),
                      Time = i.DateCreated.ToLocalTime().ToString("HH:mm"),
                      DeviceId = i.DeviceId,
                      TokenId = j.TokenId,
                      UserName = j.UserName,
                    }).ToListAsync();
    }
  }
}
