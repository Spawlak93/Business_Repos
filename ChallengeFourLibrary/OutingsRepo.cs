using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeFourLibrary
{
    public class OutingsRepo
    {
        private readonly List<IOuting> _outings_repo = new List<IOuting>();

        public void AddOutingToRepo(IOuting outing)
        {
            _outings_repo.Add(outing);
        }

        public List<IOuting> GetListOfOutings()
        {
            return _outings_repo;
        }

        public bool RemoveFromListOfOutings(IOuting outing) => _outings_repo.Remove(outing);
    }
}
