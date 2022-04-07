using NUnit.Framework;
using System.IO;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL.Tests
{
    public class GuestFileRepoTests
    {
        const string SEED_DIRECTORY = "TestData";
        const string SEED_FILE = "TestGuestSeed.csv";
        const string TEST_DIRECTORY = "Test";
        const string TEST_FILE = "guests.csv";

        string SEED_PATH = Path.Combine(SEED_DIRECTORY, SEED_FILE);
        string TEST_PATH = Path.Combine(TEST_DIRECTORY, TEST_FILE);

        const string phoneNum = "(585) 3812166";

        private GuestFileRepo _repo;

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

            _repo = new GuestFileRepo(TEST_PATH);       //test path
        }

        [Test]
        public void ShouldFindByPhone()
        {
            //guest_id,first_name,last_name,email,phone,state
            //7,Gilli,Fritz,gfritz6@ustream.tv,(585) 3812166,NY
            Guest guest = _repo.FindByPhone(phoneNum);
            Assert.AreEqual("7", guest.Id);               
            Assert.AreEqual("Gilli", guest.FirstName);
            Assert.AreEqual("Fritz", guest.LastName);
            Assert.AreEqual("gfritz6@ustream.tv", guest.Email);
            Assert.AreEqual("NY", guest.State);
        }
    }
}
