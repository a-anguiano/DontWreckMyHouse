using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservationRepo
    {
        //FindBy...//Host?
        //Find All & Find A Specific One
            //Two types of findbys perhaps

        List<Reservation> FindByHost(Host host);

        Reservation Create(Reservation reservation);

        bool Edit(Reservation reservation);         //bool?

        Reservation Cancel(Reservation reservation);
    }
}
