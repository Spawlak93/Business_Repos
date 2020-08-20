using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOneRepo
{
    public class Menu
    {
        private readonly List<MenuItem> _menuItems = new List<MenuItem>();

        public Menu() { }
        public Menu(string menuName, int menuNumber)
        {
            MenuName = menuName;
            MenuNumber = menuNumber;
        }

        public Menu(string menuName, int menuNumber, List<MenuItem> menuItems)
        {
            MenuName = menuName;
            MenuNumber = menuNumber;
            AddListOfMenuItems(menuItems);
        }

        public string MenuName { get; set; }
        public int MenuNumber { get; set; }


        public void AddListOfMenuItems(List<MenuItem> menuItems)
        {
            foreach (MenuItem menuItem in menuItems)
                _menuItems.Add(menuItem);
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            _menuItems.Add(menuItem);
        }

        public List<MenuItem> GetListOfMenuItems()
        {
            return _menuItems;
        }

        public MenuItem FindMenuItemByItemNumber(int menuItemNumber)
        {
            foreach (MenuItem menuItem in _menuItems)
                if (menuItem.MealNumber == menuItemNumber)
                    return menuItem;
            return null;
        }

        public MenuItem FindMenuItemByName(string name)
        {
            foreach (MenuItem menuItem in _menuItems)
                if (menuItem.MealName == name)
                    return menuItem;
            return null;
        }

        public bool UpdateMenuItemByNumber(MenuItem updatedMenuItem, int menuItemNumber)
        {
            MenuItem menuItem = FindMenuItemByItemNumber(menuItemNumber);
            if(menuItem == null)
            {
                return false;
            }
            int itemIndex = _menuItems.IndexOf(menuItem);
            _menuItems[itemIndex] = updatedMenuItem;
            return true;
        }

        public bool DeleteMenuItem(MenuItem menuItem)
        {
            return _menuItems.Remove(menuItem);
        }
        public bool DeleteMenuItemByNumber(int menuItemNumber)
        {
            return _menuItems.Remove(FindMenuItemByItemNumber(menuItemNumber));
        }
    }
}
