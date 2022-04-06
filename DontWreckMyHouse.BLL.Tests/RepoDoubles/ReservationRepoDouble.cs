using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Interfaces;

namespace DontWreckMyHouse.BLL.Tests.RepoDoubles
{
    public class ReservationRepoDouble //: IReservationRepo
    {
        DateTime startDate = new DateTime(2020, 6, 26);
        DateTime endDate = new DateTime(2020,7, 1);

        private readonly List<Reservation> reservations = new List<Reservation>();

        public ReservationRepoDouble()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 1;
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.TotalCost = 200M;                   //look up
            reservation.Host = HostRepoDouble.HOST;
            reservation.Guest = GuestRepoDouble.GUEST;
            reservations.Add(reservation);
        }

        //public Forage Add(Forage forage)
        //{
        //    forage.Id = Guid.NewGuid().ToString();
        //    forages.Add(forage);
        //    return forage;
        //}

        //public List<Forage> FindByDate(DateTime date)
        //{
        //    return forages
        //        .Where(i => i.Date.Date == date.Date)
        //        .ToList();
        //}

        //public bool Update(Forage forage)
        //{
        //    return false;
        //}
    }
}
