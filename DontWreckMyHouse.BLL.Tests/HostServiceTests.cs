using System.Collections.Generic;
using DontWreckMyHouse.BLL.Tests.RepoDoubles;
using DontWreckMyHouse.Core;
using NUnit.Framework;

namespace DontWreckMyHouse.BLL.Tests
{
    public class HostServiceTests
    {
        private HostService service;

        [SetUp]
        public void SetUp()
        {
            service = new HostService(new HostRepoDouble());
        }

        [Test]
        public void ShouldFindHosts_WithState()
        {
            var actual = service.FindByState("TX");
            List<Host> expectedHosts = new List<Host>();
            expectedHosts.Add(HostRepoDouble.HOST);
            expectedHosts.Add(HostRepoDouble.HOST3);

            Assert.That(actual, Is.EquivalentTo(expectedHosts));
        }

        [Test]
        public void ShouldFindHost_WithPhone()
        {
            var actual = service.FindByPhone("(713) 3421887");
            Host expectedHost = HostRepoDouble.HOST3;

            Assert.AreEqual(actual, expectedHost);
        }
    }
}
