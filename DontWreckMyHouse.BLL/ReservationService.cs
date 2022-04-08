using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Interfaces;
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


        public Result<Reservation> Create(Reservation reservation, Host host, Guest guest)    //Guest guest, Host host
        {
            Result<Reservation> result = Validate(reservation, host, guest);  //phone?
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Create(reservation, host, guest);

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

        private Result<Reservation> Validate(Reservation reservation, Host host, Guest guest)
        {
            Result<Reservation> result = ValidateNulls(reservation, host, guest);
            if (!result.Success)
            {
                return result;
            }

            ValidateFields(reservation, result);
            if (!result.Success)
            {
                return result;
            }

            ValidateChildrenExist(reservation, guest, host, result);
            return result;
        }

        private void ValidateNoOverlap(Reservation reservation, string hostId, Result<Reservation> result)
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(hostId);
            //timespan

            if (reservations.Any(r => r.StartDate == reservation.StartDate))
            {
                result.AddMessage("Cannot enter an overlapping date");
            }
            //Any Overlapping dates for list of reservations at specific host/location
        }
        private Result<Reservation> ValidateNulls(Reservation reservation, Host host, Guest guest)
        {
            var result = new Result<Reservation>();

            if (reservation == null)
            {
                result.AddMessage("Nothing to save.");
                return result;
            }

            if (host.Id == null)
            {
                result.AddMessage("Host is required.");
            }

            if (guest.Id == null)
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
            if (reservation.StartDate < reservation.EndDate)
            {
                result.AddMessage("Start date cannot be before end date.");
            }

            if (reservation.StartDate > DateTime.Today)
            {
                result.AddMessage("Start date must be in the future.");
            }
        }

        private void ValidateChildrenExist(Reservation reservation, Guest guest, Host host, Result<Reservation> result)
        {
            if (guest.Id == null
                    || guestRepo.FindByPhone(reservation.Guest.Phone) == null)
            {
                result.AddMessage("Guest does not exist.");
            }

            if (hostRepo.FindByState(host.State) == null)        //host hmm, may need findbyid or phone
            {
                result.AddMessage("Host does not exist.");
            }
        }
    }
}