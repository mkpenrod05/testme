using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testme.Models
{
    public class User
    {
        private bool _isLoggedIn;
        
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime AddedDate { get; set; }
        public int LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { _isLoggedIn = value; }
        }
    }
}