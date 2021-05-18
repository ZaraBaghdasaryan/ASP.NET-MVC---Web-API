using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VOD.UI.Models;
using Microsoft.AspNetCore.Identity;
using VOD.Common.Entities;
using VOD.Database.Services;
using VOD.Common.Extensions;
using VOD.UI.Services;

namespace VOD.UI.Controllers
{
    public class HomeController : Controller //The class can be the cupboard with many boxes in it 
    {
        private readonly ILogger<HomeController> _logger;

        private SignInManager<VODUser> _signInManager; //Creating the box _signInManager

       // private readonly IUIReadService db;
        public HomeController(ILogger<HomeController> logger, SignInManager<VODUser> signInMgr) //, IUIReadService db) //signInMgr- the injected object
        {
            _logger = logger;
            _signInManager = signInMgr;      //Storing signInMgr and logger inside our boxes as instruments to use
           // _db = db;                      //Once injected ASP-NET Core will automatically serve up instances of it through its default configuration.
        }

       
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToPage("/Account/Login",
                new { Area = "Identity" });
            return RedirectToAction("Dashboard", "Membership");
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

/*         Testing the IDbReadService 
 
            var result1 = await _db.GetAsync<Download>(); // Fetch all
                                                          
            var result2 = await _db.GetAsync<Download>(d =>d.ModuleId.Equals(1)); // Fetch all that matches the Lambda expression

            var result3 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));

            var result4 = await _db.AnyAsync<Download>(d =>d.ModuleId.Equals(1));

            var videos = new List<Video>();
            var convertedVideos = videos.ToSelectList<Video>("Id", "Title"); //Convert a List<Video> collection to SelectList items to be used with HTML and Razor to display a drop-down

            _db.Include<Download>();
            var result5 = await _db.SingleAsync<Download>(d => d.Id.Equals(3)); //load the navigation properties (Module and Course)

            _db.Include<Download>();
            _db.Include<Module, Course>();
            var result6 = await _db.SingleAsync<Download>(d => d.Id.Equals(3)); //navigation properties (Instructor and Video);
 
 
 
 */

/*  public async Task<IActionResult> Index()
{

            //  var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
if (user != null)
 {
     var courses = await _db.GetCoursesAsync(user.Id);
     var course = await _db.GetCourseAsync(user.Id, 1);
     var videos = await _db.GetVideosAsync(user.Id, 1);
     var video = await _db.GetVideoAsync(user.Id, 1);
 }*/


