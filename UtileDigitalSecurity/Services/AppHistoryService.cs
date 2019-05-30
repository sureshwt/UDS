using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtileDigitalSecurity.Data;
using UtileDigitalSecurity.Models;
using UtileDigitalSecurity.Repository;
using UtileDigitalSecurity.ViewModels;

namespace UtileDigitalSecurity.Repository
{
  public class AppHistoryService
  {
    ApplicationDbContext _db;
    AppHistoryRepository appHistoryRepo;
    public AppHistoryService(ApplicationDbContext db)
    {
      _db = db;
      appHistoryRepo = new AppHistoryRepository(db);
    }

    public string ValidateUser(UserDetails data)
    {
      var result = appHistoryRepo.getUserInfo(data);
      if(result == null)
      {
        return null;
      }
      return result.TokenId;
    }

    public UserTokenDetails MatchedById(string token, string appName)
    {
      var result = appHistoryRepo.getAppId(token , appName);
      return result;
    }

    public async Task<IEnumerable<AppLogHistory>> GetAppLogHistory(int appId)
    {
      return await appHistoryRepo.GetAppLogHistory(appId);
    }

    public AllAppsAccessHistory SaveUserLog(UserTokenDeviceDetails data)
    {
      _db.UserTokenDeviceDetails.Add(data);
        _db.SaveChanges();

      return new AllAppsAccessHistory();
    }

    public async Task<List<AllAppsAccessHistory>> GetHistoryDetails()
    {

      return await (from i in _db.UserTokenDeviceDetails
       join j in _db.UserTokenDetails on i.UserTokenDetailsId equals j.Id
       select new AllAppsAccessHistory
       {
         AppId = i.Id,
         AppName =j.AppName,
         UserName = j.UserName,
         DeviceId = i.DeviceId,
         isDisabled = j.IsDisabled,
         TokenId = j.TokenId,
         DateCreated = i.DateCreated.ToString("MM/dd/yyyy"),
         Time = i.DateCreated.ToString("hh:mm tt"),
       }).ToListAsync();
    }
  }
}
