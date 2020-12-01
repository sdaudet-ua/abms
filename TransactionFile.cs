using System;
using System.IO;

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
                string[] tempArray = dataRow.Split('#');
                int rowID = int.Parse(tempArray[0]);
                int custID = int.Parse(tempArray[4]);
                if (custID > Transaction.highestCustID)
                {
                    Transaction.highestCustID = custID;
                }
                transArray[Transaction.GetTransCount()] = new Transaction(rowID, tempArray[1], tempArray[2], tempArray[3], custID, tempArray[5], tempArray[6], tempArray[7]);
                Transaction.IncTransCount();
                dataRow = inFile.ReadLine(); 
            }
            inFile.Close();
            return transArray;
        }
        public void SaveToFile(Transaction[] transArray)
        {
            StreamWriter outFile = new StreamWriter(file);
            for (int i =0; i < Transaction.GetTransCount();i++)
            {
                outFile.WriteLine($"{transArray[i].GetID()}#{transArray[i].GetISBN()}#{transArray[i].GetCustName()}#{transArray[i].GetCustEmail()}#{transArray[i].custID}#{transArray[i].GetRentalDate()}#{transArray[i].GetReturnDate()}#{transArray[i].GetStatus()}");
            }
            outFile.Close();
        }
    }
}