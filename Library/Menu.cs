using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL;

namespace Library
{
    public class Menu
    {
        LibraryService ls = new LibraryService();
        AvailabilityService rs = new AvailabilityService();
        StudentService us = new StudentService();

        private int pars(string number) => !Regex.IsMatch(number, "[0-9]") ? -1 : int.Parse(number) - 1;

        private int StudentInd()
        {
            int count = 1;
            foreach (var a in us.Students)
                Console.WriteLine((count++) + ". Client: " + a.Name + " " + a.Surname);
            Console.Write("Index: ");
            return pars(Console.ReadLine());
        }

        private int LibraryInd()
        {
            int count = 1;
            foreach (var a in ls.Libraries)
                Console.WriteLine((count++) + ". Library: " + a.Name + " Description: " + a.Description);
            Console.Write("Index: ");
            return pars(Console.ReadLine());
        }

        private int BookInd(BLL.Library Library)
        {
            int count = 1;
            foreach (var a in Library.Books)
                Console.WriteLine((count++) + ". Book: " + a.Name );
            Console.Write("Index: ");
            return pars(Console.ReadLine());
        }

        public void Run()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("1. Students\n2. Libraries\n3. Availability\n4. Find\n0. Exit");
                Console.Write("Action: ");
                switch(Console.ReadLine())
                {
                    case "0":
                        ls.Save();
                        us.Save();
                        return;
                    case "1":
                        Students();
                        break;
                    case "2":
                        Libraries();
                        break;
                    case "3":
                        Availability();
                        break;
                    case "4":
                        Find();
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Students()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Students\n1. Add\n2. Remove\n3. Edit\n4. Show all\n5. Sort by name\n6. Sort by surname\n0. Back");
                Console.Write("Action: ");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {

                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Surname: ");
                            us.Add(name, Console.ReadLine());
                        }
                        catch(Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Select client:");
                            us.Remove(StudentInd(), ls.Libraries);
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("Select client:");
                            int ind = StudentInd();
                            Console.Write("New name: ");
                            var name = Console.ReadLine();
                            Console.Write("New surname: ");
                            us.Edit(ind, name, Console.ReadLine(), ls.Libraries);
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "4":
                        foreach (var a in us.Students)
                            Console.WriteLine(a);
                        Console.ReadKey();
                        break;
                    case "5":
                        foreach (var a in us.SortByName())
                            Console.WriteLine(a);
                        Console.ReadKey();
                        break;
                    case "6":
                        foreach (var a in us.SortBySurname())
                            Console.WriteLine(a);
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();                            
                        break;
                }
            }
        }

        private void Libraries()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Libraries\n1. Add\n2. Remove\n3. Show all\n4. Add Book\n5. Remove book\n6. Show concrate Library\n0. Back");
                Console.Write("Action: ");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {

                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.Write("Name: ");
                            var name = Console.ReadLine();
                            Console.Write("Description: ");
                            ls.AddLibrary(name, Console.ReadLine());
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Select Library:");
                            ls.RemoveLibrary(LibraryInd());
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        foreach (var a in ls.Libraries)
                            Console.WriteLine("Library: " + a.Name + " Description: " + a.Description);
                        Console.ReadKey();
                        break;
                        case "4":
                        try
                        {
                            Console.WriteLine("Select Library:");
                            int ind = LibraryInd();
                            Console.Write("Book number: ");
                            var name = Console.ReadLine();
                            Console.Write("Number of place: ");
                            var nop = Console.ReadLine();
                            Console.Write("Price for day: ");
                            ls.AddBook(ind, name, pars(nop) + 1, pars(Console.ReadLine()));

                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "5":
                        try
                        {
                            Console.WriteLine("Select Library:");
                            int ind = LibraryInd();
                            Console.WriteLine("Select Book:");
                            ls.RemoveBook(ind, BookInd(ls.GetLibrary(ind)));

                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                  
                    case "6":
                        try
                        {
                            Console.WriteLine("Select Library:");
                            Console.WriteLine(ls.ShowLibrary(LibraryInd())) ;
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); }
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Availability()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Students\n1. Add\n2. Remove\n3. Show concrate\n4. Show all Availability\n5. Show all unavailable\n0. Back");
                Console.Write("Action: ");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {

                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.WriteLine("Select Library");
                            int hind = LibraryInd();
                            Console.WriteLine("Select Book");
                            int rInd = BookInd(ls.GetLibrary(hind));
                            Console.WriteLine("Select client");
                            int ind = StudentInd();
                            Console.Write("Days: ");
                            rs.AddAvailability(ls.GetBook(hind, rInd), ind, int.Parse(Console.ReadLine()));
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Select Library");
                            int hind = LibraryInd();
                            Console.WriteLine("Select Book");
                            int rInd = BookInd(ls.GetLibrary(hind));
                            rs.RemoveAvailability(ls.GetBook(hind, rInd));
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("Select Library");
                            int hind = LibraryInd();
                            Console.WriteLine("Select Book");
                            int rInd = BookInd(ls.GetLibrary(hind));
                            var a = ls.GetBook(hind, rInd);
                            string str = a.Availability ? "Book: " + a.Name + " Is taken by: " + a.Student.Name + " " +a.Student.Surname  : "Book: " + a.Name + " Availability: " + a.Availability;
                            Console.WriteLine(str); Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "4":
                        try
                        {
                            Console.WriteLine("Select Library");
                            int hind = LibraryInd();
                            Console.WriteLine("All unavailable place");
                            foreach (var a in ls.GetLibrary(hind).Books)
                                if (a.Availability)
                                    Console.WriteLine("Book: " + a.Name + " Is taken by: " + a.Student.Name + " " + a.Student.Surname);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "5":
                        try
                        {
                            Console.WriteLine("Select Library");
                            int hind = LibraryInd();
                            Console.WriteLine("All unavailable place");
                            foreach (var a in ls.GetLibrary(hind).Books)
                                if (!a.Availability)
                                    Console.WriteLine("Book: " + a.Name);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void Find()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Clients\n2. Libraries\n0. Exit");
                Console.Write("Action: ");
                switch (Console.ReadLine())
                {
                    case "0":
                        return;
                    case "1":
                        try
                        {
                            Console.Write("Key word: ");
                            foreach (var a in us.Find(Console.ReadLine()))
                                Console.WriteLine(a);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    case "2":
                        try
                        {
                            Console.Write("Key word: ");
                            foreach (var a in ls.Find(Console.ReadLine()))
                                Console.WriteLine(a);
                            Console.ReadKey();
                        }
                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                        break;
                    default:
                        Console.WriteLine("Wrong index\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
