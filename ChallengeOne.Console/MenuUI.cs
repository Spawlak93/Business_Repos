using ChallengeOneRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOneProgram

{
    class MenuUI
    {
        //Create a Program file that allows the cafe manager to add, delete, and see all items in the menu list.
        private bool _isRunning = true;
        private bool _menuSelected = false;
        private readonly MenuRepo _repo = new MenuRepo();

        public void Run()
        {
            Seed();
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            while (_isRunning)
            {
                string userInput = GetMainMenuSelection();
                RunMainMenuItem(userInput);
            }
        }

        private string GetMainMenuSelection()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the MenuRepo Interface!\n" +
                "What would you like to do?\n" +
                "1. Display All Menus\n" +
                "2. Select A Menu \n" +
                "3. Create New Menu\n" +
                "4. Delete Menu\n" +
                "5. Exit");
            return Console.ReadLine().ToLower();
        }

        private void RunMainMenuItem(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                case "all":
                case "display all menus":
                    DisplayAllMenus();
                    //Display All Menus
                    break;
                case "2":
                case "select a menu":
                    SelectAMenu();
                    //Select a menu
                    return;
                case "3":
                case "new":
                case "create new menu":
                    CreateNewMenu();
                    //create new menu
                    return;
                case "4":
                case "delete":
                case "delete menu":
                    DeleteMenu();
                    //Delete menu
                    break;
                case "5":
                case "exit":
                case "quit":
                    _isRunning = false;
                    return;
                default:
                    return;
            }
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        private void DisplayAllMenus()
        {
            foreach (Menu menu in _repo.GetContents())
            {
                Console.WriteLine($"\nMenu Number: {menu.MenuNumber}");
                Console.WriteLine($"Menu Name: {menu.MenuName}\n");

            }

        }

        private void SelectAMenu()
        {
            Menu menu;
            Console.Clear();
            Console.WriteLine("What Menu Would You Like To Select?");
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int inputInt))
                menu = _repo.GetMenu(inputInt);
            else
                menu = _repo.GetMenu(userInput);

            if (menu != null)
            {
                _menuSelected = true;
                RunSelectedMenu(menu);
            }
            else
            {

                Console.WriteLine("I didn't understand that.\n" +
                    "Try Again?");
                if (GetConfirmation())
                    SelectAMenu();
            }
        }

        private void CreateNewMenu()
        {
            Menu newMenu = new Menu();
            Console.Clear();
            Console.WriteLine("What would you like to title this menu?");
            newMenu.MenuName = Console.ReadLine();
            Console.WriteLine("What Number would you like to give this Menu?");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if (CheckMenuNumber(userInput))
                    {
                        Console.WriteLine("That Number Is Already Taken");
                        continue;
                    }
                    newMenu.MenuNumber = userInput;
                    break;
                }
                else
                    Console.WriteLine("Please Enter A Whole Number");
            }
            _repo.AddNewMenu(newMenu);
            _menuSelected = true;
            RunSelectedMenu(newMenu);

        }

        private void DeleteMenu()
        {
            Console.Clear();
            Menu menu;
            Console.WriteLine("What Menu Would You Like To Delete?");
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int userInt))
                menu = _repo.GetMenu(userInt);
            else
                menu = _repo.GetMenu(userInput);

            if (menu != null)
            {
                Console.WriteLine($"Are You Sure You want to Delete {menu.MenuName}?(Y/N)");
                if (GetConfirmation())
                {
                    _repo.DeleteMenu(menu);
                    Console.WriteLine("Menu Deleted");
                }
                else
                    Console.WriteLine("Menu Not Deleted.");
            }
            else
            {
                Console.WriteLine("I'm Sorry I didn't find that menu.\n" +
                    "Would you like to try again?");
                if (GetConfirmation())
                    DeleteMenu();
            }


        }
        private void RunSelectedMenu(Menu menu)
        {
            while (_menuSelected)
            {
                string userInput = GetMenuMenuSelection(menu);
                RunMenuMenuItem(userInput, menu);
            }
        }


        private string GetMenuMenuSelection(Menu menu)
        {
            Console.Clear();
            Console.WriteLine($"What Would You Like to Do With {menu.MenuName}\n\n" +
                $"1. Display All Menu Items\n" +
                $"2. Display Single Menu Item\n" +
                $"3. Add New Menu Item\n" +
                $"4. Delete Menu Item\n" +
                $"5. Rename Menu\n" +
                $"6. Delete Menu\n" +
                $"7. Return To Main Menu");
            return Console.ReadLine().ToLower();
        }

        //The code below here should probably be split into its own class.
        private void RunMenuMenuItem(string userInput, Menu menu)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                case "all":
                case "display all menu items":
                    DisplayAllMenuItems(menu);
                    break;
                case "2":
                    DisplaySelectedMenuItem(menu);
                    break;
                case "3":
                case "new":
                case "add":
                    AddNewMenuItem(menu);
                    break;
                case "4":
                    DeleteMenuItem(menu);
                    break;
                case "5":
                    RenameMenu(menu);
                    break;
                case "6":
                    DeleteMenuMenu(menu);
                    _menuSelected = false;
                    return;
                case "7":
                case "exit":
                    _menuSelected = false;
                    return;
                default:
                    return;
            }
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        private void DisplayAllMenuItems(Menu menu)
        {
            foreach (MenuItem menuItem in menu.GetListOfMenuItems())
                DisplayMenuItem(menuItem);
        }

        private void DisplayMenuItem(MenuItem menuItem)
        {
            Console.WriteLine($"Meal Number: {menuItem.MealNumber}\n" +
                $"Meal Name: {menuItem.MealName}\n" +
                $"Description : {menuItem.Description}");
            foreach (string ingerdiant in menuItem.ListOfIngrediants)
            {
                Console.Write($"{ingerdiant}");
                if (menuItem.ListOfIngrediants.Last() != ingerdiant)
                {
                    Console.Write(", ");
                    if (menuItem.ListOfIngrediants.IndexOf(ingerdiant) % 5 == 4)
                        Console.Write("\n");

                }
                else
                    Console.Write(".\n");
            }
            Console.WriteLine($"Price: ${menuItem.Price:0.00}\n\n");


        }

        private void DisplaySelectedMenuItem(Menu menu)
        {
            Console.Clear();
            MenuItem menuItem;
            Console.WriteLine("What Item Would You Like To View?");
            menuItem = GetMenuItem(menu);
            if (menuItem != null)
            {
                DisplayMenuItem(menuItem);
                return;
            }

            Console.WriteLine("Item Not Found.\n" +
                "Try Again?(Y/N)");
            if (GetConfirmation())
                DisplaySelectedMenuItem(menu);
        }
        private void AddNewMenuItem(Menu menu)
        {
            MenuItem menuItem = new MenuItem();
            Console.WriteLine("What Would You Like To Call This Item?");
            menuItem.MealName = Console.ReadLine();
            Console.WriteLine("Give a Brief Description of this Item:");
            menuItem.Description = Console.ReadLine();
            Console.WriteLine("What Item Number Would You Like To Give This Item?");
            while (true)
            {
                if(int.TryParse(Console.ReadLine(), out int userInputInt))
                {
                    if (!CheckMenuItemNumber(userInputInt, menu))
                    {
                        menuItem.MealNumber = userInputInt;
                        break;
                    }
                    Console.WriteLine("That Number Is Taken.");
                }
                Console.WriteLine("Please Enter A Numeric Value.");
            }
            Console.WriteLine("What is The Price Of This Item?");
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal userInputDec))
                {
                    menuItem.Price = userInputDec;
                    break;
                }
                Console.WriteLine("Please Enter A Numeric Value.");
            }
            Console.WriteLine("Please List The Ingrediants seperated by commas.");
            string userInput = Console.ReadLine();
            List<string> ingList = userInput.Split(',').ToList();
            menuItem.ListOfIngrediants = ingList;
            menu.AddMenuItem(menuItem);
            Console.WriteLine("Item Added");
        }
        private void DeleteMenuItem(Menu menu)
        {
            Console.WriteLine("What Item Would You Like To Delete?");
            MenuItem menuItem = GetMenuItem(menu);
            if (menuItem != null)
            {
                Console.Clear();
                DisplayMenuItem(menuItem);
                Console.WriteLine("Are You Sure You Want To Delete This Item?");
                if (GetConfirmation())
                {
                    menu.DeleteMenuItem(menuItem);
                    Console.WriteLine("Item Deleted");
                }
                return;
            }
            Console.WriteLine("Item Not Found\n" +
                "Try Again?(Y/N)");
            if (GetConfirmation())
                DeleteMenuItem(menu);

        }
        private void RenameMenu(Menu menu)
        {
            Console.WriteLine("What Would You Like To Call This Menu?");
            menu.MenuName = Console.ReadLine();
            Console.WriteLine("Name Changed.");
        }

        private void DeleteMenuMenu(Menu menu)
        {

            Console.WriteLine($"Are You Sure You want to Delete {menu.MenuName}?(Y/N)");
            if (GetConfirmation())
            {
                _repo.DeleteMenu(menu);
                Console.WriteLine("Menu Deleted");
            }
            else
                Console.WriteLine("Menu Not Deleted.");
        }

        private MenuItem GetMenuItem(Menu menu)
        {
            MenuItem menuItem;
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int userInt))
                menuItem = menu.FindMenuItemByItemNumber(userInt);
            else
                menuItem = menu.FindMenuItemByName(userInput);
            return menuItem;
        }





        private bool CheckMenuNumber(int menuNumber)
        {
            foreach (Menu menu in _repo.GetContents())
                if (menu.MenuNumber == menuNumber)
                    return true;
            return false;
        }

        private bool CheckMenuItemNumber(int menuItemNumber, Menu menu)
        {
            foreach (MenuItem menuItem in menu.GetListOfMenuItems())
                if (menuItem.MealNumber == menuItemNumber)
                    return true;
            return false;
        }

        private bool GetConfirmation()
        {
            while (true)
            {
                string userInput = Console.ReadLine().ToLower();
                switch (userInput)
                {
                    case "yes":
                    case "y":
                        return true;
                    case "no":
                    case "n":
                        return false;
                    default:
                        Console.WriteLine("A simple yes or no is preferable");
                        break;
                }

            }
        }

        private void Seed()
        {
            Menu menu = new Menu("Example Fall Menu", 1);
            menu.AddMenuItem(new MenuItem(1, "Burger and fries", "Beef Patty with french fries", new List<string> { "Tomato", "Pickle", "lettuce", "Beef Patty", "Bun", "Fried Potatoes" }, 8.50m));
            menu.AddMenuItem(new MenuItem(2, "Black Bean Burger and fries", "Beef Patty with french fries", new List<string> { "Tomato", "Pickle", "lettuce", "Black Bean Patty", "Bun", "Fried Potatoes" }, 9.99m));
            menu.AddMenuItem(new MenuItem(3, "Salad", "Pathetic Salad", new List<string> { "Tomato", "Pickle", "lettuce", "dressing" }, 18.99m));
            menu.AddMenuItem(new MenuItem(4, "Avocado Toast", "Smashed avocado on whole grain toast", new List<string> { "Avocado", "Bread" }, 298.1m));
            menu.AddMenuItem(new MenuItem(5, "Menu Item 5", "Example", new List<string> { "bleh", "Bleh", "BLeh", "BLEh", "Bun", "BLEH" }, 1.99m));
            menu.AddMenuItem(new MenuItem(6, "Menu Item 6", "Yup", new List<string> { "Tomato", "Pickle", "lettuce", "Beef Patty", "Bun", "Fried Potatoes" }, 8m));
            _repo.AddNewMenu(menu);
            menu = new Menu("Example menu 2", 2);
            _repo.AddNewMenu(menu);

        }
    }
}
