using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOneRepo
{
    public class MenuItem
    {
        public MenuItem() { }
        public MenuItem(int mealNumber, string mealName, string description, List<string> ingrediants, decimal price)
        {
            MealNumber = mealNumber;
            MealName = mealName;
            Description = description;
            ListOfIngrediants = ingrediants;
            Price = price;
        }
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public List<string> ListOfIngrediants { get; set; }
        public decimal Price { get; set; }
    }
}
