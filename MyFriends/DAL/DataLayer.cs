using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.DAL
{
    // קלאס שמייצג את שכבת הנתונים, יורש מקלאס בשם DbContext
    public class DataLayer : DbContext
    {
        // קונסטרקטור שמקבל מחרוזת חיבור ומעביר אותה לקונסטרקטור של הקלאס האב
        public DataLayer(string connectionString) : base(GetOptions(connectionString))
        {
            Database.EnsureCreated();

            // להכניס נתונים בפעם הראשונה
            Seed();
        }

        private void Seed()
        {
            if (Friends.Count() > 0)
            {
                return;
            }

            Friend firstFriend = new Friend();
            firstFriend.FirstName = "מני";
            firstFriend.LastName = "לוי";
            firstFriend.EmailAddress = "meni@meno.com";
            firstFriend.PhoneNumber = "";

            Friends.Add(firstFriend);
            SaveChanges();
        }


        public DbSet<Friend> Friends { get; set; }

        public DbSet<Image> Images { get; set; }

        // פונקציה שמחזירה את אפשרויות ההתחברות למסד הנתונים
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), connectionString)
                .Options;
        }

    }
}




// DataLayer dataLayer = new DataLayer("my coonection string");