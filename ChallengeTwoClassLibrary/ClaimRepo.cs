using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeTwoClassLibrary
{
    public class ClaimRepo
    {
        private readonly List<Claim> _claimsRepo = new List<Claim>();
        public void AddClaimToRepo(Claim claim)
        {
            _claimsRepo.Add(claim);
        }
        public List<Claim> GetListOfClaims() => _claimsRepo;
        public Claim GetClaimByID(int claimID)
        {
            foreach (Claim claim in _claimsRepo)
                if (claim.ClaimID == claimID)
                    return claim;
            return null;
        }
        public Claim GetNextClaim()
        {
            if (_claimsRepo.Count > 0)
                return _claimsRepo.First();
            else return null;
        }
        public bool UpdateClaim(Claim updatedClaim, int claimToUpdateID)
        {
            Claim claim = GetClaimByID(claimToUpdateID);
            if (claim != null)
            {
                int indexToUpdate = _claimsRepo.IndexOf(claim);
                _claimsRepo[indexToUpdate] = updatedClaim;
                return true;
            }
            return false;
        }
        public bool RemoveClaimFromRepo(Claim claim) => _claimsRepo.Remove(claim);
        public bool RemoveClaimFromRepo(int claimID) => RemoveClaimFromRepo(GetClaimByID(claimID));
    }
}