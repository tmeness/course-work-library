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
    public class StudentServiceTests
    {
        StudentService us = new StudentService("");

        [TestMethod()]
        public void AddTest()
        {
            Assert.ThrowsException<LibraryException>(() => us.Add("", "sa"), "Name can`t be null");
        }

        [TestMethod()]
        public void AddTest1()
        {
            Assert.ThrowsException<LibraryException>(() => us.Add("sa", ""), "Surname can`t be null");
        }

        [TestMethod()]
        public void AddTest2()
        {
            us.Add("sa", "sa");
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.ThrowsException<LibraryException>(() => us.Remove(-1, new List<Library> { }), "Index out of range");
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            var a = new Student { Name = "d" };
            us.Students.Add(a);
            us.Students.Add(new Student { });
            us.Remove(0, new List<Library> { new Library { Books = new List<Book> { new Book { Student = a, Availability = true } } } });
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.ThrowsException<LibraryException>(() => us.Edit(-1, "sa", "sa", new List<Library>()), "Index out of range");
        }

        [TestMethod()]
        public void EditTest1()
        {
            us.Students.Add(new Student { });
            Assert.ThrowsException<LibraryException>(() => us.Edit(0, "", "sa", new List<Library>()), "Name can`t be null");
        }

        [TestMethod()]
        public void EditTest2()
        {
            us.Students.Add(new Student { });
            Assert.ThrowsException<LibraryException>(() => us.Edit(0, "фів", "", new List<Library>()), "Surname can`t be null");
        }

        [TestMethod()]
        public void EditTest3()
        {
            var a = new Student { Name = "d" };
            us.Students.Add(a);
            us.Students.Add(new Student { });
            us.Edit(0, "das", "sa", new List<Library> { new Library { Books = new List<Book> { new Book { Student = a, Availability = true } } } });
        }

        [TestMethod()]
        public void Edit1Test()
        {
            Assert.ThrowsException<LibraryException>(() => us.Edit(-1, 0), "Index out of range");
        }

        [TestMethod()]
        public void Edit1Test1()
        {
            us.Students.Add(new Student { });
            Assert.ThrowsException<LibraryException>(() => us.Edit(0, -1), "Day can`t be less then 0");
        }

        [TestMethod()]
        public void Edit1Test2()
        {
            us.Students.Add(new Student { Day = 15});
            Assert.ThrowsException<LibraryException>(() => us.Edit(0, 50), "Surname can`t be null");
        }

        [TestMethod()]
        public void Edit1Test3()
        {
            var a = new Student { Name = "d" };
            us.Students.Add(a);
            us.Students.Add(new Student { });
            us.Edit(0, 15);
        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.ThrowsException<LibraryException>(() => us.GetStudent(-1), "Index out of range");
        }
    }
}