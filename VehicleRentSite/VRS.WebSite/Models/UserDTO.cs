using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VRS.WebSite.Models
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public char Sex { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
    }
}