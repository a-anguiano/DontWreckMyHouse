using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.DAL.Tests
{
    [TestFixture]
    public class ReservationFileRepoTests
    {
        //C:\Users\19722\Code\MasteryAssessment\DontWreckMyHouse\DontWreckMyHouse.UI
        const int RESERVATION_COUNT = 13;

        //Host host = new Host();     //hmmmmmmmm
        string hostID = "3f413626-e129-4d06-b68c-36450822213f";

        [SetUp]
        public void SetUp()
        {
            //if (File.Exists(LogFile)) File.Delete(LogFile);

            if (File.Exists(TestDataFile)) File.Delete(TestDataFile);

            File.Copy(SeedFile, TestDataFile);

            _repo = new ReservationFileRepo(TestDataFile);
        }

        //private const string LogFile = "../../../DAL/log.error.csv";
        private const string SeedFile = "../../../TestData/reservation-seed.csv"; 
        private const string TestDataFile = "../../../TestData/3f413626-e129-4d06-b68c-36450822213f.csv";

        private ReservationFileRepo? _repo;

        [Test]
        public void CanCreateTestFile()
        {
            Assert.IsTrue(File.Exists(TestDataFile));
        }

        [Test]
        public void ShouldFindByHostID()
        {
            List<Reservation> reservations = _repo!.FindByHostID(hostID);   //! to get rid of green squiggle  
            Assert.AreEqual(RESERVATION_COUNT, reservations.Count);
        }
    }
}
