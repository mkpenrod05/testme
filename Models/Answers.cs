using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testme.Models
{
    public class Answers
    {
        public int RecordID { get; set; }
        public int UserID { get; set; }
        public bool Active { get; set; }
        public int QuestionID { get; set; }
        public string AnswerSubject { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public DateTime AddedDate { get; set; }
        public int LastModifiedByID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}