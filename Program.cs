using System;

namespace pa5_sdaudet_ua
{
    class Program
    {
        static void Main(string[] args)
        {
            Books.InitCount();
            Transaction.InitCount();
            BookFile workingFile = new BookFile("books.txt");
            Books[] catalogArray = workingFile.GetBooksFromFile();
            TransactionFile transLog = new TransactionFile("transactions.txt");
            Transaction[] transArray = transLog.GetTransactionsFromFile();            

            int exit = 0;
            while (exit == 0)
            {
                int userChoice = ChooseProgram();
                //This is the selection block for the main menu.  
                switch(userChoice)
                {
                    case 1:
                    Console.Clear();
                    AddBook(catalogArray);
                    break;

                    case 2:
                    Console.Clear();
                    EditBook(catalogArray);
                    break;

                    case 3:
                    Console.Clear();
                    RentBook(catalogArray, transArray);
                    break;

                    case 4:
                    Console.Clear();
                    ReturnBook(catalogArray, transArray);
                    break;

                    case 5:
                    workingFile.SaveBooksToFile(catalogArray);
                    transLog.SaveToFile(transArray);
                    break;

                    case 6:
                    Console.Clear();
                    exit=1;
                    break;

                    case 7:
                    PrintCatalog(catalogArray);
                    Console.WriteLine();
                    PrintTransactions(transArray);
                    break;

                    case 8:
                    Console.WriteLine($"BookCount: {Books.GetCount()}");
                    Console.WriteLine($"TransCount: {Transaction.GetTransCount()}");
                    Console.ReadKey();
                    break;
                }
            }
        }
        static int ChooseProgram()
        {
            //This method displays the main menu and returns the chosen value to the main method for selection.
            Console.Clear();
            Console.Write("\n");
            Console.WriteLine("1:   Add a book.");
            Console.WriteLine("2:   Edit a book.");
            Console.WriteLine("3:   Rent a book.");
            Console.WriteLine("4:   Return a book.");
            Console.WriteLine("5:   Exit\n\n");
            return int.Parse(Console.ReadLine());
        }
        static void AddBook(Books[] catalogArray)
        {
            Console.Clear();
            Console.WriteLine("**Add a Book**\n\n\n");
            Console.WriteLine("Please enter an ISBN: ");
            string ISBN = Console.ReadLine();
            int index = SearchBooks(catalogArray,ISBN);
            if (index==-1 && Books.GetCount() > 0)
            {
                Books.IncCount();
                Console.WriteLine("Please enter the Title of the book: ");
                string title = Console.ReadLine();
                Console.WriteLine("Please enter the Author of the book: ");
                string author = Console.ReadLine();
                Console.WriteLine("Please enter the Genre of the book: ");
                string genre = SelectGenre();
                Console.WriteLine("Please enter the listening time of the book (in minutes): ");
                int listenTime = int.Parse(Console.ReadLine());
                catalogArray[Books.GetCount()-1] = new Books(ISBN, title, author, genre, listenTime, 1, 1);
                Console.WriteLine("Book added to inventory, press any key to continue.");
            }
            else
            {
                catalogArray[index].IncCurrentStock();
                catalogArray[index].IncTotalStock();
                Console.WriteLine("ISBN already exists. Total and current stock quantities have been updated. Press any key to continue.");
            }
            Console.ReadKey();
        }
        static void AddBook(Books[] catalogArray, string ISBN)
        {
            if (SearchBooks(catalogArray,ISBN) != -1)
            {
                Books.IncCount();
                Console.WriteLine("Please enter the Title of the book: ");
                string title = Console.ReadLine();
                Console.WriteLine("Please enter the Author of the book: ");
                string author = Console.ReadLine();
                Console.WriteLine("Please enter the Genre of the book: ");
                string genre = SelectGenre();
                Console.WriteLine("Please enter the listening time of the book (in minutes): ");
                int listenTime = int.Parse(Console.ReadLine());
                catalogArray[Books.GetCount()-1] = new Books(ISBN, title, author, genre, listenTime, 1, 1);
                Console.WriteLine("Book added to inventory, press any key to continue.");
            }
        }
        static string SelectGenre()
        {
            bool valid = false;
            Console.WriteLine("1:   Romance");
            Console.WriteLine("2:   Mystery");
            Console.WriteLine("3:   Fantasy / SciFi");
            Console.WriteLine("4:   Thriller / Horror");
            Console.WriteLine("5:   Young Adult");
            Console.WriteLine("6:   Children's Fiction");
            Console.WriteLine("7:   Inspirational / Religious");
            Console.WriteLine("8:   Biography / Informational");
            Console.WriteLine("9:   Other");
            while (!valid)
            {
                switch(Console.ReadLine())
                {
                    case "1":
                    return "Romance";
                    
                    case "2":
                    return "Mystery";

                    case "3":
                    return "Fantasy / SciFi";

                    case "4":
                    return "Thriller / Horror";

                    case "5":
                    return "Young Adult";

                    case "6":
                    return "Children's Fiction";

                    case "7":
                    return "Inspirational / Religious";

                    case "8":
                    return "Biography / Informational";

                    case "9":
                    return "Other";

                    default:
                    break;
                }
            }
            return "N/A";
        }
        static void EditBook(Books[] catalogArray)
        {
            Console.Clear();
            Console.WriteLine("**Edit a Book**\n\n\n");
            Console.WriteLine("Please enter an ISBN: ");
            string ISBN = Console.ReadLine();
            int index = SearchBooks(catalogArray,ISBN);
            if (index == -1)
            {
                Console.WriteLine("ISBN Does not Exist, would you like to add this ISBN instead? (Y for yes, any other key for no)");
                string choice = Console.ReadLine();
                if (choice.ToUpper() == "Y")
                {
                    AddBook(catalogArray, ISBN);
                    
                }
            }
            else
            {
                Console.WriteLine($"ISBN: {catalogArray[index].GetISBN()}\nTitle: {catalogArray[index].GetTitle()}\nAuthor: {catalogArray[index].GetAuthor()}\n Genre: {catalogArray[index].GetGenre()}\nListen time: {catalogArray[index].GetListenTime()}\nTotal Stock: {catalogArray[index].GetTotalStock()}\nCurrent Stock: {catalogArray[index].GetCurrentStock()}");
                Console.WriteLine("What value would you like to change?\n");
                Console.WriteLine("1:   ISBN");
                Console.WriteLine("2:   Title");
                Console.WriteLine("3:   Author");
                Console.WriteLine("4:   Genre");
                Console.WriteLine("5:   Listen Time");
                Console.WriteLine("6:   Total Stock");
                Console.WriteLine("7:   Current Stock");
                Console.WriteLine("8:   NO CHANGE");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "1":
                    Console.Write("Please enter the new value: ");
                    catalogArray[index].SetISBN(Console.ReadLine());
                    break;
                    
                    case "2":
                    Console.Write("Please enter the new value: ");
                    catalogArray[index].SetTitle(Console.ReadLine());
                    break;

                    case "3":
                    Console.Write("Please enter the new value: ");
                    catalogArray[index].SetAuthor(Console.ReadLine());
                    break;

                    case "4":
                    Console.Write("Please enter the new value: ");
                    catalogArray[index].SetGenre(SelectGenre());
                    break;

                    case "5":
                    Console.Write("Please enter the new value (Must be numerical): ");
                    catalogArray[index].SetListenTime(int.Parse(Console.ReadLine()));
                    break;

                    case "6":
                    Console.Write("Please enter the new value (Must be numerical): ");
                    catalogArray[index].SetTotalStock(int.Parse(Console.ReadLine()));
                    break;

                    case "7":
                    Console.Write("Please enter the new value (Must be numerical): ");
                    catalogArray[index].SetCurrentStock(int.Parse(Console.ReadLine()));
                    if (catalogArray[index].GetCurrentStock() > catalogArray[index].GetTotalStock())
                    {
                        catalogArray[index].SetTotalStock(catalogArray[index].GetCurrentStock());
                    }
                    break;

                    case "8":
                    break;;
                }
            }
            Console.ReadKey();
        }
        static void RentBook(Books[] catalogArray, Transaction[] transArray)
        {
            bool exit = false;
            Console.Clear();
            while (!exit)
            {
                Console.WriteLine("**Rent a Book**\n\n\n");
                Console.WriteLine("Please enter an ISBN: ");
                string ISBN = Console.ReadLine();
                int index = SearchBooks(catalogArray, ISBN);
                if (index == -1)
                {
                    Console.WriteLine("That ISBN does not exist in the system.");
                }
                else if (catalogArray[index].GetCurrentStock() < 1)
                {
                    Console.WriteLine("That book is currently out of stock.");
                }
                else
                {
                    Transaction.IncTransCount();
                    Console.Write("Please enter customer name (Last, First): ");
                    string custName = Console.ReadLine();
                    Console.Write("Please enter customer email address: ");
                    string custEmail = Console.ReadLine();
                    Console.WriteLine("How long will this book be rented for (in days): ");
                    int rentalLength = int.Parse(Console.ReadLine());
                    transArray[Transaction.GetTransCount()-1] = new Transaction(ISBN,custName,custEmail,rentalLength);
                    catalogArray[index].DecCurrentStock();
                }
                Console.WriteLine("Rent another book? (Y for yes, any other key for no)");
                switch(Console.ReadLine().ToUpper())
                {
                    case "Y":
                    exit = false;
                    break;

                    default:
                    exit = true;
                    break;
                }
            }
            
        }
        static void ReturnBook(Books[] catalogArray, Transaction[] transArray)
        {
            Console.WriteLine("**Rent a Book**\n\n\n");
            Console.Write("Please enter an ISBN: ");
            string ISBN = Console.ReadLine();
            Console.Write("Please enter customer email address: ");
            string email = Console.ReadLine();
            int index = SearchBooks(catalogArray, ISBN);
            int transaction = SearchTransactions(transArray, ISBN, email);
            Console.WriteLine($"Transaction Information: \n\n");
            Console.WriteLine($"Customer Name: {transArray[transaction].GetCustName()}");
            Console.WriteLine($"Customer Email: {transArray[transaction].GetCustEmail()}");
            Console.WriteLine($"Rented On: {transArray[transaction].GetRentalDate()}");
            Console.WriteLine($"Return Date: {transArray[transaction].GetReturnDate()}\n");
            Console.WriteLine("Rented Title Information:\n\n");
            Console.WriteLine($"ISBN: {catalogArray[index].GetISBN()}");
            Console.WriteLine($"Title: {catalogArray[index].GetTitle()}");
            Console.WriteLine($"Author: {catalogArray[index].GetAuthor()}")

        }
        
