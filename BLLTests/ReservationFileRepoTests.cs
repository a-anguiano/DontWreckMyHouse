using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.DAL.Tests
{
    [TestFixture]
    public class ReservationFileRepoTests
    {
        const string SEED_DIRECTORY = "TestData";
        const string SEED_FILE = "TestSeed.csv";
        const string TEST_DIRECTORY = "Test";
        const string TEST_FILE = "3f413626-e129-4d06-b68c-36450822213f.csv";

        string SEED_PATH = Path.Combine(SEED_DIRECTORY, SEED_FILE);
        string TEST_PATH = Path.Combine(TEST_DIRECTORY, TEST_FILE);

        const int RESERVATION_COUNT = 13;
        const int NEXT_ID = 14;

        string hostID = "3f413626-e129-4d06-b68c-36450822213f";
        DateTime startDate = new DateTime(2021, 12, 1);
        DateTime endDate = new DateTime(2021, 12, 6);

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

        [Test]
        public void ShouldCreate()
        {
            Reservation expected = MakeResTamAmy();
            expected.Id = NEXT_ID;

            Reservation reservation = MakeResTamAmy();
            //string hostId = Guid.NewGuid().ToString(); 
            Host host = new Host();
            host.Id = hostID;
            reservation.Host = host;
            //Guest guest = new Guest();
            Reservation actual = _repo.Create(reservation);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void ShouldEdit()        //conventions for naming
        {
            Guest guest = new Guest();
            guest.Id = "1001";
            Host host = new Host();
            host.Id = hostID;
            var reservation = new Reservation() { Id = 1, StartDate = startDate, EndDate = endDate, Host = host, Guest = guest, TotalCost = 1200M };
            

            _repo!.Edit(reservation);
            var reservations = _repo!.FindByHostID(reservation.Host.Id);

            Assert.AreEqual(1, reservations[0].Id);
            Assert.AreEqual(startDate, reservations[0].StartDate);
            Assert.AreEqual(endDate, reservations[0].EndDate);
            Assert.AreEqual(host, reservations[0].Host);
            Assert.AreEqual(guest, reservations[0].Guest);
            Assert.AreEqual(1200M, reservations[0].TotalCost);  
        }

        //should cancel
        [Test]
        public void ShouldCancel()
        {
            //2,2021-11-04,2021-11-07,937,602
            DateTime startDateCancel = new DateTime(2021, 11, 4);
            DateTime endDateCancel = new DateTime(2021, 11, 7);
            Guest guest = new Guest();
            guest.Id = "937";
            var reservation = new Reservation() { Id = 2, StartDate = startDateCancel, EndDate = endDateCancel, Guest = guest, TotalCost = 602M };
            _repo!.Cancel(reservation, hostID);
            var reservations = _repo!.FindByHostID(hostID);

            Assert.AreEqual(12, reservations.Count);
        }

        //shouldNotcancel

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
