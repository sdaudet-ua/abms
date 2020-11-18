using System;

namespace fall_2020_starter_code
{
    class Program
    {
        static void Main(string[] args)
        {
            int exit = 0;
            while (exit == 0)
            {
                int userChoice = ChooseProgram();
                //This is the selection block for the main menu.  
                switch(userChoice)
                {
                    case 1:
                    Console.Clear();
                    AddBook();
                    break;

                    case 2:
                    Console.Clear();
                    EditBook();
                    break;

                    case 3:
                    Console.Clear();
                    RentBook();
                    break;

                    case 4:
                    Console.Clear();
                    ReturnBook();
                    break;

                    case 5:
                    Console.Clear();
                    exit=1;
                    break;
                }
            }
        }
        static int ChooseProgram()
        {
            //This method displays the main menu and returns the chosen value to the main method for selection.
            Console.Clear();
            Console.Write("\n\n\n");
            Console.WriteLine("1:   Add a book.");
            Console.WriteLine("2:   Edit a book.");
            Console.WriteLine("3:   Rent a book.");
            Console.WriteLine("4:   Return a book.");
            Console.WriteLine("5:   Exit");
            return int.Parse(Console.ReadLine());
        }
    }
}
