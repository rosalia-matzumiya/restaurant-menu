using System;

namespace MealMenu
{
    public class MenuItem
    {
        //Constructor.
        public MenuItem(string time, string name, string type, decimal cost)
        {
            this.Time        = time.Trim();
            this.Name        = name.Trim();
            this.Type        = type.Trim();
            this.cost        = cost;
        }

        //Properties.
        public string Time { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        private decimal cost;
        public decimal Price { get { return cost * 1.8m; } }

    }
}//end class
