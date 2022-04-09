using DontWreckMyHouse.BLL.Tests.RepoDoubles;
using DontWreckMyHouse.Core.Models;
using NUnit.Framework;

namespace DontWreckMyHouse.BLL.Tests
{
    public class GuestServiceTests
    {
        private GuestService service;

        [SetUp]
        public void SetUp()
        {
            service = new GuestService(new GuestRepoDouble());
        }
        [Test]
        public void ShouldFindGuest_WithPhone()
        {
            var actual = service.FindByPhone("(702) 7768761");
            Guest expectedGuest = GuestRepoDouble.GUEST;

            Assert.AreEqual(actual, expectedGuest);
        }
    }
}
