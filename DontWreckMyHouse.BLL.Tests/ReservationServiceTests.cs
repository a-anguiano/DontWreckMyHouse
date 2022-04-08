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
        DateTime startDateInvalid = new DateTime(2022, 7, 2);   //taken 7/2
        DateTime endDateInvalid = new DateTime(2022, 7, 4);  //taken 7/4
        DateTime invalidDate = new DateTime(2022, 7, 3);    //in middle of dates taken

        [Test]
        public void ShouldCalculateTotal_WithReservation()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateValid;
            reservation.EndDate = endDateValid;                  
            Host host = HostRepoDouble.HOST;    //weekend 425, standard 340, thurs-sun
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;
            decimal total = service.CalculateTotal(reservation);
            Assert.AreEqual(1530M, total);
        }

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

            Result<Reservation> result = service.Create(reservation);
            Assert.IsTrue(result.Success);
            Assert.NotNull(result.Value);
        }

        //UI view and controller may cover host and guest null
        [Test]
        public void ShouldNotCreateWhenOverlapOfDates_WithInvalidStartDateOnEdge()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateInvalid;
            reservation.EndDate = endDateValid;
            reservation.TotalCost = 900M;                   
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;     

            Result<Reservation> result = service.Create(reservation);
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ShouldNotCreateWhenOverlapOfDates_WithInvalidEndDateOnEdge()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateValid;
            reservation.EndDate = endDateInvalid;
            reservation.TotalCost = 900M;                  
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;      

            Result<Reservation> result = service.Create(reservation);
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ShouldNotCreateWhenOverlapOfDates_WithInvalidStartDateInMiddle()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = invalidDate;
            reservation.EndDate = endDateValid;
            reservation.TotalCost = 900M;                 
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;      

            Result<Reservation> result = service.Create(reservation);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotCreateWhenOverlapOfDates_WithInvalidEndDateInMiddle()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateValid;
            reservation.EndDate = invalidDate;
            reservation.TotalCost = 900M;
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;

            Result<Reservation> result = service.Create(reservation);
            Assert.IsFalse(result.Success);
        }
    }
}