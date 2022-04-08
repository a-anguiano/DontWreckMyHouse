using DontWreckMyHouse.BLL.Tests.RepoDoubles;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;

namespace DontWreckMyHouse.BLL.Tests
{
    public class ReservationServiceTests
    {
        private ReservationService service;

        [SetUp]
        public void SetUp()
        {
            service = new ReservationService(
           new ReservationRepoDouble(),
           new GuestRepoDouble(),
           new HostRepoDouble());
        }

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

        [Test]
        public void ShouldEdit_ReturnsEditedReservationWithoutChangingAnythingButStartOrEndDate()
        {
            Host host = HostRepoDouble.HOST;
            Guest guest = GuestRepoDouble.GUEST;
            //id,start_date,end_date,guest_id,total
            var updatedReservation = new Reservation
            {
                Id = 1,
                StartDate = DateTime.Parse("6/26/2022"),
                EndDate = DateTime.Parse("7/1/2022"),
                Host = host,
                Guest = guest,
                TotalCost = 2125M                 //new Total is Sun-Fr, 340/425 => 2125M                               
        };

            Result<Reservation> updatedResult = service!.Edit(updatedReservation);

            Assert.IsTrue(updatedResult.Success);
            Assert.NotNull(updatedResult.Value);

            var updatedReservationById = service!.GetReservationById(updatedResult.Value);    //Reservation
            var reservations = service!.FindByHost(updatedReservation.Host);
            
            // TODO: Add more asserts for additional fields
            Assert.AreEqual(2, reservations.Count);
            Assert.AreEqual(DateTime.Parse("6/26/2022"), updatedReservationById.StartDate);
            Assert.AreEqual(DateTime.Parse("7/1/2022"), updatedReservationById.EndDate);            
        }

        
    }
}