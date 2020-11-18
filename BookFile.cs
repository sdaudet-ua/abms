namespace pa5_sdaudet_ua
{
    public class BookFile
    {
        private string file;

        public BookFile(string inFile){
            this.file = inFile;
        }

        public Books[] GetBooksFromFile()
        {
            Books[] catalogArray = new Books[100];
            StreamReader inFile = new StreamReader(file);

            string dataRow = inFile.ReadLine();
            while(dataRow != null)
            {
                string[] tempArray = input.Split('#');
                int rowListenTime = int.Parse(tempArray[4]);
                int rowTotalStock = int.Parse(tempArray[5]);
                int rowCurrentStock = int.Parse(tempArray[6]);
                catalogArray[Books.GetCount()] = new Books(tempArray[0], tempArray[1], tempArray[2], tempArray[3], rowListenTime, rowTotalStock, rowCurrentStock);
                Books.IncCount();
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