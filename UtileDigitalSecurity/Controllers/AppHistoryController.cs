using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UtileDigitalSecurity.Data;
using UtileDigitalSecurity.Models;
using UtileDigitalSecurity.Repository;
using UtileDigitalSecurity.ViewModels;

namespace UtileDigitalSecurity.Controllers
{
  public class AppHistoryController : Controller
  {

    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;

    public AppHistoryController(IMapper mapper, ApplicationDbContext db)
    {
      _mapper = mapper;
       _db = db;
    }
 

    public ActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// method to get the app log history based on appId
    /// </summary>
    /// <param name="appId">perticular appid </param>
    /// <returns> List App log history</returns>
   

    [HttpGet]
    public async Task<ActionResult> AppLogHistory(int appId)
    {
      using (var db = new ApplicationDbContext())
      {
        AppHistoryService service = new AppHistoryService(_db);
        var appData = await _db.UserTokenDetails.FirstOrDefaultAsync(s => s.Id == appId );
        var appHistory = await service.GetAppLogHistory(appId);
        var tupleValue = new Tuple<UserTokenDetails,IEnumerable<AppLogHistory>>(appData, appHistory);
        return View(tupleValue);
      }
    }



    /// <summary>
    /// Method to verify token details and saves device id and log history
    /// </summary>
    /// <param name="data">End user provide the token data</param>
    /// <returns>Success or failure message</returns>
    ///

    [HttpPost]
    [Route("AppHistory/ValidateToken")]
    public ActionResult ValidateToken([FromBody] UserDetails data)
    {
      try
      {
        using (var db = new ApplicationDbContext())
        {
          if (data == null)
          {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
          }
          else
          {
            AppHistoryService history = new AppHistoryService(_db);
            var tokenInfo = history.MatchedById(data.TokenId, data.AppName);
            var isValid = history.ValidateUser(data);

            if (isValid != null)
            {
              var userTokenDeviceDetails = new UserTokenDeviceDetails
              {
                UserTokenDetailsId = tokenInfo.Id,
                DeviceId = data.DeviceId,
              };

              var validUser = history.SaveUserLog(userTokenDeviceDetails);
            }
            else
            {
              TokenValidationStatus failureStatus = new TokenValidationStatus();
              failureStatus.success = false;
              failureStatus.message = "No such token found";
              return Ok(failureStatus);
            }
            TokenValidationStatus successStatus = new TokenValidationStatus();
            successStatus.success = true;
            successStatus.message = "Token validated successfully";
            return Ok(successStatus);
          }
        }
      }
      catch (Exception ex)
      {
        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent);
      }
    }
  }
}
