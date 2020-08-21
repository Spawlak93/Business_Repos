using ChallengeTwoClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeTwoProgram
{
    class ClaimsUI
    {
        private bool _updating = false;
        private bool _isrunning = true;
        private readonly ClaimRepo _claimRepo = new ClaimRepo();
        public void Start()
        {
            Seed();
            RunMenu();
        }

        private void RunMenu()
        {
            while (_isrunning)
            {
                Console.Clear();
                string userInput = GetMenuSelection();
                RunMenuSelection(userInput);
            }
        }
        private string GetMenuSelection()
        {
            Console.WriteLine("Claims Menu.\n" +
                "1. Display All Claims\n" +
                "2. Take Care Of Next Claim\n" +
                "3. Enter a New Claim\n" +
                "4. Modify an Existing Claim\n" +
                "5. Exit");
            return Console.ReadLine().ToLower();
        }
        private void RunMenuSelection(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                case "all":
                    DisplayAllClaims();
                    break;
                case "2":
                case "next":
                    HandleNextClaim();
                    break;
                case "3":
                case "new":
                    CreateNewClaim();
                    break;
                case "4":
                case "update":
                    SelectClaimToUpdate();
                    return;
                case "5":
                case "exit":
                case "quit":
                    _isrunning = false;
                    return;
                default:
                    return;
            }
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        private void DisplayAllClaims()
        {
            DisplayFormat();
            foreach (Claim claim in _claimRepo.GetListOfClaims())
                DisplaySingleClaimInFormat(claim);
        }
        private void DisplayFormat()
        {
            Console.WriteLine(string.Format("{0,-7} | {1, -5} | {2,-30} | {3,-15} | {4,-15} | {5,-15} | {6,-5}", "ClaimID", "Type", "Description", "Amount", "DateOfAccident", "DateOfClaim", "IsValid"));
        }

        private void DisplaySingleClaimInFormat(Claim claim)
        {
            Console.WriteLine(string.Format("{0,-7} | {1, -5} | {2,-30} | {3,-15} | {4,-15} | {5,-15} | {6,-5}", claim.ClaimID, claim.ClaimType, claim.Description, claim.ClaimAmount.ToString("C2"), claim.DateOfIncident.ToShortDateString(), claim.DateOfClaim.ToShortDateString(), claim.IsValid));
        }

        private void DisplaySingleClaim(Claim claim)
        {
            Console.WriteLine($"Claim ID: {claim.ClaimID}\n" +
                $"Type: {claim.ClaimType}\n" +
                $"Description: {claim.Description}\n" +
                $"Amount: {claim.ClaimAmount:C2}\n" +
                $"DateOfAccident: {claim.DateOfIncident.ToShortDateString()}\n" +
                $"DateOfClaim: {claim.DateOfClaim.ToShortDateString()}\n" +
                $"IsValid: {claim.IsValid}");
        }

        //Added functionality of returning Claim if they are handling it. The return is not caught at this time.
        private Claim HandleNextClaim()
        {
            Claim claim = _claimRepo.GetNextClaim();
            if (claim != null)
            {
                DisplaySingleClaim(claim);
                Console.WriteLine("\n\nDo you want to deal with this claim now?(Y/N)?");
                if (GetConfirmation())
                {
                    _claimRepo.RemoveClaimFromRepo(claim);
                    return claim;
                }
            }
            else
                Console.WriteLine("No Claims To Handle.");
            return null;
        }

        private void CreateNewClaim()
        {
            Claim claim = new Claim();
            //Auto Increments IDs if they are needing specific ID Num can be updated
            claim.ClaimID = _claimRepo.GetListOfClaims().Last().ClaimID + 1;
            Console.WriteLine("Please Enter the claim type:");
            claim.ClaimType = GetClaimType();
            Console.Clear();
            Console.Write("Enter a Brief Claim Description:");
            claim.Description = Console.ReadLine();
            Console.Clear();
            Console.Write("Amount of Damage: ");
            claim.ClaimAmount = GetDec();
            Console.Clear();
            Console.Write("Date Of Accident (mm/dd/yyyy): ");
            claim.DateOfIncident = GetDateTime();
            Console.Clear();
            Console.Write("Date Of Claim (mm/dd/yyyy): ");
            claim.DateOfClaim = GetDateTime();
            Console.Clear();
            DisplaySingleClaim(claim);
            _claimRepo.AddClaimToRepo(claim);
            Console.WriteLine("\nClaim Has Been Added.");

        }

        private void SelectClaimToUpdate()
        {
            if (_claimRepo.GetListOfClaims().Count == 0)
            {
                Console.WriteLine("No Claims To Update.");
                return;
            }
            while (true)
            {
                Console.WriteLine("What is the Claim Number you would Like to Update?");
                Claim claim = _claimRepo.GetClaimByID(GetInt());
                if (claim != null)
                {
                    _updating = true;
                    UpdateClaim(claim);
                    return;
                }
                else
                {
                    Console.WriteLine("No Claim Found By That Number.\n" +
                        "Try Again?(Y/N)");
                    if (GetConfirmation())
                        Console.Clear();
                    else
                        return;
                }
            }
        }
        private void UpdateClaim(Claim claim)
        {
            while (_updating)
            {
                Console.Clear();
                Console.WriteLine($"What would You Like To Change On This Claim?\n" +
                    $"1. Claim ID: {claim.ClaimID}\n" +
                    $"2. Type: {claim.ClaimType}\n" +
                    $"3. Description: {claim.Description}\n" +
                    $"4. Amount: {claim.ClaimAmount:C2}\n" +
                    $"5. DateOfAccident: {claim.DateOfIncident.ToShortDateString()}\n" +
                    $"6. DateOfClaim: {claim.DateOfClaim.ToShortDateString()}\n" +
                    $"7. Delete Claim\n" +
                    $"8. Cancel.");
                string userInput = Console.ReadLine().ToLower();
                Console.Clear();
                switch (userInput)
                {
                    case "1":
                    case "claimid":
                    case "claim id":
                        UpdateClaimID(claim);
                        break;
                    case "2":
                    case "type:":
                        UpdateType(claim);
                        break;
                    case "3":
                    case "description":
                        UpdateDescription(claim);
                        break;
                    case "4":
                    case "amount":
                        UpdateAmount(claim);
                        break;
                    case "5":
                    case "dateofaccident":
                    case "date of accident":
                        UpdateDateOfIncident(claim);
                        break;
                    case "6":
                    case "dateofclaim":
                    case "date of claim":
                        UpdateDateOfClaim(claim);
                        break;
                    case "7":
                    case "delete":
                    case "delete claim":
                    case "deleteclaim":
                        DeleteClaim(claim);
                        break;
                    case "8":
                    case "cancel":
                    case "quit":
                    case "exit":
                    case "stop":
                        _updating = false;
                        break;
                    default:
                        break;
                }

            }
        }

        private void UpdateClaimID(Claim claim)
        {
            int originalId = claim.ClaimID;
            Console.WriteLine("What ID Number would you like to give this Claim?");
            while (true)
            {
                int newIDNum = GetInt();
                if (!CheckClaimNumber(newIDNum))
                {
                    claim.ClaimID = newIDNum;
                    _claimRepo.UpdateClaim(claim, originalId);
                    CheckIfUpdateComplete();
                    return;

                }
                else if (newIDNum == claim.ClaimID)
                {
                    Console.WriteLine("That Is Already The Id Number.\n" +
                        "Did You Want To Change It?(Y/N)");
                    if (!GetConfirmation())
                        return;
                }
                else
                    Console.WriteLine("That ID Number Is Already Taken");
            }
        }

        private void UpdateType(Claim claim)
        {
            Console.WriteLine("What Type would you like to change it to?");
            claim.ClaimType = GetClaimType();
            _claimRepo.UpdateClaim(claim, claim.ClaimID);
            CheckIfUpdateComplete();
        }
        private void UpdateDescription(Claim claim)
        {
            Console.WriteLine("please Write A New Description:");
            claim.Description = Console.ReadLine();
            _claimRepo.UpdateClaim(claim, claim.ClaimID);
            CheckIfUpdateComplete();
        }
        private void UpdateAmount(Claim claim)
        {
            Console.WriteLine("Please give new claim amount:");
            claim.ClaimAmount = GetDec();
            _claimRepo.UpdateClaim(claim, claim.ClaimID);
            CheckIfUpdateComplete();
        }
        private void UpdateDateOfIncident(Claim claim)
        {
            Console.WriteLine("please enter Adjusted Date Of Incident(mm/dd/yyyy):");
            claim.DateOfIncident = GetDateTime();
            _claimRepo.UpdateClaim(claim, claim.ClaimID);
            CheckIfUpdateComplete();
        }

        private void UpdateDateOfClaim(Claim claim)
        {
            Console.WriteLine("please enter Adjusted Date Of Claim(mm/dd/yyyy):");
            claim.DateOfClaim = GetDateTime();
            _claimRepo.UpdateClaim(claim, claim.ClaimID);
            CheckIfUpdateComplete();
        }
        private void DeleteClaim(Claim claim)
        {
            DisplaySingleClaim(claim);
            Console.WriteLine("Are you sure you want to delete this claim?(Y/N)");
            if (GetConfirmation())
            {
                _claimRepo.RemoveClaimFromRepo(claim);
                Console.WriteLine("Claim Deleted");
                _updating = false;
            }
        }

        private void CheckIfUpdateComplete()
        {
            Console.Clear();
            Console.WriteLine("Claim Updated.\n" +
                "Is There More you would like to change?(Y/N)");
            if (!GetConfirmation())
                _updating = false;
        }

        private bool CheckClaimNumber(int claimNumber)
        {
            foreach (Claim claim in _claimRepo.GetListOfClaims())
                if (claim.ClaimID == claimNumber)
                    return true;
            return false;
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
        private decimal GetDec()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal dec))
                    return dec;
                Console.WriteLine("Invalid Entry Please Enter A Numeric Value.");
                Console.Write("Amount of Damage: ");

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
        private ClaimType GetClaimType()
        {
            List<ClaimType> maturities = Enum.GetValues(typeof(ClaimType)).Cast<ClaimType>().ToList();

            foreach (ClaimType i in maturities)
                Console.WriteLine($"press {(int)i + 1} for {i}");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                    if (userInput - 1 < maturities.Count() && userInput - 1 >= 0)
                        return maturities[userInput - 1];
                Console.WriteLine($"Invalid input please enter a number between 1 and {maturities.Count()}");
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
            _claimRepo.AddClaimToRepo(new Claim(1, ClaimType.Home, "drop", 11111111.99m, new DateTime(2020, 12, 16), new DateTime(2020, 12, 12)));
            _claimRepo.AddClaimToRepo(new Claim(2, ClaimType.Car, "EXPLOSIONS ON THE SUN", 1111.90m, new DateTime(2020, 12, 11), new DateTime(2020, 12, 12)));
            _claimRepo.AddClaimToRepo(new Claim(3, ClaimType.Theft, "Stolen Grain Of Rice", .01m, new DateTime(2020, 10, 19), new DateTime(2020, 11, 12)));
            _claimRepo.AddClaimToRepo(new Claim(4, ClaimType.Home, "drop", 50m, new DateTime(2020, 09, 22), new DateTime(2020, 10, 22)));
            _claimRepo.AddClaimToRepo(new Claim(5, ClaimType.Home, "drop", 1111.9m, new DateTime(2020, 10, 13), new DateTime(2020, 12, 12)));

        }

    }
}
