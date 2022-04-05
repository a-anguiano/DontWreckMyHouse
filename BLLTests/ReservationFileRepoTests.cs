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
    public class ReservationFileRepoTests
    {
        //C:\Users\19722\Code\MasteryAssessment\DontWreckMyHouse\DontWreckMyHouse.UI
        const string SEED_FILE_PATH = @"Data\reservation-seed.csv";
        const string TEST_FILE_PATH = @"Data\reservations\3f413626-e129-4d06-b68c-36450822213f.csv";        
        const string TEST_DIR_PATH = @"Data\reservations";
        const int RESERVATION_COUNT = 13;

        ReservationFileRepo repo = new ReservationFileRepo(TEST_DIR_PATH);

        //Host host = new Host();     //hmmmmmmmm
        string hostID = "3f413626-e129-4d06-b68c-36450822213f";

        [SetUp]
        public void SetUp()
        {
            File.Copy(SEED_FILE_PATH, TEST_FILE_PATH, true);
        }

        [Test]
        public void ShouldFindByHost()      //and Id
        {
            List<Reservation> reservations = repo.FindByHostID(hostID);     //host.Id
            Assert.AreEqual(RESERVATION_COUNT, reservations.Count);
        }
    }
}
