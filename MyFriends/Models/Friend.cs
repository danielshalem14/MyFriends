using MyFriends.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFriends.Models
{
    public class Friend
    {

        public Friend()
        {
            Images = new List<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; } = "";

        [Display(Name = "שם משפחה")]
        public string LastName { get; set; } = "";

        [Display(Name = "שם מלא"), NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Display(Name = "מספר טלפון"), Phone]
        public string PhoneNumber { get; set; } = "";

        [Display(Name = "כתובת אימייל"), EmailAddress(ErrorMessage = "כתובת מייל אינה נכונה")]
        public string EmailAddress { get; set; } = "";

        public List<Image> Images { get; set; }
    }
}


