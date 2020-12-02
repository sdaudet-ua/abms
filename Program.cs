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
            Books[] catalogArray = BookFile.GetBooksFromFile();
            Transaction[] transArray = TransactionFile.GetTransactionsFromFile();            

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
                    BookFile.SaveBooksToFile(catalogArray); //Save file as soon as method is finished. 
                    break;

                    case 2:
                    Console.Clear();
                    EditBook(catalogArray);
                    BookFile.SaveBooksToFile(catalogArray);//Save file as soon as method is finished.
                    break;

                    case 3:
                    Console.Clear();
                    RentBook(catalogArray, transArray);
                    TransactionFile.SaveToFile(transArray);//Save file as soon as method is finished.
                    BookFile.SaveBooksToFile(catalogArray);//Save file as soon as method is finished.
                    break;

                    case 4:
                    Console.Clear();
                    PrintAvailableBooks(catalogArray);
                    break;

                    case 5:
                    Console.Clear();
                    ReturnBook(catalogArray, transArray);
                    TransactionFile.SaveToFile(transArray);//Save file as soon as method is finished.
                    BookFile.SaveBooksToFile(catalogArray);//Save file as soon as method is finished.
                    break;

                    case 6:
                    Console.Clear();
                    Console.WriteLine($"BookCount: {Books.GetCount()}");
                    Console.WriteLine($"TransCount: {Transaction.GetTransCount()}");
                    Console.WriteLine($"Highest customer ID: {Transaction.highestCustID}");
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
            //This method displays the main menu and ASCII Art logo.
            Console.Clear();
            DisplayLogo();
            Console.Write("\n");
            Console.WriteLine("1:   Add a book.");
            Console.WriteLine("2:   View/Edit a book.");
            Console.WriteLine("3:   Rent a book.");
            Console.WriteLine("4:   Show books available for rental.");
            Console.WriteLine("5:   View Transaction / Return a book.");
            Console.WriteLine("6:   Show book and transaction counts.");
            Console.WriteLine("7:   Show rentals by month/year.");
            Console.WriteLine("8:   Show rentals by customer email.");
            Console.WriteLine("9:   Show all rentals sorted by customer.");
            Console.WriteLine("0:   Exit the program.");
        }
        //This method is the user-facing method for adding books. 
        static void AddBook(Books[] catalogArray) 
        {
            Console.Clear();
            Console.WriteLine("**Add a Book**\n\n\n");
            Console.WriteLine("Please enter an ISBN: ");
            string ISBN = Console.ReadLine();
            //Search book array for ISBN and return the array subscript. 
            int index = SearchBooks(catalogArray,ISBN);
            //If the book does not exist, this selection will bring the user through the addition process. 
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
                //Add an array instance with a new Books object. 
                catalogArray[Books.GetCount()-1] = new Books(ISBN, title, author, genre, listenTime);
                Console.WriteLine("Book added to inventory, press any key to continue.");
            }
            //If the book already exists, just increase the quantities. 
            else
            {
                catalogArray[index].IncCurrentStock();
                catalogArray[index].IncTotalStock();
                Console.WriteLine("ISBN already exists. Total and current stock quantities have been updated. Press any key to continue.");
            }
            Console.ReadKey();
        }
        //This method has a different signature than the other AddBook(), and is only called by EditBook() if the user enters an ISBN that does not exist. (Extra)
        static void AddBook(Books[] catalogArray, string ISBN) 
        {
            if (SearchBooks(catalogArray,ISBN) != -1) //Verification that the book does not exist. 
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
                catalogArray[Books.GetCount()-1] = new Books(ISBN, title, author, genre, listenTime);
                Console.WriteLine("Book added to inventory, press any key to continue.");
            }
        }
        public static string SelectGenre() // This method returns a string for the Genre field so the Genres are standardized across the book library. (Extra)
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
            while (!valid) //While loop will keep going until a correct value is entered. 
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
            return "N/A"; //Catch-all, this line should never be executed (it is impossible).
        }
        static void EditBook(Books[] catalogArray) //EditBooks() Allows users to change fields of a pre-existing ISBN
        {
            Console.Clear();
            Console.WriteLine("**Edit a Book**\n\n\n");
            Console.WriteLine("Please enter an ISBN: ");
            string ISBN = Console.ReadLine();
            int index = SearchBooks(catalogArray,ISBN); //Search the library for the entered ISBN
            if (index == -1) //If the book does not exist, the program offers a shortcut for the user to add it. (Extra)
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
            else //If Book exists, show its current values and provide the ability to change them. 
            {
                catalogArray[index].EditBook();
            }
        }
        static void RentBook(Books[] catalogArray, Transaction[] transArray) //RentBook() utilizes both the book array and transaction array. It's function is to allow the user to create a rental record in the system. 
        {
            bool exit = false;
            Console.Clear();
            while (!exit)
            {
                Console.WriteLine("**Rent a Book**\n\n\n");
                Console.WriteLine("Please enter an ISBN: ");
                string ISBN = Console.ReadLine();
                int index = SearchBooks(catalogArray, ISBN);
                if (index == -1) //Check that the ISBN exists
                {
                    Console.WriteLine("That ISBN does not exist in the system.");
                }
                else if (catalogArray[index].GetCurrentStock() < 1) //Ensure there are enough copies available. 
                {
                    Console.WriteLine("That book is currently out of stock.");
                }
                else //If book exists and there are enough copies of it, user is allowed to proceed. 
                {
                    Transaction.IncTransCount();//Increase total transaction count. 
                    Console.Write("Please enter customer email address: ");
                    string custEmail = Console.ReadLine();
                    int customerFirstTransaction = SearchCustomer(transArray, custEmail);
                    if (customerFirstTransaction !=-1) //If the customer has rented a book before, the system will autofill the other customer data (Name) for the user. (Extra)
                    {
                        string custName = transArray[customerFirstTransaction].GetCustName();
                        Console.WriteLine($"Existing Customer: {custName}");
                        int custID = transArray[customerFirstTransaction].custID;
                        Console.WriteLine("How long will this book be rented for (in days): ");
                        int rentalLength = int.Parse(Console.ReadLine());
                        transArray[Transaction.GetTransCount()-1] = new Transaction(ISBN,custName,custEmail,custID,rentalLength);
                        catalogArray[index].DecCurrentStock();//Reduce Current stock of title by 1 to reflect rental.
                    }
                    else //If it is a new customer, add the rental normally. 
                    {
                        Console.Write("Please enter customer name (Last, First): ");
                        string custName = Console.ReadLine();
                        Console.WriteLine("How long will this book be rented for (in days): ");
                        int rentalLength = int.Parse(Console.ReadLine());
                        int custID = GetCustID(transArray,custEmail,custName);
                        transArray[Transaction.GetTransCount()-1] = new Transaction(ISBN,custName,custEmail,custID,rentalLength);
                        catalogArray[index].DecCurrentStock(); //Reduce Current stock of title by 1 to reflect rental. 
                    }
                }
                Console.WriteLine("Rent another book? (Y for yes, any other key for no)");
                //Allow the user to rent another book without going back to the main menu. 
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
        static void PrintAvailableBooks(Books[] catalogArray) //Uses the console to show an up-to-date list of all available titles. 
        {
            for (int i = 0; i < Books.GetCount();i++)
            {
                if (catalogArray[i].GetCurrentStock() > 0) //If there is at least 1 available copy, print the information in the console. 
                {
                    Console.WriteLine($"ISBN: {catalogArray[i].GetISBN()}\nTitle: {catalogArray[i].GetTitle()}\nAuthor: {catalogArray[i].GetAuthor()}\nGenre: {catalogArray[i].GetGenre()}\nListen Time: {catalogArray[i].GetListenTime()}\nCurrent Stock: {catalogArray[i].GetCurrentStock()}/{catalogArray[i].GetTotalStock()}\n\n\n");
                }
            }
            //Data will be in the console until user presses a key.
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }
        static void ReturnBook(Books[] catalogArray, Transaction[] transArray) //ReturnBook()'s function is to mark a transaction as returned and to increment the available quantity up by 1 to show it is back in stock. 
        {
            bool skipEntry = false;
            Console.WriteLine("**Rent a Book**\n\n\n");
            Console.Write("Please enter an ISBN: ");
            string ISBN = Console.ReadLine();
            int index = SearchBooks(catalogArray, ISBN);
            if (index == -1) //Check that the book exists
            {
                Console.WriteLine("This ISBN does not exist in the system. Press any key to return to the main menu.");
                skipEntry = true;
                Console.ReadKey();
            }
            if (catalogArray[index].GetTotalStock()-catalogArray[index].GetCurrentStock() == 1) //Check if there is only 1 copy rented out. If there is only one 'out' copy, the system will automatically pull the transaction record so the user does not have to enter the customer's email address. (Extra)
            {
                int transIndex =  SearchTransactions(transArray, ISBN);
                //Print transaction and book information to the console. 
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
            else if (catalogArray[index].GetTotalStock()-catalogArray[index].GetCurrentStock() == 0) //Check that the book does not have its full quantity in stock. 
            {
                Console.WriteLine("There are no copies of this book rented out. Press any key to return to the main menu.");
                skipEntry = true;
                Console.ReadKey();
            }
            while (!skipEntry) //If there is more than one copy of the ISBN checked out, ask for customer information to match the transaction. 
            {
                Console.Write("Please enter customer email address: ");
                string email = Console.ReadLine();
                int transaction = SearchTransactions(transArray, ISBN, email); //Search for transaction with email specified.
                //Show transaction and book information. 
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
                string input = Console.ReadLine(); //Allow user to choose whether or not to return the copy. 
                if (input.ToUpper() == "Y")
                {
                    catalogArray[index].IncCurrentStock();
                    transArray[transaction].Return();
                }
                skipEntry = true;
            }
        }
        
        static int SearchBooks(Books[] catalogArray,string SearchedISBN) //Search book array and return the subscript of the array to the calling method. 
        {
            int arrayCount =-1;
            bool itemFound = false;
            //Console.WriteLine($"TitleCount: {Books.GetCount()}");
            for (int i = 0; i < Books.GetCount();i++) // Counter loop for searching through array. 
            {
                if (catalogArray[i].GetISBN()==SearchedISBN) // If this instance's ISBN matched the one entered by the user, return the subscript. 
                {
                    arrayCount = i;
                    itemFound = true;
                    break; //Skips the remainder of the search once the item is found for efficiency. 
                }
                else {}
            }
            if (!itemFound)
            {
                arrayCount = -1; //If the book does not exist, the method returns -1 which is handled by the calling method. 
            }
            return arrayCount;
        }
        static int SearchTransactions(Transaction[] transArray,string SearchedISBN, string custEmail) //This signature is for a full transaction lookup. It will return the array subscript of the desired transaction. 
        {
            int arrayCount = -1;
            bool itemFound = false;
            bool emailFound = false;
            for (int i = 0; i < Transaction.GetTransCount();i++) //Counter loop for searching the array. 
            {
                if (transArray[i].GetStatus() == "Out") //Only search checked-out books. This prevents returning the wrong transaction in case a customer rents the same book 2 separate times. (Extra)
                {
                    if (transArray[i].GetCustEmail() == custEmail) //Search for user first. 
                    {
                        emailFound = true;
                        if (transArray[i].GetISBN() == SearchedISBN)
                        {
                            return i;
                        }
                    }
                }
            }
            if (emailFound && !itemFound) //Error message in case of ISBN not matching any customer records. 
            {
                Console.WriteLine("Customer email was found in the transaction log, but it is not associated with the provided ISBN.");
            }
            else if (!emailFound) //Error message for customer email not existing in the system.
            {
                Console.WriteLine("Customer email not found in transaction log.");
            }
            else //Unexpected error message. (Made this for debugging)
            {
                Console.WriteLine("An error occurred in Program.SearchTransaction().");
            }
            return arrayCount;
        }
        static int SearchTransactions(Transaction[] transArray,string SearchedISBN) //This signature is used for searching for a book that only has one copy out. 
        { //If the user enters an ISBN that only has one copy rented out, this method returns the subscript for that transaction so the calling method can suggest the transaction information. 
            int arrayCount = -1;
            for (int i = 0; i < Transaction.GetTransCount();i++)
            {
                if (transArray[i].GetStatus() == "Out") //Only select a transaction that has not been returned yet. 
                {
                    if ((transArray[i].GetISBN() == SearchedISBN))
                    {
                        return i;
                    }
                }
            }
            return arrayCount;
        }
        static int SearchCustomer(Transaction[] transArray,string customerEmail) //This method returns the first transaction that matches an entered email address. 
        {// The output from this method is used to autofill customer information when renting a book. (Extra)
            int arrayCount = -1;
            for (int i = 0; i < Transaction.GetTransCount();i++)
            {
                if (transArray[i].GetCustEmail() == customerEmail)
                {
                    return i;
                }
            }
            return arrayCount;
        }
        public static void Reporting(Books[] catalogArray, Transaction[] transArray, int reportType) //Prints various reports to the console or to a file. 
        { //Comments are not duplicated to file section, only in console section. Save to file sections are only commented with their differences from the console version. 
            if (reportType == 1) //YearMonth Report
            {
                for (int year = 2000; year <= 2150; year++) //Search for transactions by year. 
                {
                    int yearCount = 0; //Count for all rentals on current year of loop. 
                    int[] monthArray = {0,0,0,0,0,0,0,0,0,0,0,0,0}; //Local Array to hold the month values for whichever year the loop is on. 
                    string[] monthsOfYear = {"Not a Month", "January","February","March","April","May","June","July","August","September","October","November","December"}; // String array with months of year to print them to console more efficiently. 
                    for (int month = 1; month <= 12; month++) //Loop for searching by month. 
                    {
                        for (int i = 0; i < Transaction.GetTransCount();i++) //Loop to search array. 
                        {
                            if (transArray[i].GetRentalDate().Substring(0,6) == year.ToString()+month.ToString("D" + 2)/*Puts month integer in format with 2 characters*/) //Compares first 4 characters to match year, and next 2 characters to match month
                            {
                                yearCount++;
                                monthArray[month]++;
                            }
                        }
                    }
                    if (yearCount > 0) //If there were transactions on the current year of the loop, print them in the console. Otherwise do nothing, which will prevent endless console scrolling. 
                    {
                        Console.WriteLine($"{year} Total Rentals: {yearCount}");//Show total year rentals. 
                        for (int month = 1; month <= 12; month++)
                        {
                            if (monthArray[month] > 0) //Only print months that had rentals. 
                            {
                                Console.WriteLine($"{monthsOfYear[month]} {year} Rentals: {monthArray[month]}"); //Show month of year total. 
                            }
                        }
                    }
                    
                }
                Console.Write("\n\nWould you like to save this report to a file? (Y for yes, Enter to return to Main Menu.) ");
                string printDesired = Console.ReadLine();
                if (printDesired.ToUpper() == "Y") //This section is identical to the above section besides the change from Console.WriteLine() to outFile.WriteLine()
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
                    outFile.Close(); //Close and release lock on file. 
                }
                
            }
            else if (reportType == 2) //Show all transactions associated with specified email address. 
            {
                Console.Write("Please enter a customer email address: ");
                string email = Console.ReadLine();
                int custTransaction = SearchCustomer(transArray, email); //Find a transaction with that email address and return its subscript. 
                Console.WriteLine($"Showing all transactions for {transArray[custTransaction].GetCustName()} ({email}).\n"); //Show customer name from the above transaction before printing the transactions. 
                for (int i = 0; i < Transaction.GetTransCount(); i++) //Counter for array search
                {
                    if (transArray[i].GetCustEmail() == email) //If this instance's custEmail field matches the specified email address, print its information. 
                    {
                        Console.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                    }
                }
                Console.Write("\n\nWould you like to save this report to a file? (Y for yes, Enter to return to Main Menu.) ");
                string printDesired = Console.ReadLine();
                if (printDesired.ToUpper() == "Y") //This section is identical to the above section besides the change from Console.WriteLine() to outFile.WriteLine()
                {
                    Console.Write("Please enter the name of the file you would like the report saved to (Do not include a file extension.): ");
                    string fileName = Console.ReadLine()+".txt";
                    StreamWriter outFile = new StreamWriter(fileName);
                    outFile.WriteLine($"Showing all transactions for {transArray[custTransaction].GetCustName()} ({email}).\n");
                    for (int i = 0; i < Transaction.GetTransCount(); i++)
                    {
                        if (transArray[i].GetCustEmail() == email)
                        {
                            outFile.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                        }
                    }
                    outFile.Close(); //Close and release lock on file.
                }
            }
            else if (reportType == 3)
            {
                for (int customer = 0; customer <= Transaction.highestCustID; customer++) //Counter for customerID. It will count up from 0 to the highest customerID, which will display the information sorted alphabetically by last name. 
                {
                    int customerTotal = 0;
                    string customerName = null;
                    bool customerFound = false;
                    for (int i = 0; i < Transaction.GetTransCount();i++)//Search for first transaction matching customerID to retrieve name of customer. 
                    {
                        if (transArray[i].custID == customer)
                        {
                            customerFound = true;
                            customerName = transArray[i].GetCustName();
                            break; //End for loop early once a matching transaction has been found (Improves efficiency)
                        }
                    }
                    if (customerFound) //If the customer ID exists, print the customer name and their transactions. 
                    {
                        Console.WriteLine($"{customerName}");
                        for (int i = 0; i < Transaction.GetTransCount();i++) //Transactions are stored chonologically, so after sorting by last name, transactions will be printed in chronological order. 
                        {
                            if (transArray[i].custID == customer)
                            {
                                customerTotal++;
                                Console.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                            }
                        }
                        Console.WriteLine($"Customer Total: {customerTotal}\n\n"); //Print customer total transactions. 
                    }
                }
                Console.Write("Would you like to save this report to a file? (Y for yes, Enter to return to Main Menu.) ");
                string printDesired = Console.ReadLine();
                if (printDesired.ToUpper() == "Y") //
                {
                    Console.Write("Please enter the name of the file you would like the report saved to (Do not include a file extension.): ");
                    string fileName = Console.ReadLine()+".txt";
                    StreamWriter outFile = new StreamWriter(fileName);
                    for (int customer = 0; customer <= Transaction.highestCustID; customer++)
                    {
                        int customerTotal = 0;
                        string customerName = null;
                        bool customerFound = false;
                        for (int i = 0; i < Transaction.GetTransCount();i++)//Search for first transaction matching customerID to retrieve name of customer. 
                    {
                        if (transArray[i].custID == customer)
                        {
                            customerFound = true;
                            customerName = transArray[i].GetCustName();
                            break; //End for loop early once a matching transaction has been found (Improves efficiency)
                        }
                    }
                    if (customerFound) //If the customer ID exists, print the customer name and their transactions. 
                    {
                        outFile.WriteLine($"{customerName}");
                        for (int i = 0; i < Transaction.GetTransCount();i++) //Transactions are stored chonologically, so after sorting by last name, transactions will be printed in chronological order. 
                        {
                            if (transArray[i].custID == customer)
                            {
                                customerTotal++;
                                outFile.WriteLine($"{transArray[i].GetISBN()}\tRental Date: {transArray[i].GetRentalDate()}\tStatus: {transArray[i].GetStatus()}");
                            }
                        }
                        outFile.WriteLine($"Customer Total: {customerTotal}\n\n"); //Print customer total transactions. 
                    }
                    }
                    outFile.Close(); //Close and release lock on file.
                }
            }
        }
        private static int GetCustID(Transaction[] transArray,string custEmail, string custName)
        {
            string alphIndex = null;
            for (int i = 0; i < Transaction.GetTransCount()-1;i++)
            {
                if (transArray[i].GetCustEmail() == custEmail) //Check if customer already exists, if so return the existing customerID
                {
                    return transArray[i].custID;
                }
            }
            //If customer does not already exists, this switch will assign the first number of the customerID based on the first letter of the customer last name.
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
            Random rand = new Random(); //Random number generator
            int rando = rand.Next(0001,999); //Random number generator maxValue 999
            string newID = rando.ToString()+alphIndex; //Append the random number to the alphabet prefix value
            return int.Parse(newID); //Return the new customer ID. 
        }
        static void DisplayLogo()
        {
            //This ASCII art was computer generated using http://patorjk.com/software/taag/

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
