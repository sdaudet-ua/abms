using System;
using System.IO;

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
                bool valid = false;
                string userChoice = null;
                while (!valid)
                {
                    ChooseProgram();
                    Console.Write("\n\nChoice: ");
                    userChoice = Console.ReadLine();
                    int number;
                    if (userChoice == null){}
                    else if (int.TryParse(userChoice, out number))
                    {
                        valid = true;
                    }
                    else {}
                }

                //This is the selection block for the main menu.  
                switch(int.Parse(userChoice))
                {
                    case 1:
                    Console.Clear();
                    AddBook(catalogArray);
                    workingFile.SaveBooksToFile(catalogArray);
                    break;

                    case 2:
                    Console.Clear();
                    EditBook(catalogArray);
                    workingFile.SaveBooksToFile(catalogArray);
                    break;

                    case 3:
                    Console.Clear();
                    RentBook(catalogArray, transArray);
                    transLog.SaveToFile(transArray);
                    workingFile.SaveBooksToFile(catalogArray);
                    break;

                    case 4:
                    Console.Clear();
                    PrintAvailableBooks(catalogArray);
                    break;

                    case 5:
                    Console.Clear();
                    ReturnBook(catalogArray, transArray);
                    transLog.SaveToFile(transArray);
                    workingFile.SaveBooksToFile(catalogArray);
                    break;

                    case 16:
                    Console.Clear();
                    PrintCatalog(catalogArray);
                    Console.WriteLine();
                    PrintTransactions(transArray);
                    break;

                    case 6:
                    Console.Clear();
                    Console.WriteLine($"BookCount: {Books.GetCount()}");
                    Console.WriteLine($"TransCount: {Transaction.GetTransCount()}");
                    Console.ReadKey();
                    break;

                    case 7:
                    Console.Clear();
                    Reporting(catalogArray, transArray, 1);
                    break;

                    case 8:
                    Console.Clear();
                    Reporting(catalogArray, transArray, 2);
                    break;
                    
                    case 9:
                    Console.Clear();
                    Reporting(catalogArray, transArray, 3);
                    break;

                    case 0:
                    Console.Clear();
                    exit=1;
                    break;

                    default:
                    break;
                }
            }
        }
        static void ChooseProgram()
        {
            //This method displays the main menu and returns the chosen value to the main method for selection.
            Console.Clear();
            DisplayLogo();
            Console.Write("\n");
            Console.WriteLine("1:   Add a book.");
            Console.WriteLine("2:   Edit a book.");
            Console.WriteLine("3:   Rent a book.");
            Console.WriteLine("4:   Return a book.");
            Console.WriteLine("5:   Exit");
            Console.WriteLine("6:   Show book and transaction counts.");
            Console.WriteLine("7:   Show rentals by month/year.");
            Console.WriteLine("8:   Show rentals by customer email.");
            Console.WriteLine("9:   Show all rentals sorted by customer.");
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
        public static string SelectGenre()
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
                    Console.WriteLine("Book added, press Enter to return to the main menu.");
                    Console.ReadKey();
                }
            }
            else
            {
                catalogArray[index].EditBook();
            }
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
                    Console.Write("Please enter customer email address: ");
                    string custEmail = Console.ReadLine();
                    int customerFirstTransaction = SearchCustomer(transArray, custEmail);
                    if (customerFirstTransaction !=-1)
                    {
                        string custName = transArray[customerFirstTransaction].GetCustName();
                        Console.WriteLine($"Existing Customer: {custName}");
                        int custID = transArray[customerFirstTransaction].custID;
                        Console.WriteLine("How long will this book be rented for (in days): ");
                        int rentalLength = int.Parse(Console.ReadLine());
                        transArray[Transaction.GetTransCount()-1] = new Transaction(ISBN,custName,custEmail,custID,rentalLength);
                        catalogArray[index].DecCurrentStock();
                    }
                    else
                    {
                        Console.Write("Please enter customer name (Last, First): ");
                        string custName = Console.ReadLine();
                        Console.WriteLine("How long will this book be rented for (in days): ");
                        int rentalLength = int.Parse(Console.ReadLine());
                        int custID = GetCustID(transArray,custEmail,custName);
                        transArray[Transaction.GetTransCount()-1] = new Transaction(ISBN,custName,custEmail,custID,rentalLength);
                        catalogArray[index].DecCurrentStock();
                    }
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
        static void PrintAvailableBooks(Books[] catalogArray)
        {
            for (int i = 0; i < Books.GetCount();i++)
            {
                if (catalogArray[i].GetCurrentStock() > 0)
                {
                    Console.WriteLine($"ISBN: {catalogArray[i].GetISBN()}\nTitle: {catalogArray[i].GetTitle()}\nAuthor: {catalogArray[i].GetAuthor()}\nGenre: {catalogArray[i].GetGenre()}\nListen Time: {catalogArray[i].GetListenTime()}\n\n\n");
                }
            }
        }
        static void ReturnBook(Books[] catalogArray, Transaction[] transArray)
        {
            bool skipEntry = false;
            Console.WriteLine("**Rent a Book**\n\n\n");
            Console.Write("Please enter an ISBN: ");
            string ISBN = Console.ReadLine();
            int index = SearchBooks(catalogArray, ISBN);
            if (index == -1)
            {
                Console.WriteLine("This ISBN does not exist in the system. Press any key to return to the main menu.");
                skipEntry = true;
                Console.ReadKey();
            }
            if (catalogArray[index].GetTotalStock()-catalogArray[index].GetCurrentStock() == 1)
            {
                int transIndex =  SearchTransactions(transArray, ISBN);
                Console.WriteLine($"Transaction Information: \n\n");
                Console.WriteLine($"Customer Name: {transArray[transIndex].GetCustName()}");
                Console.WriteLine($"Customer Email: {transArray[transIndex].GetCustEmail()}");
                Console.WriteLine($"Rented On: {transArray[transIndex].GetRentalDate()}");
                Console.WriteLine($"Return Date: {transArray[transIndex].GetReturnDate()}\n");
                Console.WriteLine("Rented Title Information:\n\n");
                Console.WriteLine($"ISBN: {catalogArray[index].GetISBN()}");
                Console.WriteLine($"Title: {catalogArray[index].GetTitle()}");
                Console.WriteLine($"Author: {catalogArray[index].GetAuthor()}");
                Console.WriteLine($"Genre: {catalogArray[index].GetGenre()}");
                Console.WriteLine($"Listen Time: {catalogArray[index].GetListenTime()}\n");
                Console.WriteLine($"Is this the correct rental record? (Y for yes, Enter for no.)");
                string errorCheckInput = Console.ReadLine();
                if (errorCheckInput.ToUpper() == "Y")
                {
                    skipEntry = true;
                    Console.WriteLine("Would you like to return this rental? (Y for yes, Enter to return to the main menu.)");
                    string input = Console.ReadLine();
                    if (input.ToUpper() == "Y")
                    {
                        catalogArray[index].IncCurrentStock();
                        transArray[transIndex].Return();
                        Console.WriteLine("Book returned. Press any key to return to the main menu.");

                    }
                }
                
            }
            else if (catalogArray[index].GetTotalStock()-catalogArray[index].GetCurrentStock() == 0)
            {
                Console.WriteLine("There are no copies of this book rented out. Press any key to return to the main menu.");
                skipEntry = true;
                Console.ReadKey();
            }
            while (!skipEntry)
            {
                Console.Write("Please enter customer email address: ");
                string email = Console.ReadLine();
                int transaction = SearchTransactions(transArray, ISBN, email);
                Console.WriteLine($"Transaction Information: \n\n");
                Console.WriteLine($"Customer Name: {transArray[transaction].GetCustName()}");
                Console.WriteLine($"Customer Email: {transArray[transaction].GetCustEmail()}");
                Console.WriteLine($"Rented On: {transArray[transaction].GetRentalDate()}");
                Console.WriteLine($"Return Date: {transArray[transaction].GetReturnDate()}\n");
                Console.WriteLine("Rented Title Information:\n\n");
                Console.WriteLine($"ISBN: {catalogArray[index].GetISBN()}");
                Console.WriteLine($"Title: {catalogArray[index].GetTitle()}");
                Console.WriteLine($"Author: {catalogArray[index].GetAuthor()}");
                Console.WriteLine($"Genre: {catalogArray[index].GetGenre()}");
                Console.WriteLine($"Listen Time: {catalogArray[index].GetListenTime()}\n");
                Console.WriteLine("Would you like to return this rental? (Y for yes, Enter for no.)");
                string input = Console.ReadLine();
                if (input.ToUpper() == "Y")
                {
                    catalogArray[index].IncCurrentStock();
                    transArray[transaction].Return();
                }
                skipEntry = true;
            }
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
        }
        static int SearchTransactions(Transaction[] transArray,string SearchedISBN, string custEmail)
        {
            int arrayCount = -1;
            bool itemFound = false;
            bool emailFound = false;
            for (int i = 0; i < Transaction.GetTransCount();i++)
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
        static int SearchTransactions(Transaction[] transArray,string SearchedISBN)
        {
            int arrayCount = -1;
            for (int i = 0; i < Transaction.GetTransCount();i++)
            {
                if (transArray[i].GetStatus() == "Out")
                {
                    if ((transArray[i].GetISBN() == SearchedISBN))
                    {
                        return i;
                    }
                }
            }
            return arrayCount;
        }
        static int SearchCustomer(Transaction[] transArray,string customerEmail)
        {
            int arrayCount = -1;
            for (int i = 0; i < Transaction.GetTransCount();i++)
            {
                if (transArray[i].GetStatus() == "Out")
                {
                    if (transArray[i].GetCustEmail() == customerEmail)
                    {
                        return i;
                    }
                }
            }
            return arrayCount;
        }
        public static void Reporting(Books[] catalogArray, Transaction[] transArray, int reportType)
        {
            if (reportType == 1) //YearMonth Report
            {
                for (int year = 2000; year <= 2150; year++)
                {
                    int yearCount = 0;
                    int[] monthArray = {0,0,0,0,0,0,0,0,0,0,0,0,0};
                    string[] monthsOfYear = {"Not a Month", "January","February","March","April","May","June","July","August","September","October","November","December"};
                    for (int month = 1; month <= 12; month++)
                    {
                        for (int i = 0; i < Transaction.GetTransCount();i++)
                        {
                            if (transArray[i].GetRentalDate().Substring(0,4) == year.ToString())
                            {
                                if (transArray[i].GetRentalDate().Substring(0,6) == year.ToString()+month.ToString("D" + 2))
                                {
                                    yearCount++;
                                    monthArray[month]++;
                                }
                            }
                            
                        }
                    }
                    if (yearCount > 0)
                    {
                        Console.WriteLine($"{year} Total Rentals: {yearCount}");
                        for (int month = 1; month <= 12; month++)
                        {
                            if (monthArray[month] > 0)
                            {
                                Console.WriteLine($"{monthsOfYear[month]} {year} Rentals: {monthArray[month]}");
                            }
                        }
                    }
                    
                }
                Console.Write("\n\nWould you like to save this report to a file? (Y for yes, any other key for no.) ");
                string printDesired = Console.ReadLine();
                if (printDesired.ToUpper() == "Y")
                {
                    Console.Write("Please enter the name of the file you would like the report saved to (Do not include a file extension.): ");
                    string fileName = Console.ReadLine()+".txt";
                    StreamWriter outFile = new StreamWriter(fileName);
                    for (int year = 2000; year <= 2150; year++)
                    {
                        int yearCount = 0;
                        int[] monthArray = {0,0,0,0,0,0,0,0,0,0,0,0,0};
                        string[] monthsOfYear = {"Not a Month", "January","February","March","April","May","June","July","August","September","October","November","December"};
                        for (int month = 1; month <= 12; month++)
                        {
                            for (int i = 0; i < Transaction.GetTransCount();i++)
                            {
                                if (transArray[i].GetRentalDate().Substring(0,4) == year.ToString())
                                {
                                    if (transArray[i].GetRentalDate().Substring(0,6) == year.ToString()+month.ToString("D" + 2))
                                    {
                                        yearCount++;
                                        monthArray[month]++;
                                    }
                                }
                                
                            }
                        }
                        if (yearCount > 0)
                        {
                            outFile.WriteLine($"{year} Total Rentals: {yearCount}");
                            for (int month = 1; month <= 12; month++)
                            {
                                if (monthArray[month] > 0)
                                {
                                    outFile.WriteLine($"{monthsOfYear[month]} {year} Rentals: {monthArray[month]}");
                                }
                            }
                        }
                    }
                    outFile.Close();
                }
                
            }
            else if (reportType == 2)
            {
                Console.Write("Please enter a customer email address: ");
                string email = Console.ReadLine();
                int custTransaction = SearchCustomer(transArray, email);
                Console.WriteLine($"Showing all transactions for {transArray[custTransaction].GetCustName()} ({email}).\n");
                for (int i = 0; i < Transaction.GetTransCount(); i++)
                {
                    if (transArray[i].GetCustEmail() == email)
                    {
                        string bookISBN = transArray[i].GetISBN();
                        int index = SearchBooks(catalogArray, bookISBN);

                        Console.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                    }
                }
                Console.Write("\n\nWould you like to save this report to a file? (Y for yes, any other key for no.) ");
                string printDesired = Console.ReadLine();
                if (printDesired.ToUpper() == "Y")
                {
                    Console.Write("Please enter the name of the file you would like the report saved to (Do not include a file extension.): ");
                    string fileName = Console.ReadLine()+".txt";
                    StreamWriter outFile = new StreamWriter(fileName);
                    outFile.WriteLine($"Showing all transactions for {transArray[custTransaction].GetCustName()} ({email}).\n");
                    for (int i = 0; i < Transaction.GetTransCount(); i++)
                    {
                        if (transArray[i].GetCustEmail() == email)
                        {
                            string bookISBN = transArray[i].GetISBN();
                            int index = SearchBooks(catalogArray, bookISBN);

                            outFile.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                        }
                    }
                    outFile.Close();
                }
            }
            else if (reportType == 3)
            {
                for (int customer = 0; customer < Transaction.highestCustID; customer++)
                {
                    int customerTotal = 0;
                    string customerName = null;
                    bool customerFound = false;
                    for (int i = 0; i < Transaction.GetTransCount();i++)
                    {
                        if (transArray[i].custID == customer)
                        {
                            customerFound = true;
                            customerName = transArray[i].GetCustName();
                            break;
                        }
                    }
                    if (customerFound)
                    {
                        Console.WriteLine($"{customerName}");
                    }
                    for (int i = 0; i < Transaction.GetTransCount();i++)
                    {
                        if (transArray[i].custID == customer)
                        {
                            customerTotal++;
                            Console.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                        }
                    }
                    if (customerTotal > 0)
                    {
                        Console.WriteLine($"{customerName} has ordered {customerTotal} books total.\n\n");
                    }
                }
                Console.Write("Would you like to save this report to a file? (Y for yes, any other key for no.) ");
                string printDesired = Console.ReadLine();
                if (printDesired.ToUpper() == "Y")
                {
                    Console.Write("Please enter the name of the file you would like the report saved to (Do not include a file extension.): ");
                    string fileName = Console.ReadLine()+".txt";
                    StreamWriter outFile = new StreamWriter(fileName);
                    for (int customer = 0; customer < 9999; customer++)
                    {
                        int customerTotal = 0;
                        string customerName = null;
                        bool customerFound = false;
                        for (int i = 0; i < Transaction.GetTransCount();i++)
                        {
                            if (transArray[i].custID == customer)
                            {
                                customerFound = true;
                                customerName = transArray[i].GetCustName();
                                break;
                            }
                        }
                        if (customerFound)
                        {
                            outFile.WriteLine($"{customerName}");
                        }
                        for (int i = 0; i < Transaction.GetTransCount();i++)
                        {
                            if (transArray[i].custID == customer)
                            {
                                customerTotal++;
                                outFile.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                            }
                        }
                        if (customerTotal > 0)
                        {
                            outFile.WriteLine($"{customerName} has ordered {customerTotal} books total.\n\n");
                        }
                    }
                    outFile.Close();
                }
            }
        }
        private static int GetCustID(Transaction[] transArray,string custEmail, string custName)
        {
            string alphIndex = null;
            for (int i = 0; i < Transaction.GetTransCount()-1;i++)
            {
                if (transArray[i].GetCustEmail() == custEmail)
                {
                    return transArray[i].custID;
                }
            }
            switch (custName.Substring(0,1).ToUpper())
            {
                case "A":
                alphIndex = "1";
                break;

                case "B":
                alphIndex = "2";
                break;

                case "C":
                alphIndex = "3";
                break;

                case "D":
                alphIndex = "4";
                break;

                case "E":
                alphIndex = "5";
                break;

                case "F":
                alphIndex = "6";
                break;

                case "G":
                alphIndex = "7";
                break;

                case "H":
                alphIndex = "8";
                break;

                case "I":
                alphIndex = "9";
                break;

                case "J":
                alphIndex = "10";
                break;

                case "K":
                alphIndex = "11";
                break;

                case "L":
                alphIndex = "12";
                break;

                case "M":
                alphIndex = "13";
                break;

                case "N":
                alphIndex = "14";
                break;

                case "O":
                alphIndex = "15";
                break;

                case "P":
                alphIndex = "16";
                break;

                case "Q":
                alphIndex = "17";
                break;

                case "R":
                alphIndex = "18";
                break;

                case "S":
                alphIndex = "19";
                break;

                case "T":
                alphIndex = "20";
                break;

                case "U":
                alphIndex = "21";
                break;

                case "V":
                alphIndex = "22";
                break;
                case "W":
                alphIndex = "23";
                break;

                case "X":
                alphIndex = "24";
                break;

                case "Y":
                alphIndex = "25";
                break;

                case "Z":
                alphIndex = "26";
                break;
            }
            Random rand = new Random();
            int rando = rand.Next(0001,999);
            string newID = rando.ToString()+alphIndex;
            return int.Parse(newID);
        }
        public static void PrintCatalog(Books[] catalogArray)
        {
            Console.WriteLine($"ISBN\tTitle\tAuthor\tGenre\tListen Time\tTotal / Current Stock\t");
            for (int i =0; i < Books.GetCount();i++)
            {
                Console.WriteLine($"{catalogArray[i].GetISBN()}\t{catalogArray[i].GetTitle()}\t{catalogArray[i].GetAuthor()}\t{catalogArray[i].GetGenre()}\t{catalogArray[i].GetListenTime()}\t{catalogArray[i].GetTotalStock()}/{catalogArray[i].GetCurrentStock()}");
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
        static void DisplayLogo()
        {
            Console.WriteLine(@"
           ____  __  __  _____                              _ _       ____              _      __  __                                                   _      _____           _                 
     /\   |  _ \|  \/  |/ ____|              /\            | (_)     |  _ \            | |    |  \/  |                                                 | |    / ____|         | |                
    /  \  | |_) | \  / | (___    ______     /  \  _   _  __| |_  ___ | |_) | ___   ___ | | __ | \  / | __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_  | (___  _   _ ___| |_ ___ _ __ ___  
   / /\ \ |  _ <| |\/| |\___ \  |______|   / /\ \| | | |/ _` | |/ _ \|  _ < / _ \ / _ \| |/ / | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '_ ` _ \ / _ \ '_ \| __|  \___ \| | | / __| __/ _ \ '_ ` _ \ 
  / ____ \| |_) | |  | |____) |           / ____ \ |_| | (_| | | (_) | |_) | (_) | (_) |   <  | |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_   ____) | |_| \__ \ ||  __/ | | | | |
 /_/    \_\____/|_|  |_|_____/           /_/    \_\__,_|\__,_|_|\___/|____/ \___/ \___/|_|\_\ |_|  |_|\__,_|_| |_|\__,_|\__, |\___|_| |_| |_|\___|_| |_|\__| |_____/ \__, |___/\__\___|_| |_| |_|
                                                                                                                         __/ |                                        __/ |                      
                                                                                                                        |___/                                        |___/                       
");
        }
    }
}
