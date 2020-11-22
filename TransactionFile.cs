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
                transArray[Transaction.GetTransCount()] = new Transaction(rowID, tempArray[1], tempArray[2], tempArray[3], tempArray[4], tempArray[5], tempArray[6]);
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
                outFile.WriteLine($"{transArray[i].GetID()}#{transArray[i].GetISBN()}#{transArray[i].GetCustName()}#{transArray[i].GetCustEmail()}#{transArray[i].GetRentalDate()}#{transArray[i].GetReturnDate()}#{transArray[i].GetStatus()}");
            }
            outFile.Close();
        }
    }
}