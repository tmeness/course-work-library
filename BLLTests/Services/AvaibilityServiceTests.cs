using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tests
{
    [TestClass()]
    public class AvailabilityServiceTests
    {

        private AvailabilityService rs = new AvailabilityService();

        [TestMethod()]
        public void AddAvailabilityTest()
        {
            Assert.ThrowsException<LibraryException>(() => rs.AddAvailability(null, 0, 1), "Book cant be null");
        }

        [TestMethod()]
        public void AddAvailabilityTest1()
        {
            Assert.ThrowsException<LibraryException>(() => rs.AddAvailability(new Book { Availability = true }, 0, 1), "This book is taken");
        }

        [TestMethod()]
        public void AddAvailabilityTest2()
        {
            StudentService studentService = new StudentService("15");
            studentService.Students.Add(new Student { });
            rs.AddAvailability(new Book { }, 0, 1);
        }

        [TestMethod()]
        public void RemoveAvailabilityTest()
        {
            Assert.ThrowsException<LibraryException>(() => rs.RemoveAvailability(null), "Book cant be null");
        }
        [TestMethod()]
        public void RemoveAvailabilityTest1()
        {
            Assert.ThrowsException<LibraryException>(() => rs.RemoveAvailability(new Book { }), "Book cant be null");
        }
        [TestMethod()]
        public void RemoveAvailabilityTest2()
        {
            StudentService studentService = new StudentService("15");
            Student student = new Student { Day = 15 };
            studentService.Students.Add(student);
            rs.RemoveAvailability(new Book { Availability = true , Student = student });
        }
    }
}
