using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Exceptions;

namespace DontWreckMyHouse.DAL
{
    public class GuestFileRepo : IGuestRepo
    {
        private readonly string filePath;

        public GuestFileRepo(string filePath)
        {
            this.filePath = filePath;
        }

        public Guest FindByPhone(string phone)        
        {
            var guestCheck = new Guest();
            if (!File.Exists(filePath))
            {
                return guestCheck;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines("guests.csv");
            }
            catch (IOException ex)
            {
                throw new RepoExceptions("Could not read guest.", ex);
            }

            for (int i = 1; i < lines.Length; i++) 
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);      
                if (guest != null && guest.Phone == phone)
                {
                    return guest;
                }
            }
            return null;
        }

        private Guest Deserialize(string[] fields)     
        {
            if (fields.Length != 6)
            {
                return null;
            }

            Guest result = new Guest();
            result.Id = fields[0];          
            result.FirstName = fields[1];
            result.LastName = fields[2];
            result.Email = fields[3];
            result.Phone = fields[4];
            result.State = fields[5];

            return result;
        }
    }
}
