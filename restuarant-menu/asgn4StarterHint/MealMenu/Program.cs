using System;
using FileIOManager;
using MealMenu;
using System.Globalization;
using System.IO;

namespace Restaurant_Menu
{
    class Program
    {
        static string[] mealContent = FileReader.ReadLines();
        static MenuItem[] items = new MenuItem[mealContent.Length];

        //Iterate through the string array and 
        //split the meal content into comma separated values.
        static MenuItem[] GetMenuItems()
        {
            const int MEAL_TIME  = 0;
            const int MEAL_NAME  = 1;
            const int MEAL_DESCP = 2;
            const int MEAL_COST  = 3;

            for (int i = 0; i < mealContent.Length; i++)
            {
                string[] columns = mealContent[i].Split(',');

                string time   = columns[MEAL_TIME];
                string name   = columns[MEAL_NAME];
                string descp  = columns[MEAL_DESCP];
                decimal cost  = decimal.Parse(columns[MEAL_COST], NumberStyles.Currency);
                MenuItem item = new MenuItem(time, name, descp, cost);
                items[i]      = item;
            }
            return items;
        }//end method

        
        static void PrintoutData(MenuItem[] items)
        {
            const int LUNCH  = 0;
            const int DINNER = 2;
            int counter = 0;

            foreach (MenuItem menuItem in items)
            {
                string tempTitle = "* " + menuItem.Time.Substring(0, 1).ToUpper() + 
                                    menuItem.Time.Substring(1) + " Items *";

                if (menuItem.Time == "lunch")
                {
                    if (counter == LUNCH)
                    {
                        Console.WriteLine(tempTitle);
                    }
                    counter++;
                }
                    else if (counter == DINNER)
                    {
                        Console.WriteLine(tempTitle);
                        counter++;
                    }
                Console.Write(FileWriter.FormatSpacing(menuItem.Price.ToString("c")));
                Console.Write("\t");
                Console.Write(menuItem.Name + ", ");
                Console.WriteLine(menuItem.Type);
            }
            Console.WriteLine("\n" + "Please check the text file in: ");
            //FileInfo gives the exact file location of the text file. 
            Console.WriteLine(new FileInfo(FileLocation.OUTPUT_FILE).FullName);
        }//end method

        
        //Iterate through the items array and sort the output by time i.e. lunch, dinner
        //and name i.e. bento box, roe, tuna
        public static void SortArray(MenuItem[] items)
        {
            bool repeat = true;
            while (repeat)
            {
                repeat = false;
                for (int i = 1; i < items.Length; i++)
                {
                    const int NEG_ONE = -1;
                    const int ZERO    = 0;
                    const int ONE     = 1;
                    MenuItem firstItem = items[i - ONE];
                    MenuItem secondItem = items[i];

                    if (string.Compare(firstItem.Time, secondItem.Time) == NEG_ONE ||
                       (string.Compare(firstItem.Time, secondItem.Time) == ZERO &&
                        string.Compare(firstItem.Name, secondItem.Name) == ONE))
                    {
                        items[i - 1] = secondItem;
                        items[i] = firstItem;

                        repeat = true;
                    }
                }
            }
        }//end method



        static void Main(string[] args)
        {
            MenuItem[] items = GetMenuItems();
            SortArray(items);
            PrintoutData(items);
            FileWriter.LoggingData(items);

            Console.ReadLine();
        }//end main

    }

}//end class
