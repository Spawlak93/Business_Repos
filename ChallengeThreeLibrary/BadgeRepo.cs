using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeThreeLibrary
{
    public class BadgeRepo
    {
        private readonly Dictionary<int, Badge> _badgeRepo = new Dictionary<int, Badge>();

        public bool AddBadgeToRepo(Badge badge)
        {
            if(!ContainsKey(badge.BadgeID))
            {
                _badgeRepo.Add(badge.BadgeID, badge);
                return true;
            }
            return false;
        }

        public void AddBadgeToRepo(params Badge[] badges)
        {
            foreach (Badge badge in badges)
                AddBadgeToRepo(badge);
        }
        public Dictionary<int, Badge> GetDictionaryOfBadges() => _badgeRepo;
        public Badge GetBadge(int badgeID)
        {
            if (ContainsKey(badgeID))
                return _badgeRepo[badgeID];
            return null;

        }
        public bool ContainsKey(int badgeID)
        {
            if (_badgeRepo.ContainsKey(badgeID))
                return true;
            return false;
        }
        public bool UpdateBadge(Badge badge)
        {
            if (ContainsKey(badge.BadgeID))
            {
                _badgeRepo[badge.BadgeID] = badge;
                return true;
            }
            return false;
        }
        public bool DeleteBadge(int badgeID) => _badgeRepo.Remove(badgeID);
    }
}
