using Microsoft.AspNetCore.Mvc;
using MyFriends.DAL;
using MyFriends.Models;
using System.Diagnostics;

namespace MyFriends.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Friends()
        {
            List<Friend> friends = Data.Get.Friends.ToList();

            return View(friends);
        }

        // פונקציה שטעבירה לנתיב ליצירת חבר
        public IActionResult Create()
        {
            return View(new Friend());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddFriend(Friend friend)
        {
            Data.Get.Friends.Add(friend);
            Data.Get.SaveChanges();
            return RedirectToAction("Friends");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Friends");
            }

            Friend? friend = Data.Get.Friends.FirstOrDefault(friend => friend.Id == id);

            if (friend == null)
            {
                return RedirectToAction("Friends");
            }

            return View(friend);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
