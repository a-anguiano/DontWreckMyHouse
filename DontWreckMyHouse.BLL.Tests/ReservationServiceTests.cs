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

        DateTime startDateValid = new DateTime(2022, 7, 7); 
        DateTime endDateValid = new DateTime(2022, 7, 10);
        DateTime startDateInvalid = new DateTime(2022, 7, 2);   
        DateTime endDateInvalid = new DateTime(2022, 7, 4);  
        DateTime invalidDate = new DateTime(2022, 7, 3);    

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
            reservation.TotalCost = 900M;                  
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;      

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
        public void ShouldNotCreateWhenStartDateIsAfterEndDate()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = endDateValid;
            reservation.EndDate = startDateValid;
            reservation.TotalCost = 900M;
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = GuestRepoDouble.GUEST;
            reservation.Guest = guest;

            Result<Reservation> result = service.Create(reservation);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotCreateWhenStartDateIsNotInTheFuture()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateValid.AddYears(-1);
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
        public void ShouldNotCreateWhenGuestIsNull()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateValid.AddYears(-1);
            reservation.EndDate = endDateValid;
            reservation.TotalCost = 900M;
            Host host = HostRepoDouble.HOST;
            reservation.Host = host;
            Guest guest = null;
            reservation.Guest = guest;

            Result<Reservation> result = service.Create(reservation);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldNotCreateWhenHostIsNull()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 3;
            reservation.StartDate = startDateValid.AddYears(-1);
            reservation.EndDate = endDateValid;
            reservation.TotalCost = 900M;
            Host host = null;
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

            var updatedReservationById = service!.GetReservationById(updatedResult.Value);   
            var reservations = service!.FindByHost(updatedReservation.Host);
            
            // TODO: Add more asserts for additional fields
            Assert.AreEqual(2, reservations.Count);
            Assert.AreEqual(DateTime.Parse("6/26/2022"), updatedReservationById.StartDate);
            Assert.AreEqual(DateTime.Parse("7/1/2022"), updatedReservationById.EndDate);            
        }

        [Test]
        public void ShouldCancel()
        {
            DateTime startDate1 = new DateTime(2022, 7, 2);
            DateTime endDate1 = new DateTime(2022, 7, 4);

            Reservation reservation1 = new Reservation();
            reservation1.Id = 2;
            reservation1.StartDate = startDate1;
            reservation1.EndDate = endDate1;
            reservation1.TotalCost = 250M;
            reservation1.Host = HostRepoDouble.HOST;
            reservation1.Guest = GuestRepoDouble.GUEST;

            var deleteResult = service!.Cancel(reservation1);
            Assert.IsTrue(deleteResult.Success);

            var reservations = service.FindByHost(reservation1.Host);

            Assert.AreEqual(1, reservations.Count);                       
        }

        [Test]
        public void ShouldNotCancel()
        {
            DateTime startDate1 = new DateTime(2021, 7, 2);
            DateTime endDate1 = new DateTime(2022, 7, 4);

            Reservation reservation1 = new Reservation();
            reservation1.Id = 2;
            reservation1.StartDate = startDate1;
            reservation1.EndDate = endDate1;
            reservation1.TotalCost = 250M;
            reservation1.Host = HostRepoDouble.HOST;
            reservation1.Guest = GuestRepoDouble.GUEST;

            var deleteResult = service!.Cancel(reservation1);
            Assert.IsFalse(deleteResult.Success);

            var reservations = service.FindByHost(reservation1.Host);

            Assert.AreEqual(2, reservations.Count);
        }
    }
}