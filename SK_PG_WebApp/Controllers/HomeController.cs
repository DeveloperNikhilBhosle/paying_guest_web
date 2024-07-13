using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SK_PG_WebApp.DAL;
using SK_PG_WebApp.Helper;
using SK_PG_WebApp.Models;
using SK_PG_WebApp.Models.DynamicModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SK_PG_WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext dbContext;
        private readonly IConfiguration config;

        public HomeController(ILogger<HomeController> logger, DatabaseContext databaseContext, IConfiguration _config)
        {
            _logger = logger;
            dbContext = databaseContext;
            config = _config;
        }

        public IActionResult Index()
        {
            var UserId = HttpContext.User.FindFirstValue(ClaimTypes.Actor);
            var data = dbContext.UsersDbSet.ToList();
            return View();
        }

        [Authorize]
        [Route("/admin")]
        public IActionResult Index1()
        {
            var UserId = HttpContext.User.FindFirstValue("UserId");
            ViewBag.ActiveProperty = string.Empty; ViewBag.TotalRooms = string.Empty; ViewBag.TotalLocations = string.Empty;
            ViewBag.OpenPGRequest = string.Empty; ViewBag.ActivePG = string.Empty; ViewBag.ActivePGBoys = string.Empty;
            ViewBag.ActivePGGirls = string.Empty;
            
            var list = new List<NotificationsDC>();
            ViewBag.News =list;
            Hashtable hash = new Hashtable();
            hash.Add("ipuserId", UserId);
            var data = new ManualDbContext(config).GetDataSet(StoredProcedure.USP_DASHBOARD,hash);
            if (data.IsNotNull())
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    ViewBag.ActiveProperty = Convert.ToString(data.Tables[0].Rows[0]["pgMasterCnt"]); 
                    ViewBag.TotalRooms = Convert.ToString(data.Tables[0].Rows[0]["pgRMCnt"]); 
                    ViewBag.TotalLocations = Convert.ToString(data.Tables[0].Rows[0]["pgLocationCnt"]);
                    ViewBag.OpenPGRequest = Convert.ToString(data.Tables[0].Rows[0]["pgReqCnt"]); 
                    ViewBag.ActivePG = Convert.ToString(data.Tables[0].Rows[0]["activePGCnt"]); 
                    ViewBag.ActivePGBoys = Convert.ToString(data.Tables[0].Rows[0]["activeMalePG"]);
                    ViewBag.ActivePGGirls = Convert.ToString(data.Tables[0].Rows[0]["activeFeMalePG"]);
                }


                foreach(DataRow items in data.Tables[1].Rows)
                {
                    var news = Convert.ToInt32(items["news"]);
                    if(news == 1)
                    {
                        list.Add(new NotificationsDC()
                        {
                            news = Convert.ToString(items["title"])
                        });
                    }else if(news == 2)
                    {
                        list.Add(new NotificationsDC()
                        {
                            news = Convert.ToString(items["title"]),
                            isGreatNews = false,
                            isInfoNew = true
                        });
                    }else if (news == 3)
                    {
                        list.Add(new NotificationsDC()
                        {
                            news = Convert.ToString(items["title"]),
                            isGreatNews = false,
                            isBadNews = true
                        });
                    }
                    
                }
            }
            //var UserId = HttpContext.User.FindFirstValue(ClaimTypes.Actor);
            //var data = dbContext.UsersDbSet.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
