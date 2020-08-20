using System;
using System.Collections.Generic;
using ChallengeTwoClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeTwoTests
{
    [TestClass]
    public class RepoTests
    {
        ClaimRepo _repo;
        Claim _claim;

        [TestInitialize]
        public void Setup()
        {
            _repo = new ClaimRepo();
            _claim = new Claim(1, ClaimType.Home, "drop", 1111.90m, new DateTime(11, 10, 13), new DateTime(11, 12, 12));
            _repo.AddClaimToRepo(_claim);
            _claim = new Claim();
            _repo.AddClaimToRepo(_claim);
        }
        [TestMethod]
        public void AddToRepoTest()
        {
            Assert.AreEqual(2, _repo.GetListOfClaims().Count);
        }
        [TestMethod]
        public void GetClaimTests()
        {
            _claim = new Claim(3, ClaimType.Home, "drop", 1111.90m, new DateTime(11, 12, 13), new DateTime(11, 12, 15));
            _repo.AddClaimToRepo(_claim);

            Assert.AreEqual(ClaimType.Home, _repo.GetClaimByID(1).ClaimType);
            Assert.IsFalse(_repo.GetNextClaim().IsValid);
            Assert.IsTrue(_repo.GetClaimByID(3).IsValid);
        }
        [TestMethod]
        public void UpdateTest()
        {
            _claim = new Claim(1, ClaimType.Home, "Pop", 100m, new DateTime(11, 10, 13), new DateTime(11, 12, 12));
            _repo.UpdateClaim(_claim, 1);
            Assert.AreEqual("Pop", _repo.GetNextClaim().Description);
            Assert.AreEqual(100m, _repo.GetNextClaim().ClaimAmount);
        }
        [TestMethod]
        public void DeleteTests()
        {
            _repo.RemoveClaimFromRepo(1);

            _repo.RemoveClaimFromRepo(_repo.GetNextClaim());

            Assert.AreEqual(0, _repo.GetListOfClaims().Count);
        }

        [TestMethod]
        public void GetNextWhenRepoEmpty()
        {
            ClaimRepo claimRepo = new ClaimRepo();
            _claim = claimRepo.GetNextClaim();

            Assert.IsNull(_claim);
        }
    }
}
