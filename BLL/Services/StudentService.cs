using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class StudentService
    {
        public List<Student> Students { get; private set; } = new List<Student>();
        Serialize<Student> serialize;
        public static StudentService instanse;

        public StudentService(string name)
        {
            instanse = this;
        }

        public StudentService()
        {
            instanse = this;
            serialize = new Serialize<Student>("Students");
            try { Students = serialize.Load().ToList(); }
            catch { serialize.Save(Students.ToArray());  }
        }

        public void Add(string name, string surname)
        {
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new LibraryException("Name can`t be null");
            if (surname == null || String.IsNullOrEmpty(surname.Trim()))
                throw new LibraryException("Surname can`t be null");
            Students.Add(new Student { Name = name, Surname = surname });
        }

        public void Remove(int ind, List<Library> libraries)
        {
            if (ind < 0 || ind >= Students.Count)
                throw new LibraryException("Index out of range");
            foreach (var library in libraries)
                foreach (var book in library.Books)
                    if (book.Student == Students[ind])
                    {
                        book.Student = null;
                        book.Availability = false;
                    }
            Students.RemoveAt(ind);
        }

        public void Edit(int ind, string name, string surname, List<Library> libraries)
        {
            if (ind < 0 || ind >= Students.Count)
                throw new LibraryException("Index out of range");
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new LibraryException("Name can`t be null");
            if (surname == null || String.IsNullOrEmpty(surname.Trim()))
                throw new LibraryException("Surname can`t be null");
            foreach (var library in libraries)
                foreach (var room in library.Books)
                    if (room.Student == Students[ind])
                    {
                        room.Student.Name = name;
                        room.Student.Surname = surname;
                    }
            Students[ind].Name = name;
            Students[ind].Surname = surname;
        }

        public void Edit(int ind, int day)
        {
            if (ind < 0 || ind >= Students.Count)
                throw new LibraryException("Index out of range");
            if (day < 0)
                throw new LibraryException("Day can`t be less then 0");
            if (Students[ind].Day != 0)
                throw new LibraryException("This user have alredy reserve room");
            Students[ind].Day = day;
        }

        public List<Student> SortByName()
        {
            var temp = Students;
            for(int i = 0; i < Students.Count - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Students.Count; j++)
                    if (temp[min].Name.CompareTo(temp[j].Name) > 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;
            }
            return temp;
        }

        public List<Student> SortBySurname()
        {
            var temp = Students;
            for (int i = 0; i < Students.Count - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Students.Count; j++)
                    if (temp[min].Name.CompareTo(temp[j].Name) > 0)
                        min = j;
                var a = temp[min];
                temp[min] = temp[i];
                temp[i] = a;
            }
            return temp;
        }

        public List<Student> Find(string key)
        {
            if (Regex.IsMatch(key, "[0-9]+"))
                return Students.FindAll(x => x.Day == int.Parse(key));
            var temp = Students.FindAll(x => x.Name == key);
            if (temp != null)
                return temp;
            else
            {
                temp = Students.FindAll(x => x.Surname == key);
                if (temp != null)
                    return temp;
                else throw new LibraryException("You have no students that containt key word: " + key);
            }
        }

        public Student GetStudent(int ind)
        {
            if (ind < 0 || ind >= Students.Count)
                throw new LibraryException("Index out of range");
            return Students[ind];
        }

        public void Save() => serialize.Save(Students.ToArray());
    }
}