using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class LibraryService
    {
        public List<Library> Libraries { get; set; } = new List<Library>();
        private Serialize<Library> serialize;

        public LibraryService(string name) { }

        public LibraryService()
        {
            serialize = new Serialize<Library>("Library");
            try { Libraries = serialize.Load().ToList(); }
            catch { serialize.Save(Libraries.ToArray()); }
        }

        public void AddLibrary(string name, string description)
        {
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new LibraryException("Name can`t be null");
            if (description == null || String.IsNullOrEmpty(description.Trim()))
                throw new LibraryException("Description can`t be null");
            Libraries.Add(new Library { Name = name, Description = description });
        }

        public void RemoveLibrary(int ind)
        {
            if (ind < 0 || ind >= Libraries.Count)
                throw new LibraryException("Index out of range");
            foreach (var room in Libraries[ind].Books)
                foreach (var user in StudentService.instanse.Students)
                    if (user == room.Student)
                        user.Day = 0;
            Libraries.RemoveAt(ind);
        }

        public void EditLibrary(int ind, string name, string description)
        {

            if (ind < 0 || ind >= Libraries.Count)
                throw new LibraryException("Index out of range");
            if (name == null || String.IsNullOrEmpty(name.Trim()))
                throw new LibraryException("Name can`t be null");
            if (description == null || String.IsNullOrEmpty(description.Trim()))
                throw new LibraryException("Description can`t be null");
            Libraries[ind].Name = name;
            Libraries[ind].Description = description;
        }

        public void AddBook(int ind, string number, int place, int price)
        {
            if (ind < 0 || ind >= Libraries.Count)
                throw new LibraryException("Index out of range");
            if (!Regex.IsMatch(number, "[0-9]{3}"))
                throw new LibraryException("Wrong book number");
            if (place < 0 || place > 4)
                throw new LibraryException("Invalid number of place");
            if(price < 0)
                throw new LibraryException("Price can`t be less than 0");
            Libraries[ind].Books.Add(new Book { Name = number, Library = Libraries[ind] });
        }

        public void RemoveBook(int hotelInd, int roomInd)
        {
            if(hotelInd < 0 || hotelInd >= Libraries.Count)
                throw new LibraryException("Index out of range");
            if(roomInd < 0 || roomInd >= Libraries[hotelInd].Books.Count)
                throw new LibraryException("Index out of range");
            foreach (var user in StudentService.instanse.Students)
                if (user == Libraries[hotelInd].Books[roomInd].Student)
                    user.Day = 0;
            Libraries[hotelInd].Books.RemoveAt(roomInd);
        }

        public Library GetLibrary(int ind) => ind < 0 || ind >= Libraries.Count ? throw new LibraryException("Index out of range") : Libraries[ind];

        public Book GetBook(int ind, int rInd)
        {
            if (ind < 0 || ind >= Libraries.Count)
                throw new LibraryException("Index out of range");
            if (rInd < 0 || rInd >= Libraries[ind].Books.Count)
                throw new LibraryException("Index out of range");
            return Libraries[ind].Books[rInd];
        }

        public List<Library> Find(string key)
        {
            var temp = Libraries.FindAll(x => x.Name == key);
            if (temp != null)
                return temp;
            else
            {
                temp = Libraries.FindAll(x => x.Description == key);
                if (temp != null)
                    return temp;
                else throw new LibraryException("You have no users that containt key word: " + key);
            }
        }

        public string ShowLibrary(int ind)
        {
            if (ind < 0 || ind >= Libraries.Count)
                throw new LibraryException("Index out of range");
            string str = "";
            int all = 0;
            str +="Library: "+ Libraries[ind].Name + "\n";
            foreach(var a in Libraries[ind].Books)
            {
               
                str += "Book: " + a.Name + " Is Taken: " + a.Availability + "\n";
            }
            str += "All place: " + all;
            return str;
        }

        public void Save() => serialize.Save(Libraries.ToArray());
    }
}
