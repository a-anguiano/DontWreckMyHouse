using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservationRepo
    {
        List<Reservation> FindByHostID(string hostId);     

        Reservation Create(Reservation reservation); 

        Reservation Edit(Reservation reservation);       

        Reservation Cancel(Reservation reservation);
    }
}
