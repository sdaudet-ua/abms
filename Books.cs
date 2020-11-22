using System;


namespace pa5_sdaudet_ua
{
    public class Books
    {
        private string ISBN;
        private string title;
        private string author;
        private string genre;
        private int listenTime;
        private int totalStock;
        private int currentStock;
        private static int titleCount;

        public Books(string inISBN, string inTitle, string inAuthor, string inGenre, int inListenTime)
        {
            ISBN = inISBN;
            title = inTitle;
            author = inAuthor;
            genre = inGenre;
            listenTime = inListenTime;
            totalStock = 1;
            currentStock = 1;
            //Console.WriteLine("Constructor Complete");
        }
        public Books(string inISBN, string inTitle, string inAuthor, string inGenre, int inListenTime, int inTotalStock, int inCurrentStock)
        {
            ISBN = inISBN;
            title = inTitle;
            author = inAuthor;
            genre = inGenre;
            listenTime = inListenTime;
            totalStock = inTotalStock;
            currentStock = inCurrentStock;
        }
        public Books()
        {

        }
        public string GetISBN()
        {
            return ISBN;
        }
        public string GetTitle()
        {
            return title;
        }
        public string GetAuthor()
        {
            return author;
        }
        public string GetGenre()
        {
            return genre;
        }
        public int GetListenTime()
        {
            return listenTime;
        }
        public int GetTotalStock()
        {
            return totalStock;
        }
        public int GetCurrentStock()
        {
            return currentStock;
        }
        public void SetISBN(string inISBN)
        {
            ISBN = inISBN;
            Console.WriteLine($"Book ISBN changed to {ISBN}.");
        }
        public void SetTitle(string inTitle)
        {
            title = inTitle;
            Console.WriteLine($"Book title changed to {title}.");
        }
        public void SetAuthor(string inAuthor)
        {
            author = inAuthor;
            Console.WriteLine($"Book author changed to {author}.");
        }
        public void SetGenre(string inGenre)
        {
            genre = inGenre;
            Console.WriteLine($"Book genre changed to {genre}.");
        }
        public void SetListenTime(int inTime)
        {
            listenTime = inTime;
            Console.WriteLine($"Book listen time changed to {listenTime}.");
        }
        public void SetTotalStock(int desiredQuantity)
        {
            totalStock = desiredQuantity;
            Console.WriteLine($"Book total stock changed to {totalStock}");
        }
        public void IncTotalStock()
        {
            totalStock++;
        }
        public void SetCurrentStock(int desiredQuantity)
        {
            currentStock = desiredQuantity;
            Console.WriteLine($"Book current stock changed to {currentStock}");
        }
        public void IncCurrentStock()
        {
            currentStock++;
        }
        public void DecCurrentStock()
        {
            currentStock--;
        }
        public static void IncCount()
        {
            titleCount++;
        }
        public static int GetCount()
        {
            return titleCount;
        }
        public static void InitCount()
        {
            titleCount = 0;
        }
    }
}