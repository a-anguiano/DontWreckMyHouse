using System;
using System.Collections.Generic;
using System.Linq;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.BLL.Tests.RepoDoubles
{
    public class ReservationRepoDouble : IReservationRepo
    {
        DateTime startDate = new DateTime(2020, 6, 26);
        DateTime endDate = new DateTime(2020,7, 1);
        DateTime startDate1 = new DateTime(2022, 7, 2);
        DateTime endDate1 = new DateTime(2022, 7, 4);

        private readonly List<Reservation> reservations = new List<Reservation>();

        public ReservationRepoDouble()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 1;
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.TotalCost = 200M;                  
            reservation.Host = HostRepoDouble.HOST;
            reservation.Guest = GuestRepoDouble.GUEST;
            reservations.Add(reservation);

            Reservation reservation1 = new Reservation();
            reservation1.Id = 2;
            reservation1.StartDate = startDate1;
            reservation1.EndDate = endDate1;
            reservation1.TotalCost = 250M;
            reservation1.Host = HostRepoDouble.HOST;
            reservation1.Guest = GuestRepoDouble.GUEST;
            reservations.Add(reservation1);
        }

        public List<Reservation> FindByHostID(string hostId)    
        {
            Host host = new Host();
            host.Id = hostId;
                return reservations
                    .Where(i => i.Host.Id == hostId)
                    .ToList();
        }

        public Reservation Create(Reservation reservation)
        {
            List<Reservation> reservations = FindByHostID(reservation.Host.Id);
            reservation.Id = reservations.Max(i => i.Id) + 1;
            reservations.Add(reservation);
            return reservation;
        }

        public Reservation Edit(Reservation reservationToUpdate)
        {
            List<Reservation> all = FindByHostID(reservationToUpdate.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id != reservationToUpdate.Id)
                {
                    continue;
                }
                reservations.Remove(all[i]);
                all[i] = reservationToUpdate;                
                reservations.Add(all[i]);
                return reservationToUpdate;
            }
            return reservationToUpdate;
        }

        public Reservation Cancel(Reservation reservation)
        {
            List<Reservation> all = FindByHostID(reservation.Host.Id);

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id != reservation.Id)
                {
                    continue;
                }
                reservations.RemoveAt(i);
                return reservation;
            }
            return reservation;
        }
    }
}
