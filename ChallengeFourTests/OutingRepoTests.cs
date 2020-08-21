using System;
using ChallengeFourLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeFourTests
{
    [TestClass]
    public class OutingRepoTests
    {
        OutingsRepo _repo;
        IOuting amusementParkOuting;
        IOuting concertOuting;
        IOuting bowlingOuting;
        IOuting golfOuting;
        [TestInitialize]
        public void Setup()
        {
            _repo = new OutingsRepo();
            golfOuting = new Golf(12, new DateTime(2020, 11, 12), 5.87m);
            bowlingOuting = new Bowling(12, new DateTime(2020, 10, 12), 51.87m);
            concertOuting = new Concert(12, new DateTime(2020, 9, 12), 55.02m);
            amusementParkOuting = new AmusementPark(12, new DateTime(2020, 8, 12), 995.87m);
        }
        [TestMethod]
        public void TestAddMethod()
        {
            _repo.AddOutingToRepo(golfOuting);
            _repo.AddOutingToRepo(bowlingOuting);
            _repo.AddOutingToRepo(concertOuting);
            _repo.AddOutingToRepo(amusementParkOuting);

            Assert.IsTrue(_repo.GetListOfOutings().Count == 4);
            Assert.AreEqual(12 * 55.02m, _repo.GetListOfOutings()[2].TotalCost);
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            _repo.AddOutingToRepo(golfOuting);
            _repo.AddOutingToRepo(bowlingOuting);
            _repo.AddOutingToRepo(concertOuting);
            _repo.AddOutingToRepo(amusementParkOuting);

            _repo.RemoveFromListOfOutings(bowlingOuting);

            Assert.AreEqual(3, _repo.GetListOfOutings().Count);
        }
    }
}
