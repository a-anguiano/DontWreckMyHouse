using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System.IO;
using DontWreckMyHouse.Core.Exceptions;

namespace DontWreckMyHouse.DAL
{
    public class ReservationFileRepo : IReservationRepo
    {
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private readonly string directory;

        public ReservationFileRepo(string directory)  
        {
            this.directory = directory;
        }

        public List<Reservation> FindByHostID(string hostId)      //and Id?
        {
            var reservations = new List<Reservation>();
            var path = GetFilePath(hostId);

            //if (!File.Exists(path))
            //{
            //    return _reservations;           //return type....
            //}

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException ex)
            {
                throw new RepoExceptions("Could not read reservations.", ex);
            }

            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Reservation reservation = Deserialize(fields, hostId);                

                if (reservation != null)
                {
                    Host host = new Host();
                    host.Id = hostId;
                    reservation.Host = host;
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }
    

        public Reservation Create(Reservation reservation)      //two params?Host host
        {
            List<Reservation> all = FindByHostID(reservation.Host.Id);  //altered

            int nextId = (all.Count == 0 ? 0 : all.Max(i => i.Id)) + 1;
            reservation.Id = nextId;

            all.Add(reservation);
            Write(all, reservation.Host.Id);
            return reservation;
        }

        public Reservation Edit(Reservation reservationToUpdate)         //bool?
        {
            //var result = new Result<>
            List<Reservation> all = FindByHostID(reservationToUpdate.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id != reservationToUpdate.Id)
                {
                    continue; 
                }
                all[i] = reservationToUpdate;
                Write(all, reservationToUpdate.Host.Id);
                return reservationToUpdate;
            }
            return reservationToUpdate;
        }

        public Reservation Cancel(Reservation reservation)
        {
            List<Reservation> all = FindByHostID(reservation.Host.Id);

            for(int i = 0; i < all.Count; i++)
            {
                if(all[i].Id == reservation.Id)
                {
                    all.RemoveAt(i);
                    Write(all, reservation.Host.Id);
                    return reservation;
                }
            }
            return reservation;
        }

        private string GetFilePath(string guid)
        {
            return Path.Combine(directory, $"{guid}.csv");
        }

        private string Serialize(Reservation item)
        {
            return string.Format("{0},{1},{2},{3},{4:0.00}",
                    item.Id,
                    item.StartDate,
                    item.EndDate,
                    item.Guest.Id,
                    item.TotalCost);
        }

        private Reservation Deserialize(string[] fields, string guid)
        {
            if (fields.Length != 5)  
            {
                return null;
            }

            Reservation result = new Reservation();
            result.Id = int.Parse(fields[0]);          //hmmm
            result.StartDate = DateTime.Parse(fields[1]);
            result.EndDate = DateTime.Parse(fields[2]);

            Guest guest = new Guest();
            guest.Id = fields[3];
            result.Guest = guest;                      //
            result.TotalCost = decimal.Parse(fields[4]);
            return result;
        }

        private void Write(List<Reservation> reservations, string guid)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(GetFilePath(guid));
                writer.WriteLine(HEADER);

                if (reservations == null)
                {
                    return;
                }

                foreach (var reservation in reservations)
                {
                    writer.WriteLine(Serialize(reservation));
                }
            }
            catch (IOException ex)
            {
                throw new RepoExceptions("Could not write reservation.", ex);
            }
        }
    }
}