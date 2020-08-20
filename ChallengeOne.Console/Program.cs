using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOneProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //menu items should have a number a name a description a list of ingredients and a price.
            //Ability to create new menu
            //Add new menu items to it
            //Edit menu items
            //menu repo for menus?
            //test class proving our methods
            MenuUI ui = new MenuUI();

            ui.Run();
        }
    }
}
