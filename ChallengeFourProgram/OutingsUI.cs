using ChallengeFourLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutingProgram
{
    public class OutingsUI
    {
        private readonly OutingsRepo _outingsRepo = new OutingsRepo();
        private bool _isRunning = true;
        private IOuting _newouting;

        public void Start()
        {
            Seed();
            Run();
        }

        private void Run()
        {
            while (_isRunning)
            {
                Console.Clear();
                string userInput = GetMenuSelection();
                SelectMenuItem(userInput);
            }
        }

        private string GetMenuSelection()
        {
            Console.WriteLine("Welcome To The Outings Cost Tracker.\n" +
                "What Would You Like To Do?\n" +
                "1. Display All Outings.\n" +
                "2. Add New Outing.\n" +
                "3. Get Total Costs of Outings.\n" +
                "4. Exit\n");
            return Console.ReadLine().ToLower();
        }
        private void SelectMenuItem(string userInput)
        {
            Console.Clear();
            switch(userInput)
            {
                case "1":
                case "1.":
                case "display":
                case "all":
                case "display all":
                case "display all outings":
                    DisplayAllOutings();
                    //display
                    break;
                case "2":
                case "2.":
                case "new":
                case "add":
                case "add new":
                case "add new outing":
                    AddOuting();
                    //Add New
                    break;
                case "3":
                case "3.":
                case "get total":
                case "cost":
                case "costs":
                    DisplayCosts();
                    //total costs
                    break;
                case "4":
                case "4.":
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

        private void DisplayAllOutings()
        {
            Console.WriteLine(string.Format("{0,-15} | {1,-10} | {2,-15} | {3,-15} | {4,-20}", "OutingType", "Attendance", "Date", "Cost Per Person", "Cost Of Outing"));
            DisplayOutingsInFormat();
        }
        private void DisplayOutingsInFormat()
        {
            foreach(IOuting outing in _outingsRepo.GetListOfOutings())
                Console.WriteLine(string.Format("{0,-15} | {1,-10} | {2,-15} | {3,-15} | {4,-20}", outing.OutingType, outing.NumberInAttendance, outing.DateOfEvent.ToShortDateString(), outing.CostPerPerson.ToString("C2"), outing.TotalCost.ToString("C2")));
        }
        private void AddOuting()
        {
            Console.WriteLine("What Type Of Event Would You Like To Create?");
            OutingType type = GetTypeOfEvent();
            GenerateNewOuting(type);
            if (_newouting == null)
                return;
            Console.Clear();
            Console.Write("How Many People Were In Attendance:");
            _newouting.NumberInAttendance = GetInt();
            Console.Clear();
            Console.Write("What Was The Cost Per Person?");
            _newouting.CostPerPerson = GetDec();
            Console.Clear();
            Console.Write("When Did The Outing Occur(mm/dd/yyyy):");
            _newouting.DateOfEvent = GetDateTime();
            _outingsRepo.AddOutingToRepo(_newouting);
            Console.Clear();
            Console.WriteLine("Outing Added.");

        }
        private void DisplayCosts()
        {
            List<OutingType> outingTypes = Enum.GetValues(typeof(OutingType)).Cast<OutingType>().ToList();
            decimal totalCost = 0;
            foreach (OutingType outingtype in outingTypes)
            {
                decimal counter = 0;
                foreach (IOuting outing in _outingsRepo.GetListOfOutings())
                {
                    if (outing.OutingType == outingtype)
                        counter += outing.TotalCost;
                }
                Console.WriteLine($"Cost of {outingtype} outings: {counter:C2}");
                totalCost += counter;
            }
            Console.WriteLine($"Total cost of outings: {totalCost:C2}");
        }
        private OutingType GetTypeOfEvent()
        {
            List<OutingType> outingTypes = Enum.GetValues(typeof(OutingType)).Cast<OutingType>().ToList();
            foreach(OutingType outing in outingTypes)
            {
                Console.WriteLine($"Press {(int)outing + 1} for {outing}");
            }
            while (true)
            {
                int userInput = GetInt() - 1;
                if (userInput < outingTypes.Count() && userInput >= 0)
                    return outingTypes[userInput];
                Console.WriteLine($"Invalid input please enter a number between 1 and {outingTypes.Count()}");
            }
        }
        private void GenerateNewOuting(OutingType type)
        {
            switch(type)
            {
                case OutingType.Golf:
                    _newouting = new Golf();
                    return;
                case OutingType.Bowling:
                    _newouting = new Bowling();
                    return;
                case OutingType.Concert:
                    _newouting = new Concert();
                    return;
                case OutingType.AmusementPark:
                    _newouting = new AmusementPark();
                    return;
                default:
                    Console.WriteLine("An Error Has Occured.\n" +
                        "Type Of Event not classed in Repository");
                    _newouting = null;
                    return;
            }
        }

        private int GetInt()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    return userInput;
                }
                Console.WriteLine("Please Enter A whole Number.");

            }
        }
        private decimal GetDec()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal userInput))
                {
                    return userInput;
                }
                Console.WriteLine("Please Enter A Numeric Value.");

            }
        }
        private DateTime GetDateTime()
        {
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out DateTime userInput))
                    return userInput;
                Console.Write("Invalid Entry.\n" +
                    "Please Try Again (mm/dd/yyyy):");
            }

        }

        private void Seed()
        {
            _outingsRepo.AddOutingToRepo(new Bowling(12, new DateTime(2020, 12, 08), 20m));
            _outingsRepo.AddOutingToRepo(new Bowling(12, new DateTime(2020, 12, 09), 20m));
            _outingsRepo.AddOutingToRepo(new Bowling(12, new DateTime(2020, 12, 10), 20m));
            _outingsRepo.AddOutingToRepo(new Bowling(12, new DateTime(2020, 12, 11), 20m));
            _outingsRepo.AddOutingToRepo(new Concert(40, new DateTime(2020, 12, 11), 101.99m));
            _outingsRepo.AddOutingToRepo(new AmusementPark(20, new DateTime(2020, 12, 11), 45.11m));
            _outingsRepo.AddOutingToRepo(new AmusementPark(20, new DateTime(2020, 12, 11), 55m));
            _outingsRepo.AddOutingToRepo(new AmusementPark(20, new DateTime(2020, 12, 11), 23.11m));
            _outingsRepo.AddOutingToRepo(new AmusementPark(20, new DateTime(2020, 12, 11), 45.11m));
            _outingsRepo.AddOutingToRepo(new AmusementPark(5, new DateTime(2020, 12, 11), 90.11m));
            _outingsRepo.AddOutingToRepo(new Golf(12, new DateTime(2020, 12, 11), 1000m));
            _outingsRepo.AddOutingToRepo(new Golf(12, new DateTime(2020, 12, 11), 50.99m));
        }
    }
}
