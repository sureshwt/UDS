using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtileDigitalSecurity.Data;
using UtileDigitalSecurity.Models;
using UtileDigitalSecurity.Repository;
using UtileDigitalSecurity.ViewModels;

namespace UtileDigitalSecurity.Services
{
  public class TokenGenerationService
  {
    ApplicationDbContext _db;
    TokenGenerationRepository tgRepo;
    private readonly IMapper _mapper;

    public TokenGenerationService(ApplicationDbContext db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
      tgRepo = new TokenGenerationRepository(db);
    }
    public async Task<string> Create(UserTokenDetails model)
    {
      var result = await tgRepo.Add(model);
      if (result != null) { return result.TokenId; }
      return null;
    }

    public UserTokenDetails ValidateToken(string tokenId)
    {
      var result = tgRepo.getAppDetailsById(tokenId);
      if (result == null)
        return null;
      return new UserTokenDetails();
    }

    public async Task<AppDetails> getAppDetailsByAppId(int appId)
    {
      var result = await tgRepo.GetAppDetailsByAppId(appId);
      if (result == null)
        return null;
      return _mapper.Map<AppDetails>(result);
    }

    public async Task<AppDetails> UpdateDetails(AppDetails details)
    {
      var result = await tgRepo.GetAppDetailsByAppId(details.Id);
      if (result != null)
      {
        result.AppName = details.AppName;
        result.IsDisabled = details.IsDisabled;
        result.UserName = details.UserName;
        result.TokenId = details.TokenId;
        _db.Entry(result).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return new AppDetails();
      }
      return new AppDetails();
    }

    public async Task<List<AllAppsAccessHistory>> GetAllAppsHistory()
    {
      return await tgRepo.GetAllAppsHistory();
    }

    public async Task<UserTokenDetails> setDisabledByAppId(int appId)
    {
      return await tgRepo.setDisabledByAppId(appId);
    }
  }
}
