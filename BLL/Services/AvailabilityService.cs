using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AvailabilityService
    {
        public void AddAvailability(Book book, int studentInd, int day)
        {
            if (book == null)
                throw new LibraryException("Book cant be null");
            if (book.Availability)
                throw new LibraryException("This book is taken");
            StudentService.instanse.Edit(studentInd, day);
            book.Student = StudentService.instanse.GetStudent(studentInd);
            book.Availability = true;
        }

        public void RemoveAvailability(Book book)
        {
            if (book == null)
                throw new LibraryException("Book cant be null");
            if(!book.Availability)
                throw new LibraryException("This book is available");
            StudentService.instanse.Students.Find(x => x == book.Student).Day = 0;
            book.Student = null;
            book.Availability = false;
        }
    }
}
