using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        public List<Reservation> FindByHost(Host host)      //only core, not model
        {
            throw new NotImplementedException();
        }


        public Result<Reservation> Create(Reservation reservation)
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