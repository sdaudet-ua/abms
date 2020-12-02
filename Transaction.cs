using System;

namespace pa5_sdaudet_ua
{
    public class Transaction
    {
        private int ID; //Transaction ID (not used as initially planned)
        private string ISBN; //ISBN of book that is attached to transaction. 
        private string custName; //Customer Name
        private string custEmail; //Customer Email Address
        public int custID; //Customer ID (for sorting purposes, not user facing)
        private string rentalDate; //Date book was rented out
        private string returnDate; //Date book is due by. 
        private string status; //Current disposition of the rented book. 
        private static int transactionCount; //Class-wide transaction counter. 
        public static int highestCustID; //Maximum search value for reporting. 

        public Transaction(string inISBN, string customerName, string customerEmail, int inCustID, int rentalLength) //Constructor for User Interface
        {
            ID = transactionCount+1;
            ISBN = inISBN;
            custName = customerName;
            custEmail = customerEmail;
            custID = inCustID;
            rentalDate = DateTime.Now.ToString("yyyyMMdd"); //Rental Date is today's date
            returnDate = DateTime.Now.AddDays(rentalLength).ToString("yyyyMMdd"); //When user is renting a book, he/she enters the length of the rental period, which is added to the returnDate and stored. 
            status = "Out"; //Set the disposition of the transaction for user to see if the book has been returned yet. 
        }
        public Transaction(int inID, string inISBN, string customerName, string customerEmail, int inCustID, string inRentalDate, string inReturnDate, string inStatus) //Constructor for importing file. All values provided by file. 
        {
            ID = inID;
            ISBN = inISBN;
            custName = customerName;
            custEmail = customerEmail;
            custID = inCustID;
            rentalDate = inRentalDate;
            returnDate = inReturnDate;
            status = inStatus;
        }
        public string GetISBN() //Accessor method
        {
            return ISBN;
        }
        public int GetID()//Accessor method
        {
            return ID;
        }
        public string GetCustName()//Accessor method
        {
            return custName;
        }
        public string GetCustEmail()//Accessor method
        {
            return custEmail;
        }
        public string GetRentalDate()//Accessor method
        {
            return rentalDate;
        }
        public string GetReturnDate()//Accessor method
        {
            return returnDate;
        }
        public string GetStatus()//Accessor method
        {
            return status;
        }
        public void Return()//This method changes the status "out" to "Returned on (today's date)"
        {
            status = $"Returned on {DateTime.Now.ToString("yyyyMMdd")}";
        }
        public static int GetTransCount()//Accessor method
        {
            return transactionCount;
        }
        public static void IncTransCount()//Increments the class-wide transaction counter by 1
        {
            transactionCount++;
        }
        public static void InitCount()//Initialize the class-wide counter to 0. Called at start of program. 
        {
            transactionCount = 0;
        }
        
    }
}