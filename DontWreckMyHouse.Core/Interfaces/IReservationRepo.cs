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

        List<Reservation> FindByHostID(string hostId);      //hmmm

        Reservation Create(Reservation reservation); //changed

        Reservation Edit(Reservation reservation);         //bool?

        Reservation Cancel(Reservation reservation, string hostId);
    }
}
