using System;
using System.IO;

namespace pa5_sdaudet_ua
{
    public class BookFile
    {
        private static string file;

        public BookFile(string inFile){
            file = inFile;
        }

        public Books[] GetBooksFromFile()
        {
            Books[] classBookArray = new Books[200];
            StreamReader inFile = new StreamReader(file);

            string dataRow = inFile.ReadLine();
            while(dataRow != null)
            {
                string[] tempArray = dataRow.Split('#');
                int rowListenTime = int.Parse(tempArray[4]);
                int rowTotalStock = int.Parse(tempArray[5]);
                int rowCurrentStock = int.Parse(tempArray[6]);
                classBookArray[Books.GetCount()] = new Books(tempArray[0], tempArray[1], tempArray[2], tempArray[3], rowListenTime, rowTotalStock, rowCurrentStock);
                //Console.WriteLine(tempArray[0]+tempArray[1]+tempArray[2]+tempArray[3]+rowListenTime+rowTotalStock+rowCurrentStock);
                Books.IncCount();
                dataRow = inFile.ReadLine(); 
            }
            inFile.Close();
            return classBookArray;
        }
        public void SaveBooksToFile(Books[] catalogArray)
        {
            StreamWriter outFile = new StreamWriter(file);
            for (int i =0; i < Books.GetCount();i++)
            {
                outFile.WriteLine($"{catalogArray[i].GetISBN()}#{catalogArray[i].GetTitle()}#{catalogArray[i].GetAuthor()}#{catalogArray[i].GetGenre()}#{catalogArray[i].GetListenTime()}#{catalogArray[i].GetTotalStock()}#{catalogArray[i].GetCurrentStock()}"); 
            }
            outFile.Close();
        }
    }
}