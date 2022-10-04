using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace u21653268_HW05.Models
{
    public class DataService
    {
        private string ConnectionString;

        public DataService()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        public List<Book> getAllBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select books.bookId as BookID, books.name as BookName, books.pagecount as Pages, books.point as Point, authors.name as AuthorName, authors.surname as AuthorSurname, authors.authorId as AuthorID, types.typeId as TypeID, types.name as TypeName," +
                    " CASE WHEN books.bookId IN" +
                    " (Select borrows.bookId from borrows WHERE borrows.broughtDate IS NULL)" +
                    " THEN 'Out' ELSE 'Available' END AS Status" +
                    " from books INNER JOIN authors on books.authorId = authors.authorId " +
                    " Inner join types on books.typeId = types.typeId" +
                    " ORDER BY books.bookId";
                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book someBook = new Book();
                        someBook.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                        someBook.AuthorName = Convert.ToString(reader["AuthorSurname"]);
                        someBook.BookID = Convert.ToInt32(reader["BookID"]);
                        someBook.TypeID = Convert.ToInt32(reader["TypeID"]);
                        someBook.TypeName = Convert.ToString(reader["TypeName"]);
                        someBook.BookName = Convert.ToString(reader["BookName"]);
                        someBook.PageCount = Convert.ToInt32(reader["Pages"]);
                        someBook.Point = Convert.ToInt32(reader["Point"]);
                        someBook.Status = Convert.ToString(reader["Status"]);


                        books.Add(someBook);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return books;
        }

        public List<Book> getSearchedBooks(string BookName, string AuthorName, string Type)
        {
            List<Book> books = new List<Book>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select books.bookId as BookID, books.name as BookName, books.pagecount as Pages, books.point as Point, authors.name as AuthorName, authors.surname as AuthorSurname, authors.authorId as AuthorID, types.typeId as TypeID, types.name as TypeName," +
                    " CASE WHEN books.bookId IN" +
                    " (Select borrows.bookId from borrows WHERE borrows.broughtDate IS NULL)" +
                    " THEN 'Out' ELSE 'Available' END AS Status" +
                    " from books Inner join authors on books.authorId = authors.authorId " +
                    " Inner join types on books.typeId = types.typeId" +
                    " WHERE books.name LIKE '%" + BookName + "%'" +
                    " AND types.name LIKE '%" + Type + "%'" +
                    " AND authors.surname LIKE '%" + AuthorName + "%'" +
                    " ORDER BY books.bookId";
         
                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book someBook = new Book();
                        someBook.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                        someBook.AuthorName = Convert.ToString(reader["AuthorSurname"]);
                        someBook.BookID = Convert.ToInt32(reader["BookID"]);
                        someBook.TypeID = Convert.ToInt32(reader["TypeID"]);
                        someBook.TypeName = Convert.ToString(reader["TypeName"]);
                        someBook.BookName = Convert.ToString(reader["BookName"]);
                        someBook.PageCount = Convert.ToInt32(reader["Pages"]);
                        someBook.Point = Convert.ToInt32(reader["Point"]);
                        someBook.Status = Convert.ToString(reader["Status"]);


                        books.Add(someBook);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return books;
        }

        public List<Author> getAllAuthors()
        {
            List<Author> authors = new List<Author>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select * from authors";

                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Author someAuthor = new Author();
                        someAuthor.AuthorID = Convert.ToInt32(reader["authorId"]);
                        someAuthor.AuthorName = Convert.ToString(reader["name"]);
                        someAuthor.AuthorSurname = Convert.ToString(reader["surname"]);

                        authors.Add(someAuthor);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return authors;
        }

        public List<Borrow> getAllBorrows()
        {
            List<Borrow> borrows = new List<Borrow>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select * from borrows";
                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Borrow someBorrow = new Borrow();
                        someBorrow.BorrowID = Convert.ToInt32(reader["borrowId"]);
                        someBorrow.BookID = Convert.ToInt32(reader["bookId"]);
                        someBorrow.StudentID = Convert.ToInt32(reader["studentId"]);
                        someBorrow.BroughtDate = Convert.ToDateTime(reader["broughtDate"]);
                        someBorrow.TakenDate = Convert.ToDateTime(reader["takenDate"]);
                        borrows.Add(someBorrow);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return borrows;
        }

        public List<Student> getAllStudents(int BookID)
        {
            List<Student> students = new List<Student>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select students.studentId as StudentID, students.name as StudentName, students.surname as StudentSurname, students.class as Class," +
                    " students.point as Point, students.gender as Gender, students.birthdate as birthdate, CASE WHEN " +
                    " students.studentId IN " +
                    " (Select borrows.studentId from borrows WHERE borrows.broughtDate IS NULL AND borrows.bookId = '" + BookID + "')" +
                    " THEN 'Return' ELSE 'Borrow' END AS Status" +
                    " from students";

                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student someStudent = new Student();
                        someStudent.StudentID = Convert.ToInt32(reader["StudentId"]);
                        someStudent.Point = Convert.ToInt32(reader["Point"]);
                        someStudent.StudentName = Convert.ToString(reader["StudentName"]);
                        someStudent.StudentSurname = Convert.ToString(reader["StudentSurname"]);
                        someStudent.Gender = Convert.ToString(reader["Gender"]);
                        someStudent.Class = Convert.ToString(reader["Class"]);
                        someStudent.BirthDate = Convert.ToDateTime(reader["birthdate"]);
                        someStudent.BorrowStatus = Convert.ToString(reader["Status"]);
                        students.Add(someStudent);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return students;
        }

        public List<Student> getSearchedStudents(string StudentName, string ClassName, int BookID)
        {
            List<Student> students = new List<Student>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select students.studentId as StudentID, students.name as StudentName, students.surname as StudentSurname, students.class as Class," +
                    " students.point as Point, students.gender as Gender, students.birthdate as birthdate, CASE WHEN " +
                    " students.studentId IN " +
                    " (Select borrows.studentId from borrows WHERE borrows.broughtDate IS NULL AND borrows.bookId = '" + BookID + "')" +
                    " THEN 'Return' ELSE 'Borrow' END AS Status" +
                    " From students WHERE students.name LIKE '%" + StudentName + "%'" +
                    " AND students.class Like '%" + ClassName + "%'";
                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student someStudent = new Student();
                        someStudent.StudentID = Convert.ToInt32(reader["StudentID"]);
                        someStudent.StudentName = Convert.ToString(reader["StudentName"]);
                        someStudent.StudentSurname = Convert.ToString(reader["StudentSurname"]);
                        someStudent.Gender = Convert.ToString(reader["Gender"]);
                        someStudent.Class = Convert.ToString(reader["Class"]);
                        someStudent.Point = Convert.ToInt32(reader["Point"]);
                        someStudent.BirthDate = Convert.ToDateTime(reader["birthdate"]);
                        someStudent.BorrowStatus = Convert.ToString(reader["Status"]);

                        students.Add(someStudent);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return students;
        }

        public List<Type> getAllTypes()
        {
            List<Type> types = new List<Type>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select * from types";
                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Type someType = new Type();
                        someType.TypeID = Convert.ToInt32(reader["typeId"]);
                        someType.TypeName = Convert.ToString(reader["name"]);

                        types.Add(someType);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return types;
        }

        public List<StudentClass> getAllStudentClasses()
        {
            List<StudentClass> classes = new List<StudentClass>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select Distinct students.class from students";
                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StudentClass someClass = new StudentClass();
                        someClass.ClassName = Convert.ToString(reader["class"]);

                        classes.Add(someClass);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return classes;
        }


        public List<BorrowedBook> getBorrowedBooks(int BookID)
        {
            List<BorrowedBook> books = new List<BorrowedBook>();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select borrows.borrowId as BorrowId, borrows.takenDate as Taken," +
                    " CASE WHEN borrows.broughtDate IS NULL THEN 'Out' ELSE" +
                    " CONVERT(varchar, borrows.broughtDate) END AS BroughtDate," +
                    " (students.name + ' ' + students.surname) as FullName" +
                    " from borrows Inner join students on borrows.studentId = students.studentId" +
                    " WHERE borrows.bookId = ' " + BookID + "'" +
                    " ORDER BY borrows.borrowId DESC";
                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BorrowedBook someBook = new BorrowedBook();
                        someBook.BorrowID = Convert.ToInt32(reader["BorrowId"]);
                        someBook.TakenDate= Convert.ToString(reader["Taken"]);
                        someBook.BroughtDate = Convert.ToString(reader["BroughtDate"]);
                        someBook.StudentFullName = Convert.ToString(reader["FullName"]);

                        books.Add(someBook);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return books;
        }

        public Book getStatusOfBook(int BookID)
        {
            Book someBook = new Book();
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection currConnection = new SqlConnection(ConnectionString);
                currConnection.Open();


                string sqlQuery = "Select books.bookId as BookId, books.name as BookName," +
                    " CASE WHEN books.bookId IN" +
                    " (Select borrows.bookId from borrows WHERE borrows.broughtDate IS NULL)" +
                    " THEN 'Out' ELSE 'Available' END AS Status" +
                    " from books WHERE books.bookId = '" + BookID +"'";

                SqlCommand command = new SqlCommand(sqlQuery, currConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        someBook.BookID = Convert.ToInt32(reader["BookId"]);
                        someBook.BookName = Convert.ToString(reader["BookName"]);
                        someBook.Status = Convert.ToString(reader["Status"]);
                    }
                }
                currConnection.Close();

            }
            catch
            {

            }
            return someBook;
        }

        public void BorrowBook(int StudentID, int BookID)
        {
            string dateToday = Convert.ToString(DateTime.Now);
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection currConnection = new SqlConnection(ConnectionString);
            currConnection.Open();

            string sqlQuery = "Set IDENTITY_INSERT borrows ON INSERT INTO" +
                    " borrows(borrowId, studentId, bookId, takenDate, broughtDate) VALUES" +
                    " ((Select MAX(borrows.borrowId) + 1 from borrows), " + StudentID + ", " + BookID + ", CONVERT(datetime, '" + dateToday + "'), NULL)";

            SqlCommand command = new SqlCommand(sqlQuery, currConnection);

            command.ExecuteNonQuery();
            currConnection.Close();
        }

        public void ReturnBook(int BorrowID)
        {
            string dateToday = Convert.ToString(DateTime.Now);
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection currConnection = new SqlConnection(ConnectionString);
            currConnection.Open();

            string sqlQuery = "UPDATE borrows SET borrows.broughtDate = CONVERT(datetime, '" + dateToday + "')" +
                " WHERE borrows.borrowId = '" + BorrowID + "'";

            SqlCommand command = new SqlCommand(sqlQuery, currConnection);

            command.ExecuteNonQuery();
            currConnection.Close();
        }

    }
}