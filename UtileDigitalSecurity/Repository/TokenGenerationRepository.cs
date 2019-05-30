using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtileDigitalSecurity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UtileDigitalSecurity.ViewModels;
using UtileDigitalSecurity.Data;

namespace UtileDigitalSecurity.Repository
{
  public class TokenGenerationRepository
  {
    private ApplicationDbContext db;
    private DbSet<UserTokenDetails> Set => db.UserTokenDetails;


    public TokenGenerationRepository(ApplicationDbContext db)
    {
      this.db = db;
    }
    public UserTokenDetails getAppDetailsById(string id)
    {
      return Set.FirstOrDefault(t => t.TokenId == id);
    }

    public async Task<UserTokenDetails> Add(UserTokenDetails add)
    {
      var i = await Set.AddAsync(add);
      await db.SaveChangesAsync();
      return add;
    }
    public async Task<UserTokenDetails> GetAppDetailsByAppId(int appId)
    {
      return await Set.FirstOrDefaultAsync(t => t.Id == appId);
    }

    public async Task<List<AllAppsAccessHistory>> GetAllAppsHistory()
    {
      List<AllAppsAccessHistory> historydetails = await (from udata in db.UserTokenDetails
                                         join us in db.UserTokenDeviceDetails on udata.Id equals us.UserTokenDetailsId
                                         select new AllAppsAccessHistory
                                         {
                                           DeviceId = us.DeviceId,
                                           UserName = udata.UserName,
                                           AppId = udata.Id,
                                           isDisabled = udata.IsDisabled,
                                           TokenId = udata.TokenId,
                                           DateCreated = us.DateCreated.ToLocalTime().ToString("dd/MM/yyyy"),
                                           Time = us.DateCreated.ToLocalTime().ToString("HH:mm"),
                                           AppName = udata.AppName
                                         }).ToListAsync();


      return historydetails;
    }

    public async Task<UserTokenDetails> setDisabledByAppId(int appId)
    {
      var isDisable = await Set.FirstOrDefaultAsync(s => s.Id == appId);
      return isDisable;
    }
  }
}
