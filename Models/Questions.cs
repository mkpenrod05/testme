using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace testme.Models
{
    public class Questions
    {
        public int RecordID { get; set; }
        public int UserID { get; set; }
        public bool Active { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionSubject { get; set; }
        public int Difficulty { get; set; }
        public int TestID { get; set; }
        public DateTime AddedDate { get; set; }
        public int LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}