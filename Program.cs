using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryCatalog catalogue = new LibraryCatalog();

            DisplayOptions();
            int choice = int.Parse(ReadLine());
            ProcessOption(catalogue, choice);
            while (choice != 8)
            {
                DisplayOptions();
                choice = int.Parse(ReadLine());
                ProcessOption(catalogue, choice);
            }
            ReadLine();
        }

        static void DisplayOptions()
        {
            WriteLine("Choose one of the following options: ");
            WriteLine("1. Add a new book");
            WriteLine("2. Delete a book");
            WriteLine("3. Display book details");
            WriteLine("4. List all books");
            WriteLine("5. Sort catalogue in ascending order of ISBN number, then display list");
            WriteLine("6. Sort catalogue in ascending order of Author then display list");
            WriteLine("7. Sort catalogue in ascending order of publication year, then display list");
            WriteLine("8. Save data and quit");
            Write("Choice: ");
        }

        static void ProcessOption(LibraryCatalog catalogue, int choice)
        {
            WriteLine();

            switch (choice)
            {
                case 1:
                    AddBook(catalogue);
                    WriteLine();
                    break;
                case 2:
                    DeleteBook(catalogue);
                    WriteLine();
                    break;
                case 3:
                    DisplayBookDetail(catalogue);
                    WriteLine();
                    break;
                case 4:
                    catalogue.Display();
                    WriteLine();
                    break;
                case 5:
                    catalogue.SortISBNAsc();
                    catalogue.Display();
                    WriteLine();
                    break;
                case 6:
                    catalogue.SortAuthorAsc();
                    catalogue.Display();
                    WriteLine();
                    break;
                case 7:
                    catalogue.SortPublicationYear();
                    catalogue.Display();
                    WriteLine();
                    break;
                default:
                    WriteLine("Goodbye, the data will now be written to the text file");
                    catalogue.Close();
                    break;
            }
        }
        static void AddBook(LibraryCatalog catalogue)
        {
            WriteLine("Enter an ISBN: ");
            int isbn = int.Parse(ReadLine());
            WriteLine("Enter a title: ");
            string title = ReadLine();
            WriteLine("Enter the author: ");
            string author = ReadLine();
            WriteLine("Enter the publication year: ");
            int year = int.Parse(ReadLine());

            Book newOne = new Book(isbn, title, author, year);
            catalogue.AddOne(newOne);
            WriteLine("The book was added to the list");
        }

        static void DeleteBook(LibraryCatalog catalogue)
        {
            WriteLine("Enter the book you would like to delete: ");
            int wanted = int.Parse(ReadLine());
            int pos = catalogue.FindBook(wanted);
            if(pos <0)
            {
                WriteLine("This book does not exist");
            }
            else
            {
                catalogue.DeleteOne(pos+1);
                WriteLine("This book has been deleted");
            }
        }
    
        static void DisplayBookDetail(LibraryCatalog catalogue)
        {
            WriteLine("Book ISBN: ");
            int wanted = int.Parse(ReadLine());
            int pos = catalogue.FindBook(wanted);

            if (pos == -1)
            {
                WriteLine("Book not found.");
            }
            else
            {
                Book currentOne = catalogue.getBook(pos);
                currentOne.DisplayBook();
                WriteLine("The book is number {0} in the list.", pos +1);
            }
        }
       

    }
}
