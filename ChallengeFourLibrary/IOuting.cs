using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeFourLibrary
{
    public enum OutingType { Golf, Bowling, AmusementPark, Concert}
    public interface IOuting
    {
        OutingType OutingType { get; }
        int NumberInAttendance { get; set; }
        DateTime DateOfEvent { get; set; }
        decimal CostPerPerson { get; set; }
        decimal TotalCost { get; }
    }
}