        static int SearchBooks(Books[] catalogArray,string SearchedISBN)
        {
            int arrayCount =-1;
            bool itemFound = false;
            //Console.WriteLine($"TitleCount: {Books.GetCount()}");
            for (int i = 0; i < Books.GetCount();i++)
            {
                if (catalogArray[i].GetISBN()==SearchedISBN)
                {
                    arrayCount = i;
                    itemFound = true;
                    break;
                }
                else {}
            }
            if (!itemFound)
            {
                arrayCount = -1;
            }
            return arrayCount;
        }static int SearchTransactions(Transaction[] transArray,string SearchedISBN, string custEmail)
        {
            int arrayCount = -1;
            bool itemFound = false;
            bool emailFound = false;
            for (int i = 0; i <= Transaction.GetTransCount();i++)
            {
                if (transArray[i].GetStatus() == "Out")
                {
                    if (transArray[i].GetCustEmail() == custEmail)
                    {
                        emailFound = true;
                        if (transArray[i].GetISBN() == SearchedISBN)
                        {
                            return i;
                        }
                    }
                }
            }
            if (emailFound && !itemFound)
            {
                Console.WriteLine("Customer email was found in the transaction log, but it is not associated with the provided ISBN.");
            }
            else if (!emailFound)
            {
                Console.WriteLine("Customer email not found in transaction log.");
            }
            else 
            {
                Console.WriteLine("An error occurred in the SearchTransaction method inside the Program class.");
            }
            return arrayCount;
        }
        public static void PrintCatalog(Books[] catalogArray)
        {
            for (int i =0; i < Books.GetCount();i++)
            {
                Console.WriteLine(catalogArray[i].GetISBN() + catalogArray[i].GetTitle() + catalogArray[i].GetAuthor());
            }
            Console.ReadKey();
            
        }
        public static void PrintTransactions(Transaction[] transArray)
        {
            for (int i =0; i < Transaction.GetTransCount();i++)
            {
                Console.WriteLine(transArray[i].GetID() + transArray[i].GetISBN() + transArray[i].GetCustName());
            }
            Console.ReadKey();
            
        }
    }
}
