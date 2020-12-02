using System;


namespace pa5_sdaudet_ua
{
    public class Books
    {
        private string ISBN; 
        private string title;
        private string author;
        private string genre;
        private int listenTime;
        private int totalStock;
        private int currentStock;
        private static int titleCount;

        public Books(string inISBN, string inTitle, string inAuthor, string inGenre, int inListenTime) //Constructor for UI adding
        {
            ISBN = inISBN;
            title = inTitle;
            author = inAuthor;
            genre = inGenre;
            listenTime = inListenTime;
            totalStock = 1;
            currentStock = 1;
        }
        public Books(string inISBN, string inTitle, string inAuthor, string inGenre, int inListenTime, int inTotalStock, int inCurrentStock)//Constructor for file import
        {
            ISBN = inISBN;
            title = inTitle;
            author = inAuthor;
            genre = inGenre;
            listenTime = inListenTime;
            totalStock = inTotalStock;
            currentStock = inCurrentStock;
        }
        public string GetISBN()//Accessor method
        {
            return ISBN;
        }
        public string GetTitle()//Accessor method
        {
            return title;
        }
        public string GetAuthor()//Accessor method
        {
            return author;
        }
        public string GetGenre()//Accessor method
        {
            return genre;
        }
        public int GetListenTime()//Accessor method
        {
            return listenTime;
        }
        public int GetTotalStock()//Accessor method
        {
            return totalStock;
        }
        public int GetCurrentStock()//Accessor method
        {
            return currentStock;
        }
        public void IncTotalStock()//Increment Total Stock by 1
        {
            totalStock++;
        }
        public void IncCurrentStock()//Increment current stock by 1
        {
            currentStock++;
        }
        public void DecCurrentStock()//Decrement current stock by 1
        {
            currentStock--;
        }
        public static void IncCount()//Increment class-wide book counter
        {
            titleCount++;
        }
        public static int GetCount()//Return class-wide book counter
        {
            return titleCount;
        }
        public static void InitCount()//Initialize class-wide book counter
        {
            titleCount = 0;
        }
        public void EditBook()//Method that prints current book info and allows user to change specific values. 
        {
            bool done = false;
            while(!done)
            {
                //Print options to console display.
                Console.Clear();
                Console.WriteLine($"ISBN: {ISBN}\nTitle: {title}\nAuthor: {author}\n Genre: {genre}\nListen time: {listenTime}\nTotal Stock: {totalStock}\nCurrent Stock: {currentStock}");
                Console.WriteLine("What value would you like to change?\n");
                Console.WriteLine("1:   ISBN");
                Console.WriteLine("2:   Title");
                Console.WriteLine("3:   Author");
                Console.WriteLine("4:   Genre");
                Console.WriteLine("5:   Listen Time");
                Console.WriteLine("6:   Total Stock");
                Console.WriteLine("7:   Current Stock");
                Console.WriteLine("\nENTER: NO CHANGE\n");
                Console.Write("Choice: ");
                string input = Console.ReadLine();
            
                switch(input)
                {//This switch allows users to edit the fields of a book. Or to view them and make no changes. All cases are very simple and self-explanatory. 
                    case "1":
                    Console.Write("Please enter the new value: ");
                    ISBN = Console.ReadLine();
                    break;
                    
                    case "2":
                    Console.Write("Please enter the new value: ");
                    title = Console.ReadLine();
                    break;

                    case "3":
                    Console.Write("Please enter the new value: ");
                    author = Console.ReadLine();
                    break;

                    case "4":
                    Console.Write("Please enter the new value: ");
                    genre = Program.SelectGenre();
                    break;

                    case "5":
                    Console.Write("Please enter the new value (Must be numerical): ");
                    listenTime = int.Parse(Console.ReadLine());
                    break;

                    case "6":
                    Console.Write("Please enter the new value (Must be numerical): ");
                    totalStock = int.Parse(Console.ReadLine());
                    if (currentStock > totalStock)
                    {
                        currentStock = totalStock;
                        Console.WriteLine($"\n\n***Value was lower than current stock, so total Stock and current stock are now: {currentStock}. Press any key to continue. ");
                        Console.ReadKey();
                    }
                    break;

                    case "7":
                    Console.Write("Please enter the new value (Must be numerical): ");
                    currentStock = int.Parse(Console.ReadLine());
                    if (currentStock > totalStock)
                    {
                        totalStock = currentStock;
                        Console.WriteLine($"\n\n***Value was larger than total stock, so total Stock and current stock are now: {totalStock}. Press any key to continue. ");
                        Console.ReadKey();
                    }
                    break;

                    case "8":
                    done = true;
                    break;

                    default:
                    done = true;
                    break;
                }
            }
        }
    }
}