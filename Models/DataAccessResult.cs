using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testme.Models
{
    public class DataAccessResult
    {
        public bool IsError { get; set; }
        public string TransactionDetails { get; set; }
        public string UserMessage { get; set; }
    }
}