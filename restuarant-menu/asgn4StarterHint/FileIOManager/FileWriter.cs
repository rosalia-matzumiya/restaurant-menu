using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using MealMenu;

namespace FileIOManager
{
    static public class FileWriter
    {
        //private static int counter = 0;

        public static void LoggingData(MenuItem[] items)
        {
            string lastMealTime = null;
            bool first = true;

            if (File.Exists(FileLocation.OUTPUT_FILE))
            {
                File.Delete(FileLocation.OUTPUT_FILE);
            }
            //Using will dispose the file once the loop is over.
            using(FileStream fileStream = File.OpenWrite(FileLocation.OUTPUT_FILE))
            {
                using(StreamWriter sw = new StreamWriter(fileStream))
                {
                    foreach (MenuItem mealItem in items)
                    {
                        if(mealItem.Time != lastMealTime)
                        {
                            if(!first)
                            {
                                sw.WriteLine();
                            }
                            sw.WriteLine("* " + mealItem.Time.Substring(0, 1).ToUpper() + mealItem.Time.Substring(1) + " Items *");
                        }
                        sw.Write(FormatSpacing(mealItem.Price.ToString("c")));
                        sw.Write("\t");
                        sw.Write(mealItem.Name + ", ");
                        sw.WriteLine(mealItem.Type);

                        first = false;
                        lastMealTime = mealItem.Time;
                    }
                }
            }
            Console.ReadLine();
        }//end method

        public static string FormatSpacing(string number)
        {
            const int SPACING = 3;
            int pos = number.Length - SPACING;
            String formattedSpacing = new String(' ', SPACING - pos) + number;
            return formattedSpacing;
        }//end method
  
    }
}//end class

