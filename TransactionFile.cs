namespace pa5_sdaudet_ua
{
    public class TransactionFile
    {
        private string file;

        public TransactionFile(string inFile){
            file = inFile;
        }

        public Transaction[] GetTransactionsFromFile()
        {
            Transaction[] transArray = new Transaction[500];
            StreamReader inFile = new StreamReader(file);

            string dataRow = inFile.ReadLine();
            while(dataRow != null)
            {
                string[] tempArray = input.Split('#');
                int rowID = int.Parse(tempArray[0]);
                int rowTotalStock = int.Parse(tempArray[5]);
                int rowCurrentStock = int.Parse(tempArray[6]);
                transArray[Transaction.GetTransCount()] = new Transaction(rowID, tempArray[1], tempArray[2], tempArray[3], tempArray[4], tempArray[5]);
                Transaction.IncTransCount();
                dataRow = inFile.ReadLine(); 
            }
            inFile.Close();
            return catalogArray;
        }
        public void SaveBooksToFile()
        {
            StreamWriter outFile = new StreamWriter(file);
            for (int i =0; i < Books.GetCount();i++)
            {
                outFile.WriteLine($"{ISBN}#{title}#{author}#{genre}#{listenTime}#{totalStock}#{currentStock}"); 
            }
            outFile.Close();
        }
    }
}