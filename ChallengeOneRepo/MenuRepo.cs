using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOneRepo
{
    public class MenuRepo
    {
        private readonly List<Menu> _menuRepo = new List<Menu>();


        //CRUD
        //Create
        //Read
        //Update
        //Delete

        public void AddNewMenu(Menu menu)
        {
            _menuRepo.Add(menu);
        }

        public List<Menu> GetContents() =>  _menuRepo;

        public Menu GetMenu(int menuNumber)
        {
            foreach (Menu menu in _menuRepo)
                if (menu.MenuNumber == menuNumber)
                    return menu;
            return null;
        }

        public Menu GetMenu(string menuName)
        {
            foreach (Menu menu in _menuRepo)
                if (menu.MenuName.ToLower() == menuName.ToLower())
                    return menu;
            return null;
        }

        public bool UpdateExistingMenu(Menu updatedMenu, int menuNumber)
        {
            Menu menu = GetMenu(menuNumber);
            if (menu == null)
                return false;
            int itemIndex = _menuRepo.IndexOf(menu);
            _menuRepo[itemIndex] = updatedMenu;
            return true;
        }

        public bool DeleteMenu(Menu menu) => _menuRepo.Remove(menu);

        public bool DeleteMenu(int menuNumber) => _menuRepo.Remove(GetMenu(menuNumber));
        public bool DeleteMenu(string menuName) => _menuRepo.Remove(GetMenu(menuName));
    }
}
