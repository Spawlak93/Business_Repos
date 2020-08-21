using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeFourLibrary
{
    public class Golf : IOuting
    {
        public Golf() { }
        public Golf (int numberInAttendance, DateTime dateOfEvent, decimal costPerPerson)
        {
            NumberInAttendance = numberInAttendance;
            DateOfEvent = dateOfEvent;
            CostPerPerson = costPerPerson;
        }
        public OutingType OutingType => OutingType.Golf;

        public int NumberInAttendance { get; set; }

        public DateTime DateOfEvent { get; set; }

        public decimal CostPerPerson { get; set; }

        public decimal TotalCost => NumberInAttendance * CostPerPerson;
    }

    public class Bowling : IOuting
    {
        public Bowling() { }
        public Bowling(int numberInAttendance, DateTime dateOfEvent, decimal costPerPerson)
        {
            NumberInAttendance = numberInAttendance;
            DateOfEvent = dateOfEvent;
            CostPerPerson = costPerPerson;
        }
        public OutingType OutingType => OutingType.Bowling;

        public int NumberInAttendance { get; set; }

        public DateTime DateOfEvent { get; set; }

        public decimal CostPerPerson { get; set; }

        public decimal TotalCost { get => CostPerPerson * NumberInAttendance; }
    }

    public class AmusementPark : IOuting
    {
        public AmusementPark() { }
        public AmusementPark(int numberInAttendance, DateTime dateOfEvent, decimal costPerPerson)
        {
            NumberInAttendance = numberInAttendance;
            DateOfEvent = dateOfEvent;
            CostPerPerson = costPerPerson;
        }
        public OutingType OutingType => OutingType.AmusementPark;

        public int NumberInAttendance { get; set; }

        public DateTime DateOfEvent { get; set; }

        public decimal CostPerPerson { get; set; }

        public decimal TotalCost { get => CostPerPerson * NumberInAttendance; }
    }

    public class Concert : IOuting
    {
        public Concert() { }
        public Concert(int numberInAttendance, DateTime dateOfEvent, decimal costPerPerson)
        {
            NumberInAttendance = numberInAttendance;
            DateOfEvent = dateOfEvent;
            CostPerPerson = costPerPerson;
        }
        public OutingType OutingType => OutingType.Concert;

        public int NumberInAttendance { get; set; }

        public DateTime DateOfEvent { get; set; }

        public decimal CostPerPerson { get; set; }

        public decimal TotalCost { get => CostPerPerson * NumberInAttendance; }
    }
}
