using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UtileDigitalSecurity.Data;
using UtileDigitalSecurity.Models;
using UtileDigitalSecurity.Repository;
using UtileDigitalSecurity.Services;
using UtileDigitalSecurity.ViewModels;

namespace UtileDigitalSecurity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private ApplicationDbContext _db;

        public HomeController(IMapper mapper, ApplicationDbContext db)
        {
            _mapper = mapper;
            _db = db;
            //AppHistory historyDetails = new AppHistory();
        }


        /// <summary>
        /// Method to get the Token list
        /// </summary>
        /// <returns>list of tokenn to created</returns>

        public async Task<IActionResult> Index()
        {
            using (var dba = new ApplicationDbContext())
            {
                var model = await _db.UserTokenDetails.OrderByDescending(s => s.Id).ToListAsync();
                //var model = await db.UserTokenDetails.OrderByDescending().TolistAsync();
                var viewModel = _mapper.Map<IEnumerable<AppDetails>>(model);
                return View(viewModel);
            }
        }

        /// <summary>
        /// Method to Return partial view to create token
        /// </summary>
        /// <returns>partial view for create token</returns>


        public ActionResult GenerateNewToken()
        {
            AppDetails model = new AppDetails();
            return PartialView(model);
        }

        /// <summary>
        /// Method to Save the token Details 
        /// </summary>
        /// <param name="det">token details</param>
        /// <returns>Return to Home page</returns>


        [HttpPost]
        public async Task<ActionResult> GenerateNewToken(AppDetails det)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    if (ModelState.IsValid)
                    {
                        TokenGenerationService token = new TokenGenerationService(_db, _mapper);
                        var tokenExits = token.ValidateToken(det.TokenId);
                        if (tokenExits == null)
                        {
                            var dateNow = DateTime.UtcNow;
                            await token.Create(new UserTokenDetails
                            {
                                DateCreated = dateNow,
                                AppName = det.AppName,
                                UserName = det.UserName,
                                TokenId = det.TokenId,
                                IsDisabled = true
                            });
                            TempData["CreateCompleteMessage"] = "Token Created Successfully";
                        }
                        else
                        {
                            TempData["tokenExits"] = "Token Already Exists";
                            return RedirectToAction("Index");
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        ///Method to Returns partial view for Update the token
        /// </summary>
        /// <param name="appId">for perticular token</param>
        /// <returns>partial view for update the token</returns>

        [HttpGet]
        public async Task<ActionResult> UpdateToken(int appId)
        {
            using (var db = new ApplicationDbContext())
            {
                TokenGenerationService token = new TokenGenerationService(db, _mapper);
                var result = await token.getAppDetailsByAppId(appId);

                return PartialView(result);
            }
        }

        /// <summary>
        ///Method to perform Update the token details
        /// </summary>
        /// <param name="details">updated token details</param>
        /// <returns>in success case returnd home and in failure case returns partial view</returns>


        [HttpPost]
        public async Task<ActionResult> UpdateToken(AppDetails details)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    TokenGenerationService token = new TokenGenerationService(db, _mapper);
                    var record = await token.UpdateDetails(details);
                    TempData["UpdateTokenSuccess"] = "Token Updated Successfully!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateTokenFailure"] = "Token Updated Successfully!";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Method to get the all apps Log history
        /// </summary>
        /// <returns>returns all app log history</returns>


        public async Task<ActionResult> AllAppHistory()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    TokenGenerationService token = new TokenGenerationService(_db, _mapper);
                    var historydetails = await token.GetAllAppsHistory();
                    return View(historydetails);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
        /// <summary>
        /// This method to perform the disable or enable the record
        /// </summary>
        /// <param name="appId">based on app id</param>
        /// <returns></returns>
        public async Task<ActionResult> IsEnableOrDisable(int appId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    TokenGenerationService token = new TokenGenerationService(_db, _mapper);

                    var tokenRecord = await token.setDisabledByAppId(appId);

                    if (tokenRecord.IsDisabled == true)
                    {
                        tokenRecord.IsDisabled = false;
                    }
                    else
                    {
                        tokenRecord.IsDisabled = true;
                    }
                    _db.Entry(tokenRecord).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        [HttpGet]
        public async Task<ActionResult> AppNameHistory(int appId)
        {
            using (var db = new ApplicationDbContext())
            {
                AppHistoryService service = new AppHistoryService(_db);
                var appData = await _db.UserTokenDetails.FirstOrDefaultAsync(s => s.Id == appId);
                var appHistory = await service.GetAppLogHistory(appId);
                var tupleValue = new Tuple<UserTokenDetails, IEnumerable<AppLogHistory>>(appData, appHistory);
                return View(tupleValue);
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
