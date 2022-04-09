using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.DAL.Tests
{
    public class HostFileRepoTests
    {
        const string SEED_DIRECTORY = "TestData";
        const string SEED_FILE = "TestHostSeed.csv";
        const string TEST_DIRECTORY = "Test";
        const string TEST_FILE = "hosts.csv";

        string SEED_PATH = Path.Combine(SEED_DIRECTORY, SEED_FILE);
        string TEST_PATH = Path.Combine(TEST_DIRECTORY, TEST_FILE);

        const int HOST_TX_COUNT = 17;     
        const string HOST_KY_NAME = "KY";


        private HostFileRepo _repo;

        [SetUp]
        public void SetUp()
        {
            if (!Directory.Exists(TEST_DIRECTORY))
            {
                Directory.CreateDirectory(TEST_DIRECTORY);
            }

            File.Copy(SEED_PATH, TEST_PATH, true);

            _repo = new HostFileRepo(TEST_PATH);      
        }

        [Test]
        public void ShouldFindByState()
        {
            List<Host> hosts = _repo.FindByState(HOST_KY_NAME);
            Assert.AreEqual(HOST_TX_COUNT, hosts.Count);
        }
    }
}
