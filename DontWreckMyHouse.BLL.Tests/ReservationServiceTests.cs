using DontWreckMyHouse.BLL.Tests.RepoDoubles;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;

namespace DontWreckMyHouse.BLL.Tests
{
    public class ReservationServiceTests
    {
        ReservationService service = new ReservationService(
           new ReservationRepoDouble(),
           new GuestRepoDouble(),
           new HostRepoDouble());

        DateTime startDateValid = new DateTime(2022, 7, 7); //could add from current date
        DateTime endDateValid = new DateTime(2022, 7, 10);

        [Test]
        public void ShouldCreate()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateValid;
            reservation.EndDate = endDateValid;
            reservation.TotalCost = 900M;                   //host math
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;      //could make guest 2

            Result<Reservation> result = service.Create(reservation, host, guest);
            Assert.IsTrue(result.Success);
            Assert.NotNull(result.Value);
            //Assert.AreEqual(36, result.Value.Id.Length);
        }

        //UI view and controller may cover host and guest null
        [Test]
        public void ShouldNotCreateWhenHostNotFound()
        {
            //id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate
            string invalidID = Guid.NewGuid().ToString();
            Host host = new Host(invalidID, "Torrance", "overlookHotel@king.com", "(975) 8675309", "Overlook Hotel",
                "Denver", "CO", 75077, 200, 250);


            Reservation reservation = new Reservation();
            reservation.Id = 2;
            reservation.StartDate = startDateValid;
            reservation.EndDate = endDateValid;
            reservation.TotalCost = 900M;                   //host math
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;      //could make guest 2

            Result<Reservation> result = service.Create(reservation, host, guest);
            Assert.IsFalse(result.Success);
        }

        //ShouldNotCreateWhen...
    }
}