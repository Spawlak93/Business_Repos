using System;
using System.Collections.Generic;
using ChallengeOneRepo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeOne.Tests
{
    [TestClass]
    public class RepoTests
    {
        private MenuRepo _repo;
        private Menu _menu;
        private MenuItem _menuItem;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new MenuRepo();
            _menu = new Menu("Test Menu", 12);
            List<string> list = new List<string>
            {
                "Tomato",
                "Ketchup",
                "Salsa",
                "Marinara"
            };
            _menuItem = new MenuItem(1,"Tomatoes Galore","BLEH",list,1111.99m);
            _menuItem.ListOfIngrediants.Add("bruschetta");
            _menu.AddMenuItem(_menuItem);
            _repo.AddNewMenu(_menu);
        }
        [TestMethod]
        public void TestRepoAddMethod()
        {
            _repo.AddNewMenu(_menu);

            Assert.AreEqual(2, _repo.GetContents().Count);
        }

        [TestMethod]
        public void TestFindMenuOptions()
        {
            _menu = new Menu();
            _menu.MenuNumber = 2;
            _menu.MenuName = "Test";
            _repo.AddNewMenu(_menu);

            Menu menu1 = _repo.GetMenu(12);
            Menu menu2 = _repo.GetMenu("test");

            Assert.AreEqual("test menu", menu1.MenuName.ToLower());
            Assert.AreEqual(2, menu2.MenuNumber);
        }

        [TestMethod]
        public void TestUpdateMethod()
        {
            _menu = new Menu("Test Menu", 5);
            _repo.AddNewMenu(_menu);

            Assert.AreEqual("Test Menu", _repo.GetMenu(5).MenuName);

            Menu menu = _repo.GetMenu(5);
            menu.MenuName = "Updated Menu";

            _repo.UpdateExistingMenu(menu, 5);

            Assert.AreEqual("Updated Menu", _repo.GetMenu(5).MenuName);
        }

        [TestMethod]
        public void TestingMainDeleteMethod()
        {
            _repo.DeleteMenu(_menu);

            Assert.IsNull(_repo.GetMenu(12));
        }

        [TestMethod]
        public void TestingAlternativeDeleteMethods()
        {
            _menu = new Menu("Test Menu", 5);
            _repo.AddNewMenu(_menu);

            Assert.AreEqual(2, _repo.GetContents().Count);
            _repo.DeleteMenu(12);

            _repo.DeleteMenu("Test Menu");

            Assert.AreEqual(0, _repo.GetContents().Count);

        }
    }
}
