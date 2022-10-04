using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21653268_HW05.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string TypeName { get; set; }
        public int PageCount { get; set; }
        public int Point { get; set; }
        public int AuthorID { get; set; }
        public int TypeID { get; set; }
        public string Status { get; set; }

    }
}