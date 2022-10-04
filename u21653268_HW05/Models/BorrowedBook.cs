using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21653268_HW05.Models
{
    public class BorrowedBook
    {
        public int BorrowID { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
        public string StudentFullName { get; set; }
        public string Status { get; set; }
    }
}