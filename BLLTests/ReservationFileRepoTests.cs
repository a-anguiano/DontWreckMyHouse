using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL.Tests
{
    [TestFixture]
    public class ReservationFileRepoTests
    {
        //C:\Users\19722\Code\MasteryAssessment\DontWreckMyHouse\DontWreckMyHouse.UI
        const string SEED_DIRECTORY = "TestData";
        const string SEED_FILE = "TestSeed.csv";
        const string TEST_DIRECTORY = "Test";
        const string TEST_FILE = "3f413626-e129-4d06-b68c-36450822213f.csv";

        string SEED_PATH = Path.Combine(SEED_DIRECTORY, SEED_FILE);
        string TEST_PATH = Path.Combine(TEST_DIRECTORY, TEST_FILE);

        const int RESERVATION_COUNT = 13;
        const int NEXT_ID = 14;

        //Host host = new Host();     //hmmmmmmmm
        string hostID = "3f413626-e129-4d06-b68c-36450822213f";
        DateTime startDate = new DateTime(21, 12, 1);
        DateTime endDate = new DateTime(21, 12, 6);

        private ReservationFileRepo _repo;
        
        [SetUp]
        public void SetUp()
        {
            //if (File.Exists(LogFile)) File.Delete(LogFile);
            if (!Directory.Exists(TEST_DIRECTORY))
            {
                Directory.CreateDirectory(TEST_DIRECTORY);
            }

            //if (File.Exists(TEST_FILE)) File.Delete(TEST_FILE);

            File.Copy(SEED_PATH, TEST_PATH, true);

            _repo = new ReservationFileRepo(TEST_DIRECTORY);
        }

        [Test]
        public void CanCreateTestFile()
        {
            Assert.IsTrue(File.Exists(TEST_PATH));
        }

        [Test]
        public void ShouldFindByHostID()
        {
            List<Reservation> reservations = _repo.FindByHostID(hostID);  
            Assert.AreEqual(RESERVATION_COUNT, reservations.Count);
        }

        //should create
        [Test]
        public void ShouldCreate()
        {
            Reservation expected = MakeResTamAmy();
            expected.Id = NEXT_ID;

            Reservation reservation = MakeResTamAmy();
            //string hostId = Guid.NewGuid().ToString(); 
            Reservation actual = _repo.Create(reservation, hostID);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        //should edit
        //should cancel

        private Reservation MakeResTamAmy()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 14;     //perhaps
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            Guest guest = new Guest();
            guest.Id = "1001";
            reservation.Guest = guest;      //int?
            reservation.TotalCost = 1200M;
            return reservation;
        }
    }
}
