using System;

namespace pa5_sdaudet_ua
{
    public class Transaction
    {
        private int ID;
        private string ISBN;
        private string custName;
        private string custEmail;
        private string rentalDate;
        private string returnDate;
        private string status;
        private static int transactionCount;

        public Transaction(string inISBN, string customerName, string customerEmail, int rentalLength)
        {
            ID = transactionCount+1;
            ISBN = inISBN;
            custName = customerName;
            custEmail = customerEmail;
            rentalDate = DateTime.Now.ToString("yyyyMMdd");
            returnDate = DateTime.Now.AddDays(rentalLength).ToString("yyyyMMdd");
            status = "Out";
        }
        public Transaction(int inID, string inISBN, string customerName, string customerEmail, string inRentalDate, string inReturnDate, string inStatus)
        {
            ID = inID;
            ISBN = inISBN;
            custName = customerName;
            custEmail = customerEmail;
            rentalDate = inRentalDate;
            returnDate = inReturnDate;
            status = inStatus;
        }
        public string GetISBN()
        {
            return ISBN;
        }
        public int GetID()
        {
            return ID;
        }
        public string GetCustName()
        {
            return custName;
        }
        public string GetCustEmail()
        {
            return custEmail;
        }
        public string GetRentalDate()
        {
            return rentalDate;
        }
        public string GetReturnDate()
        {
            return returnDate;
        }
        public string GetStatus()
        {
            return status;
        }
        public void Return()
        {
            status = $"Returned on {DateTime.Now.ToString("yyyyMMdd")}";
        }
        public static int GetTransCount()
        {
            return transactionCount;
        }
        public static void IncTransCount()
        {
            transactionCount++;
        }
        public static void InitCount()
        {
            transactionCount = 0;
        }
    }
}