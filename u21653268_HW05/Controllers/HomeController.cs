using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21653268_HW05.Models;

namespace u21653268_HW05.Controllers
{
    public class HomeController : Controller
    {
        private DataService dataService = new DataService();
        public ActionResult Index()
        {
            BookAuthorType bookAuthorTypeObject = new BookAuthorType();
            bookAuthorTypeObject.Books = dataService.getAllBooks();
            bookAuthorTypeObject.Authors = dataService.getAllAuthors();
            bookAuthorTypeObject.Types = dataService.getAllTypes();

            return View(bookAuthorTypeObject);
        }
        [HttpPost]
        public ActionResult Index(string BookName, string AuthorName, string Type)
        {
            BookAuthorType bookAuthorTypeObject = new BookAuthorType();
            bookAuthorTypeObject.Books = dataService.getSearchedBooks(BookName, AuthorName, Type);
            bookAuthorTypeObject.Authors = dataService.getAllAuthors();
            bookAuthorTypeObject.Types = dataService.getAllTypes();

            return View(bookAuthorTypeObject);
        }

        public ActionResult Students(int BookID)
        {
            StudentClassVM studentClassVMObject = new StudentClassVM();
            studentClassVMObject.Students = dataService.getAllStudents(BookID);
            studentClassVMObject.Classes = dataService.getAllStudentClasses();
            studentClassVMObject.BookID = BookID;

            return View(studentClassVMObject);
        }

        [HttpPost]
        public ActionResult Students(string StudentName, string ClassName, int BookID)
        {
            StudentClassVM studentClassVMObject = new StudentClassVM();
            studentClassVMObject.Students = dataService.getSearchedStudents(StudentName, ClassName, BookID);
            studentClassVMObject.Classes = dataService.getAllStudentClasses();
            studentClassVMObject.BookID = BookID;

            return View(studentClassVMObject);
        }

        public ActionResult BookDetails(int BookID)
        {
            List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();
            borrowedBooks = dataService.getBorrowedBooks(BookID);

            BorrowedBookVM borrowedBookVMObject = new BorrowedBookVM();
            borrowedBookVMObject.BorrowedBooks = borrowedBooks;
            borrowedBookVMObject.BookObj = dataService.getStatusOfBook(BookID);
            borrowedBookVMObject.BookID = BookID;

            return View(borrowedBookVMObject);
        }

        public ActionResult Borrow(int StudentID, int BookID)
        {
            dataService.BorrowBook(StudentID, BookID);
            return RedirectToAction("Index");
        }

        public ActionResult Return(int BorrowID)
        {
            dataService.ReturnBook(BorrowID);
            return RedirectToAction("Index");
        }
    }
}