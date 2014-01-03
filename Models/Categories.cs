using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testme.Models
{
    public class Categories
    {
        public int RecordID { get; set; }
        public bool Active { get; set; }
        public string CategoryName { get; set; }
        public int UserID { get; set; }
        public DateTime AddedDate { get; set; }
        public int LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}