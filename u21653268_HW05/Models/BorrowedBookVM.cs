using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21653268_HW05.Models
{
    public class BorrowedBookVM
    {
        public int BookID { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
        public Book BookObj { get; set; }
    }
}