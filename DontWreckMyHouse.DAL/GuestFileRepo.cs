using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Models;
using System.IO;
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

        public Guest FindByPhone(string phone)        //string or int
        {
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines("guests.csv");
            }
            catch (IOException ex)
            {
                throw new RepoExceptions("Could not read guest.", ex);
            }

            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);      //, phone
                if (guest != null && guest.Phone == phone)
                {
                    return guest;
                }
            }
            return null;
        }

        //NOT ADDING SO NO NEED TO WRITE OR SERIALIZE
        private Guest Deserialize(string[] fields)      //, string phone
        {
            if (fields.Length != 6)
            {
                return null;
            }

            //guest_id,first_name,last_name,email,phone,state

            Guest result = new Guest();
            result.Id = fields[0];          //hmmm
            result.FirstName = fields[1];
            result.LastName = fields[2];
            result.Email = fields[3];
            result.Phone = fields[4];
            result.State = fields[5];

            return result;
        }
    }
}
