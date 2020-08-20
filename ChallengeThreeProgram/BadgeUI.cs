using ChallengeThreeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeThreeProgram
{
    public class BadgeUI
    {
        private readonly BadgeRepo _badgeRepo = new BadgeRepo();
        private bool _isRunning = true;
        private bool _badgeMenuRunning = false;
        public void Start()
        {
            Seed();
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            while (_isRunning)
            {
                Console.Clear();
                string userInput = GetMainMenuSelection();
                SelectMainMenuItem(userInput);
            }
        }

        private string GetMainMenuSelection()
        {
            Console.WriteLine("Welcome To Badge Management Interface.\n" +
                "What Would You Like To Do?\n" +
                "1. Add A New Badge.\n" +
                "2. Edit An Existing Badge.\n" +
                "3. List All Badges.\n" +
                "4. Exit.");
            return Console.ReadLine().ToLower();
        }

        private void SelectMainMenuItem(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                case "add":
                case "new":
                    AddNewBadge();
                    break;
                case "2":
                case "edit":
                    SelectBadgeToEdit();
                    return;
                case "3":
                case "all":
                    DisplayAllBadges();
                    break;
                case "4":
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
        //1
        private void AddNewBadge()
        {
            Console.Write("Badge Number:");
            int badgeNumber = GetNewBadgeNumber();
            Console.Write("Badge Type:");
            string badgeName = Console.ReadLine();
            Console.Write("Please Enter All Doors This Badge Has Access seperated By Commas (eg. \"A1,A2,B3,D12\")\n" +
                "Access Doors:");
            _badgeRepo.AddBadgeToRepo(new Badge(badgeNumber, badgeName, GetListOfDoors()));
            Console.Clear();
            Console.WriteLine("Badge Added.");

        }
        public int GetNewBadgeNumber()
        {
            while (true)
            {
                int userInput = GetInt();
                if (!_badgeRepo.ContainsKey(userInput))
                    return userInput;
                Console.Write("That Badge Number Already Exists.\n" +
                    "Please Try Again:");
            }
        }

        private List<string> GetListOfDoors()
        {
            string doorsString = Console.ReadLine().ToUpper();
            List<string> doors = new List<string>();
            foreach (string door in doorsString.Split(',').ToList())
            {
                doors.Add(FormatDoor(door));
            }
            return doors;
        }
        private string GetSingleDoor()
        {
            string door = FormatDoor(Console.ReadLine().ToUpper());         
            return door;
        }
        private string FormatDoor(string door)
        {
            List<char> doorChars = new List<char>();
            foreach (char c in door)
            {
                if (c != ' ' && c != '.' && c != ',')
                    doorChars.Add(c);
            }
            return new string(doorChars.ToArray());
        }

        //2
        private void SelectBadgeToEdit()
        {
            Console.Clear();
            Console.WriteLine("What Badge Number Would You Like To Edit?");
            int userInput = GetInt();
            if (CheckBadgeNumber(userInput))
            {
                RunEditBadgeMenu(_badgeRepo.GetDictionaryOfBadges()[userInput]);
                return;
            }
            Console.WriteLine("No Badge Found With That Number.\n" +
                "Try Again?(Y/N)");
            if (GetConfirmation())
                SelectBadgeToEdit();

        }
        public bool CheckBadgeNumber(int userInput)
        {
            if (_badgeRepo.ContainsKey(userInput))
                return true;
            return false;
        }
        private void RunEditBadgeMenu(Badge badge)
        {
            _badgeMenuRunning = true;
            while (_badgeMenuRunning)
            {
                Console.Clear();
                string userInput = GetBadgeMenuSelection(badge);
                SelectBadgeMenuItem(userInput, badge);
            }
        }
        private string GetBadgeMenuSelection(Badge badge)
        {
            DisplayBadgeInfo(badge);
            Console.WriteLine("What Would You Like To Do With This Badge?\n" +
                "1. Add A door\n" +
                "2. Add Multiple Doors\n" +
                "3. Remove A door\n" +
                "4. Remove All Access\n" +
                "5. Return To Main Menu");
            return Console.ReadLine();
        }
        private void SelectBadgeMenuItem(string userInput, Badge badge)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    AddDoor(badge);
                    break;
                case "2":
                    AddDoors(badge);
                    break;
                case "3":
                    if (CheckIfEmpty(badge))
                    {
                        Console.WriteLine("No Access To Remove.");
                        break;
                    }
                    RemoveDoor(badge);
                    break;
                case "4":
                    if (CheckIfEmpty(badge))
                    {
                        Console.WriteLine("No Access To Remove.");
                        break;
                    }
                    RemoveAllDoors(badge);
                    return;
                case "5":
                    _badgeMenuRunning = false;
                    return;
                default:
                    return;
            }
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        private void AddDoor(Badge badge)
        {
            Console.Write("What Door Would You Like To Add:");
            badge.AddDoor(GetSingleDoor());
            _badgeRepo.UpdateBadge(badge);
            Console.Clear();
            Console.WriteLine("Door Added");
        }
        private void AddDoors(Badge badge)
        {
            Console.Write("Please Enter All The Doors To Be Added Seperated By Commas(eg... A1,B2,C15).\n" +
                "Doors Accessable:");
            badge.AddDoors(GetListOfDoors());
            _badgeRepo.UpdateBadge(badge);
            Console.Clear();
            Console.WriteLine("Doors Added");
        }
        private void RemoveDoor(Badge badge)
        {
            Console.Clear();
            DisplayBadgeInfo(badge);
            Console.Write("What Door Would You Like To Remove:");
            if (badge.RemoveDoor(GetSingleDoor()))
            {
                _badgeRepo.UpdateBadge(badge);
                Console.Clear();
                Console.WriteLine("Door Removed");
                return;
            }
            Console.Write("This Badge Does not have Access to that door.\n" +
                "Try Again?(Y/N)");
            if (GetConfirmation())
                RemoveDoor(badge);
            Console.Clear();
            Console.WriteLine("Action Cancled");

        }
        private void RemoveAllDoors(Badge badge)
        {
            Console.WriteLine("Are You Sure You Want To Remove All Door Access from this card?");
            if (GetConfirmation())
            {
                badge.RemoveAllDoors();
                _badgeRepo.UpdateBadge(badge);
            }

        }
        private bool CheckIfEmpty(Badge badge)
        {
            if (badge.DoorsAccessable.Count == 0)
                return true;
            return false;
        }


        //3
        private void DisplayAllBadges()
        {
            Console.WriteLine(string.Format("{0,-15} | {1})", "BadgeNumber", "DoorsAccessable"));
            foreach (int key in _badgeRepo.GetDictionaryOfBadges().Keys)
                DisplayBadgeInFormat(_badgeRepo.GetBadge(key));
        }
        private void DisplayBadgeInFormat(Badge badge)
        {
            Console.Write(string.Format("{0, -15} | ", badge.BadgeID));
            foreach (string door in badge.DoorsAccessable)
            {
                Console.Write(door);
                if (door != badge.DoorsAccessable.Last())
                    Console.Write(", ");
                else
                    Console.Write(".\n");
            }

        }
        private void DisplayBadgeInfo(Badge badge)
        {
            Console.WriteLine($"BadgeID:{badge.BadgeID}");
            Console.Write("Doors Accessable:");
            foreach (string door in badge.DoorsAccessable)
            {
                if (badge.DoorsAccessable.IndexOf(door) % 10 == 9)
                    Console.Write("\n                 ");
                Console.Write(door);
                if (door != badge.DoorsAccessable.Last())
                    Console.Write(", ");
                else
                    Console.Write(".\n");
            }
        }


        private int GetInt()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int i))
                    return i;
                Console.Write("Invalid Entry.\n" +
                    "Please Enter A Numeric Value:");
            }
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
            _badgeRepo.AddBadgeToRepo(new Badge(1212, "Security", "A7", "B12", "C1", "C2"));
            _badgeRepo.AddBadgeToRepo(new Badge(1213, "Security", "A7", "B12", "C1", "C2","C3","C4","C5","C6","C7","C8","C9","C10"));
            _badgeRepo.AddBadgeToRepo(new Badge(1214, "Security", "A7", "B12", "C1", "C2"));
            _badgeRepo.AddBadgeToRepo(new Badge(1012, "Cleaner", "C1", "C2"));
            _badgeRepo.AddBadgeToRepo(new Badge(1013, "Cleaner", "C1", "C2"));
            _badgeRepo.AddBadgeToRepo(new Badge(2001, "VIP", "A7", "B12", "C1", "C2", "P22"));
            _badgeRepo.AddBadgeToRepo(new Badge(1112, "Developer", "A7", "B12"));
            _badgeRepo.AddBadgeToRepo(new Badge(1111, "Developer", "A7", "B12"));
        }


    }
}
