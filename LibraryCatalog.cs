using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Library
{
    internal class LibraryCatalog
    {
        ArrayList BookList;
        const char DELIM = ',';
        String BookDataFile;
        int sortedState = 0;

        public LibraryCatalog()
        {
            BookList = new ArrayList();
            BookDataFile = "BookData.txt";
            ReadData();
            sortedState = 0;
        }
        private int BinarySearchISBN(int wanted)
        {
            int first = 0;
            int last = BookList.Count - 1;
            while (first <= last)
            {
                int mid = (first + last) / 2;
                Book currentOne = (Book)BookList[mid];

                if (currentOne.getISBN() == wanted)
                    return mid;
                else if (currentOne.getISBN() < wanted)
                    first = mid + 1;
                else
                    last = mid - 1;
            }
            return -1;
        }

        private int LinearSearchISBN(int wanted)
        {
            int pos = -1;
            bool found = false;
            int counter = 0;

            while (!found && counter < BookList.Count)
            {
                Book currentOne = (Book)BookList[counter];

                if (currentOne.getISBN() == wanted)
                {
                    found = true;
                    pos = counter;
                }
                counter++;
            }
            return pos;
        }
        private void swop(int swop1, int swop2)
        {
            Book temp = (Book)BookList[swop2];
            BookList[swop2] = (Book)BookList[swop1];
            BookList[swop1] = temp;

        }
        private void BubbleSortAscISBN()
        {
            for (int pass = 1; pass < BookList.Count;  pass++)
            {
                for (int compare = 1; compare < BookList.Count; compare++)
                {
                    Book first = (Book)BookList[compare - 1];
                    Book second = (Book)BookList[compare];

                    if(first.getISBN() > second.getISBN())
                    {
                        swop(compare - 1, compare);
                    }
                }
                
            }
        }
        private void SelectionSortAscAuthor()
        {
            int minPos = 0;

            for (int pass =1; pass <BookList.Count; pass++)
            {
                for (int x =pass; x <BookList.Count; x++)
                {
                    Book first = (Book)BookList[x];
                    Book second = (Book)BookList[minPos];

                    if (first.getAuthor().CompareTo(second.getAuthor()) < 0)
                    {
                        minPos = x;
                    }
                }
                swop(pass - 1, minPos);
                minPos = pass;
            }
        }
        private void InsertionSortAscPublicationYear()
        {
            for (int pass =1; pass <BookList.Count; pass++)
            {
                Book newBook = (Book)BookList[pass];
                int curPos = pass - 1;
                Book currentBook = (Book)BookList[curPos];
   

                while ((curPos != -1) && (newBook.getPublicationYear() < currentBook.getPublicationYear()))
                {

                curPos--;

                    if (curPos != -1)
                    {
                        currentBook = (Book)BookList[curPos];
                    }

                }
            BookList.RemoveAt(pass);
            BookList.Insert(curPos + 1, newBook);

             }
        }

        private void ReadData()
        {
            StreamReader sr = new StreamReader(BookDataFile);
            int isbn, publicationYear; ;
            String title, author;

            String[] books;
            String bookLine = sr.ReadLine();

            while (bookLine != null)
            {
                books = bookLine.Split(DELIM);
                isbn = int.Parse(books[0]);
                title = books[1];
                author = books[2];
                publicationYear = int.Parse(books[3], CultureInfo.InvariantCulture);

                Book newBook = new Book (isbn, title, author, publicationYear);
                AddOne(newBook);
                bookLine = sr.ReadLine();
            }
            sr.Close();
        }
        private void WriteData()
        {
            StreamWriter sw = new StreamWriter(BookDataFile);
            for (int x = 0; x < BookList.Count; x++)
            {
                Book tempBook= (Book)BookList[x];
                sw.WriteLine(tempBook.getISBN() + "," +
                tempBook.getTittle() + ","+
                tempBook.getAuthor() + ","+
                tempBook.getPublicationYear());
                
            }
            sw.Close();

        }
        public void Close()
        {
            WriteData();
        }
      
        public Book getBook(int pos)
        {
            if ((pos >= 0) && (pos <= BookList.Count ))
                return (Book)BookList[pos];
            else return null;
        }
        public void Display()
        {
            for (int x = 0; x <= BookList.Count - 1; x++)
            {
                Book currentBook = (Book)BookList[x];
                currentBook.DisplayBook();
            }
        }

        public void AddOne(Book newBook)
        {
            BookList.Add(newBook);
            sortedState = 0;
        }
        public void DeleteOne(int pos)
        {
            BookList.RemoveAt(pos - 1);
            sortedState = 0;
        }
        public int FindBook(int wanted)
        {

            if (sortedState == 1)
            {
                Console.WriteLine("Using binary search");
                return BinarySearchISBN(wanted);
            }
            else
            {
                WriteLine("Using linear search");
                return LinearSearchISBN(wanted);
            }
        }
        public void DisplayAll()
        {
            for (int x = 0; x <= BookList.Count - 1; x++)
            {
                Book currentBook = (Book)BookList[x];
                currentBook.DisplayBook();
            }
        }

        public void SortISBNAsc()
        {
            BubbleSortAscISBN();
            sortedState = 1;
        }
        public void SortAuthorAsc()
        {
            SelectionSortAscAuthor();
            sortedState = 2;
        }
        public void SortPublicationYear()
        {
            InsertionSortAscPublicationYear();
            sortedState = 3;
        }
    }
}
