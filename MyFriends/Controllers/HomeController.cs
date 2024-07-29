using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // פונקציה שמחזירה רשימת חברים לדף התצוגה
        public IActionResult Friends()
        {
            List<Friend> friends = Data.Get.Friends.ToList();
            return View(friends);
        }

        // פונקציה שמחזירה תצוגה ליצירת חבר חדש
        public IActionResult Create()
        {
            return View(new Friend());
        }

        // פונקציה שמוסיפה חבר חדש לרשימה ושומרת את השינויים
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddFriend(Friend friend)
        {
            Data.Get.Friends.Add(friend);
            Data.Get.SaveChanges();
            return RedirectToAction("Friends");
        }

        // פונקציה שמחזירה את הדף הראשי
        public IActionResult Index()
        {
            return View();
        }

        // פונקציה שמחזירה את דף הפרטיות
        public IActionResult Privacy()
        {
            return View();
        }

        // פונקציה שמחזירה תצוגת פרטים על חבר לפי מזהה
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Friends");
            }

            Friend? friend = Data.Get.Friends.Include(f => f.Images).ToList().FirstOrDefault(friend => friend.Id == id);

            if (friend == null)
            {
                return RedirectToAction("Friends");
            }

            return View(friend);
        }

        // פונקציה שמעדכנת פרטים של חבר קיים ושומרת את השינויים
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateFriend(Friend newFriend)
        {
            if (newFriend == null)
            {
                return RedirectToAction("Friends");
            }

            Friend? existingFriend = Data.Get.Friends.FirstOrDefault(f => f.Id == newFriend.Id);

            if (existingFriend == null)
            {
                return RedirectToAction("Friends");
            }

            // עדכון ערכי החבר הקיים בערכים החדשים
            Data.Get.Entry(existingFriend).CurrentValues.SetValues(newFriend);

            // שמירת השינויים
            Data.Get.SaveChanges();

            return RedirectToAction("Friends");
        }

        // פונקציה שמחזירה תצוגת עריכה לחבר לפי מזהה
        public IActionResult Edit(int? id)
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

        // פונקציה שמוחקת חבר לפי מזהה ושומרת את השינויים
        public IActionResult DeleteFriend(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Friend> friendList = Data.Get.Friends.ToList();
            Friend? friendToRemove = friendList.Find(friend => friend.Id == id);

            if (friendToRemove == null)
            {
                return NotFound();
            }

            Data.Get.Friends.Remove(friendToRemove);
            Data.Get.SaveChanges();
            return RedirectToAction(nameof(Friends)); // "Friends"
        }


        // פונקציה להוספת תמונה לחבר קיים במערכת
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddNewImage(Friend friendFromRequest)
        {
            Friend? friendFromDb = Data.Get.Friends.FirstOrDefault(f => f.Id == friendFromRequest.Id);

            if (friendFromDb == null)
            {
                return NotFound();
            }

            byte[]? firstImage = friendFromRequest.Images.Last().MyImage;

            if (firstImage == null)
            {
                return NotFound();
            }

            friendFromDb.AddImage(firstImage);
            Data.Get.SaveChanges();
            return RedirectToAction("Details", new { id = friendFromDb.Id });
        }


        // פונקציה שמחזירה דף שגיאה עם פרטי הבקשה
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
