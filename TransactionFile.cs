using System;
using System.IO;

namespace pa5_sdaudet_ua
{
    public class TransactionFile
    {
        private static string file = "transactions.txt";

        public static Transaction[] GetTransactionsFromFile()
        {
            Transaction[] transArray = new Transaction[500]; //Allow up to 500 entries. 
            StreamReader inFile = new StreamReader(file); //Open log file. 

            string dataRow = inFile.ReadLine(); //Priming Read for while loop. 
            while(dataRow != null)
            {
                string[] tempArray = dataRow.Split('#'); //Split in to array with # delimiter
                int rowID = int.Parse(tempArray[0]); //Convert string to int
                int custID = int.Parse(tempArray[4]); //Convert string to int
                if (custID > Transaction.highestCustID)//Update highest customer ID value if needed
                {
                    Transaction.highestCustID = custID;
                }
                transArray[Transaction.GetTransCount()] = new Transaction(rowID, tempArray[1], tempArray[2], tempArray[3], custID, tempArray[5], tempArray[6], tempArray[7]);//Construct a new object in a new instance of the array
                Transaction.IncTransCount(); //Increment the class-wide count by 1
                dataRow = inFile.ReadLine(); //Update read for while loop. 
            }
            inFile.Close(); //Close and release lock on file. 
            return transArray; //Return the object Array to the main class. 
        }
        public static void SaveToFile(Transaction[] transArray)
        {
            StreamWriter outFile = new StreamWriter(file); //Open file to write to. 
            for (int i =0; i < Transaction.GetTransCount();i++) //Copy all array info to txt file for long term storage with # delimiter. 
            {
                outFile.WriteLine($"{transArray[i].GetID()}#{transArray[i].GetISBN()}#{transArray[i].GetCustName()}#{transArray[i].GetCustEmail()}#{transArray[i].custID}#{transArray[i].GetRentalDate()}#{transArray[i].GetReturnDate()}#{transArray[i].GetStatus()}");
            }
            outFile.Close(); //Close and release lock on file 
        }
    }
}