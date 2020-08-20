using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeThreeLibrary
{
    public class Badge
    {
        private readonly List<string> _DoorsAccessable = new List<string>();
        public Badge(int badgeID, string badgeName) { BadgeID = badgeID; BadgeName = badgeName; }
        public Badge(int badgeID, string badgeName, params string[] doorsAccessable)
        {
            BadgeID = badgeID;
            BadgeName = badgeName;
            foreach (string door in doorsAccessable)
                DoorsAccessable.Add(door);
        }
        public Badge(int badgeID, string badgeName, List<string> doors)
        {
            BadgeID = badgeID;
            BadgeName = badgeName;
            foreach (string door in doors)
                DoorsAccessable.Add(door);
        }

        public int BadgeID { get; }
        public string BadgeName { get; set; }
        public List<string> DoorsAccessable
        {
            get
            {
                return _DoorsAccessable;
            }
        }

        public void AddDoor(string door)
        {
            if(!DoorsAccessable.Contains(door))
                DoorsAccessable.Add(door);
        }
        public void AddDoors(params string[] doors)
        {
            foreach (string door in doors)
                if (!DoorsAccessable.Contains(door))
                    DoorsAccessable.Add(door);
        }
        public void AddDoors(List<string> doors)
        {
            foreach (string door in doors)
                if (!DoorsAccessable.Contains(door))
                    DoorsAccessable.Add(door);
        }

        public List<string> GetListOfDoorsAccessable()
        {
            return DoorsAccessable;
        }
        public bool RemoveDoor(string door)
        {           
             return DoorsAccessable.Remove(door);           
        }
        public void RemoveAllDoors()
        {
            while (DoorsAccessable.Count > 0)
                DoorsAccessable.RemoveAt(0);
        }
    }
}
