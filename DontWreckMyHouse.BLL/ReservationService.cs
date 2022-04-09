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

        //FindAll()

        public List<Reservation> FindByHost(Host host)      //only core, not model
        {
            List<Reservation> result = reservationRepo.FindByHostID(host.Id);        //reservationFileRepo
            return result;
        }

        public List<Reservation> FindReservationsForHostAndGuest(Host host, Guest guest)
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(host.Id);

            var result = reservations.Where(r => r.Guest.Phone == guest.Phone).ToList();
            return result;
        }

        public Reservation GetReservationById(Reservation reservation)
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(reservation.Host.Id);

            var result = reservations.First(r => r.Id == reservation.Id);
            return result;
        }

        public decimal CalculateTotal(Reservation reservation)
        {
            //Can we use linq?
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
        }

        public Result<Reservation> Create(Reservation reservation)  
        {
            Result<Reservation> result = Validate(reservation); 
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Create(reservation);

            return result;
        }

        public Result<Reservation> Edit(Reservation reservationToUpdate)         //bool?
        {
            Result<Reservation> result = Validate(reservationToUpdate);     //edit controller needs to send in "new"
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Edit(reservationToUpdate);
            return result;
        }

        public Result<Reservation> Cancel(Reservation reservationToDelete)
        {
            Result<Reservation> result = ValidateForCancel(reservationToDelete); //may need to adjust for dates in the past?
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Cancel(reservationToDelete);

            return result;
        }

        private Result<Reservation> Validate(Reservation reservation) 
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
            if (!result.Success)
            {
                return result;
            }
            
            ValidateNoOverlap(reservation, result);
            return result;
        }

        private void ValidateNoOverlap(Reservation reservation, Result<Reservation> result)
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(reservation.Host.Id);

            if (reservations.Any(r => r.StartDate >= reservation.StartDate && r.StartDate <= reservation.EndDate
            || r.EndDate >= reservation.StartDate && r.EndDate <= reservation.EndDate))
            {
                result.AddMessage("Cannot enter an overlapping date");
            }
        }

        private Result<Reservation> ValidateForCancel(Reservation reservation)
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

            //StartDate and EndDate can never be null since type DateTime is never equal to null

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