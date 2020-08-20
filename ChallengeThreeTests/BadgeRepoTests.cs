using System;
using ChallengeThreeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeThreeTests
{
    [TestClass]
    public class BadgeRepoTests
    {
        BadgeRepo _repo;
        Badge badge;
        Badge badgeTwo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new BadgeRepo();
            badge = new Badge(1212, "Security", "D7", "A5", "B3");
            badgeTwo = new Badge(1234, "Cleaner");
        }
        [TestMethod]

        public void AddBadgeToRepo()
        {
            _repo.AddBadgeToRepo(badge);
            _repo.AddBadgeToRepo(badgeTwo);

            Assert.AreEqual(2, _repo.GetDictionaryOfBadges().Count);
            Assert.AreEqual("Cleaner", _repo.GetBadge(1234).BadgeName);
            Assert.IsTrue(_repo.GetBadge(1212).DoorsAccessable.Contains("A5"));
            Assert.IsFalse(_repo.AddBadgeToRepo(badge));
        }

        [TestMethod]
        public void AddBadgesToRepo()
        {
            _repo.AddBadgeToRepo(badge, badgeTwo);

            Assert.AreEqual(2, _repo.GetDictionaryOfBadges().Count);
            Assert.AreEqual("Cleaner", _repo.GetBadge(1234).BadgeName);
            Assert.IsTrue(_repo.GetBadge(1212).DoorsAccessable.Contains("A5"));

        }

        [TestMethod]
        public void GetBadgeTest()
        {
            _repo.AddBadgeToRepo(badge, badgeTwo);

            Assert.IsNull(_repo.GetBadge(12));
            Assert.AreEqual("Security", _repo.GetBadge(1212).BadgeName);
        }

        [TestMethod]
        public void UpdateTest()
        {
            _repo.AddBadgeToRepo(badge, badgeTwo);

            Assert.IsTrue(_repo.GetBadge(1212).DoorsAccessable.Contains("A5"));

            badge.RemoveDoor("A5");
            _repo.UpdateBadge(badge);

            Assert.IsFalse(_repo.GetBadge(1212).DoorsAccessable.Contains("A5"));
        }

        [TestMethod]
        public void DeleteTest()
        {
            _repo.AddBadgeToRepo(badge, badgeTwo);

            _repo.DeleteBadge(1212);

            Assert.AreEqual(1, _repo.GetDictionaryOfBadges().Count);
            Assert.IsTrue(_repo.DeleteBadge(1234));
            Assert.IsFalse(_repo.DeleteBadge(1234));
        }




    }
}
