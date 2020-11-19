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
            ISBN = inISBN;
            custName = customerName;
            custEmail = customerEmail;
            rentalDate = DateTime.Now.ToString("yyyyMMdd");
            returnDate = DateTime.Now.AddDays(rentalLength).ToString("yyyyMMdd");
            status = "Out";
            transactionCount++;
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
        public int GetTransCount()
        {
            return transactionCount;
        }
        public void Return()
        {
            status = "Returned";
        }
        public void IncTransCount()
        {
            transactionCount++;
        }
    }
}