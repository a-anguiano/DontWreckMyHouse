using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL
{
    public class GuestService
    {
        private readonly IGuestRepo repository;

        public GuestService(IGuestRepo repository)
        {
            this.repository = repository;
        }
        public Guest FindByPhone(string phone)
        {
            return repository.FindByPhone(phone);
        }

        public List<Guest> FindById(List<Reservation> reservations)
        {
            List<Guest> guests = new List<Guest>();
            foreach(Reservation reservation in reservations)
            {
                Guest guest = repository.FindById(reservation.Guest.Id);
                guests.Add(guest);
            }

            return guests;
        }
    }
}
