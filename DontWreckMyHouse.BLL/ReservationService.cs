using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Interfaces;

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

        public List<Reservation> FindByHost(Host host)     
        {
            List<Reservation> result = reservationRepo.FindByHostID(host.Id);       
            return result;
        }

        public List<Reservation> FindReservationsForHostAndGuest(Host host, Guest guest)
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(host.Id);

            return reservations.Where(r => r.Guest.Id == guest.Id).ToList();
        }

        public Reservation GetReservationById(Reservation reservation)      //By host id
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(reservation.Host.Id); //HERE

            var result = reservations.First(r => r.Id == reservation.Id);
            return result;
        }

        public decimal CalculateTotal(Reservation reservation)
        {
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

        public Result<Reservation> Edit(Reservation reservationToUpdate)       
        {
            Result<Reservation> result = ValidateForEdit(reservationToUpdate);    
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Edit(reservationToUpdate);
            return result;
        }

        public Result<Reservation> Cancel(Reservation reservationToDelete)
        {
            Result<Reservation> result = ValidateForCancel(reservationToDelete); 
            if (!result.Success)
            {
                return result;
            }

            result.Value = reservationRepo.Cancel(reservationToDelete);

            return result;
        }

        private Result<Reservation> Validate(Reservation reservation) 
        {
            var result = new Result<Reservation>();
            ValidateNulls(reservation, result); //Result<Reservation> result = 
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
            
            ValidateNoOverlap(reservation, result); //except the one editing
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

        private void ValidateNoOverlapForEdit(Reservation reservation, Result<Reservation> result)
        {
            List<Reservation> reservations = reservationRepo.FindByHostID(reservation.Host.Id);

            if (reservations.Where(r => r.Id != reservation.Id).Any(r => r.StartDate >= reservation.StartDate && r.StartDate <= reservation.EndDate
            || r.EndDate >= reservation.StartDate && r.EndDate <= reservation.EndDate))
            {
                result.AddMessage("Cannot enter an overlapping date");
            }
        }

        private Result<Reservation> ValidateForEdit(Reservation reservation)
        {
            var result = new Result<Reservation>();

            ValidateFields(reservation, result);
            if (!result.Success)
            {
                return result;
            }

            ValidateNoOverlapForEdit(reservation, result);
            return result;
        }

        private Result<Reservation> ValidateForCancel(Reservation reservation)
        {
            var result = new Result<Reservation>();
            ValidateNulls(reservation, result); //Result<Reservation> result = 
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

        private Result<Reservation> ValidateNulls(Reservation reservation, Result<Reservation> result)
        {
            //var result = new Result<Reservation>();

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
            if (guestRepo.FindById(reservation.Guest.Id) == null)
            {
                result.AddMessage("Guest does not exist.");
            }

            if (hostRepo.FindByPhone(reservation.Host.Phone) == null)   //by id?
            {
                result.AddMessage("Host does not exist.");
            }
        }
    }
}