using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL
{
    public class ReservationFileRepo : IReservationRepo
    {
        public List<Reservation> FindByHost(Host host)
        {
            throw new NotImplementedException();
        }

        public Reservation Create(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Reservation reservation)         //bool?
        {
            throw new NotImplementedException();
        }

        public Reservation Cancel(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}