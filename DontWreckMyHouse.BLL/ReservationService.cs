using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IReservationRepo reservationRepo;
        private readonly IGuestRepo guestRepo;
        private readonly IHostRepo hostRepo;

        public ReservationService(IReservationRepo reservationRepo, IGuestRepo guestRepo, IHostRepo hostRepo)
        {
            this.reservationRepo = reservationRepo;
            this.guestRepo = guestRepo;
            this.hostRepo = hostRepo;
        }

        public List<Reservation> FindByHost(Host host)      //only core, not model
        {
            List<Reservation> result = reservationRepo.FindByHostID(host.Id);        //reservationFileRepo
            return result;
        }

        public decimal CalculateTotal(Reservation reservation)
        {
            //TimeSpan stayDays = reservation.EndDate.Subtract(reservation.StartDate);
            //stayDays.Where(d => Weekday(d) == 1 || Weekday(d) == 7);

            decimal total = 0;
            for (DateTime date = reservation.StartDate; date.Date <= reservation.EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    total += reservation.Host.WeekendRate;
                }
                else
                {
                    total += reservation.Host.StandardRate;
                }
            }

            return total;
            //consider any LINQ
            //d => d.DayOfWeek == d.Saturday || d.DayOfWeek == d.Sunday //meh
            //StayDays.Where(d => Weekday(d) == 1 || Weekday(d) == 7)
        }

        public Result<Reservation> Create(Reservation reservation)    //Guest guest, Host host
        {
            Result<Reservation> result = Validate(reservation);  //phone?
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Create(reservation);

            return result;
        }

        public bool Edit(Reservation reservation)         //bool?
        {
            throw new NotImplementedException();
        }

        public Reservation Cancel(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        private Result<Reservation> Validate(Reservation reservation) //, Host host, Guest guest)
        {
            Result<Reservation> result = ValidateNulls(reservation);
            if (!result.Success)
            {
                return result;
            }

            ValidateFields(reservation, result);
            if (!result.Success)
            {
                return result;
            }

            ValidateChildrenExist(reservation, result);
            return result;

            //MOVE TO HERE
        }

        private void ValidateNoOverlap(Reservation reservation, Result<Reservation> result)
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(reservation.Host.Id);
            //timespan

            if (reservations.Any(r => r.StartDate >= reservation.StartDate && r.StartDate >= reservation.EndDate
            || r.EndDate <= reservation.StartDate && r.EndDate >= reservation.EndDate))
            {
                result.AddMessage("Cannot enter an overlapping date");
            }
            //Any Overlapping dates for list of reservations at specific host/location
        }
        private Result<Reservation> ValidateNulls(Reservation reservation)
        {
            var result = new Result<Reservation>();

            if (reservation == null)
            {
                result.AddMessage("Nothing to save.");
                return result;
            }

            if (reservation.Host == null)
            {
                result.AddMessage("Host is required.");
            }

            if (reservation.Guest == null)
            {
                result.AddMessage("Guest is required.");
            }

            //if (reservation.StartDate == null)
            //{
            //    result.AddMessage("Start Date is required.");
            //}
            //if (reservation.EndDate == null)
            //{
            //    result.AddMessage("End Date is required.");
            //}

            return result;
        }

        private void ValidateFields(Reservation reservation, Result<Reservation> result)
        {
            if (reservation.StartDate > reservation.EndDate)
            {
                result.AddMessage("Start date cannot be before end date.");
            }

            if (reservation.StartDate < DateTime.Today)
            {
                result.AddMessage("Start date must be in the future.");
            }
        }

        private void ValidateChildrenExist(Reservation reservation, Result<Reservation> result)
        {
            if (guestRepo.FindByPhone(reservation.Guest.Phone) == null)
            {
                result.AddMessage("Guest does not exist.");
            }

            if (hostRepo.FindByState(reservation.Host.State) == null)        //host hmm, may need findbyid or phone!!!
            {
                result.AddMessage("Host does not exist.");
            }
        }
    }
}