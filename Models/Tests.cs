using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testme.Models
{
    public class Tests
    {
        public int RecordID { get; set; }
        public int UserID { get; set; }
        public bool Active { get; set; }
        public int CategoryID { get; set; }
        public string TestName { get; set; }
        public DateTime AddedDate { get; set; }
        public int LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}