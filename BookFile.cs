using System;
using System.IO;

namespace pa5_sdaudet_ua
{
    public class BookFile
    {
        private static string file = "books.txt";

        public static Books[] GetBooksFromFile()
        {
            Books[] classBookArray = new Books[500];//Allow up to 500 entries.
            StreamReader inFile = new StreamReader(file);//Open catalog file.

            string dataRow = inFile.ReadLine();//Priming Read for while loop. 
            while(dataRow != null)
            {
                string[] tempArray = dataRow.Split('#');//Split in to array with # delimiter
                int rowListenTime = int.Parse(tempArray[4]);//Convert string to int
                int rowTotalStock = int.Parse(tempArray[5]);//Convert string to int
                int rowCurrentStock = int.Parse(tempArray[6]);//Convert string to int
                classBookArray[Books.GetCount()] = new Books(tempArray[0], tempArray[1], tempArray[2], tempArray[3], rowListenTime, rowTotalStock, rowCurrentStock);//Construct a new object in a new instance of the array
                Books.IncCount();//Increment the class-wide count by 1
                dataRow = inFile.ReadLine(); //Update read for while loop. 
            }
            inFile.Close();//Close and release lock on file.
            return classBookArray;//Return the object Array to the main class.
        }
        public static void SaveBooksToFile(Books[] catalogArray)
        {
            StreamWriter outFile = new StreamWriter(file);//Open file to write to.
            for (int i =0; i < Books.GetCount();i++)//Copy all array info to txt file for long term storage with # delimiter.
            {
                outFile.WriteLine($"{catalogArray[i].GetISBN()}#{catalogArray[i].GetTitle()}#{catalogArray[i].GetAuthor()}#{catalogArray[i].GetGenre()}#{catalogArray[i].GetListenTime()}#{catalogArray[i].GetTotalStock()}#{catalogArray[i].GetCurrentStock()}"); 
            }
            outFile.Close();//Close and release lock on file 
        }
    }
}