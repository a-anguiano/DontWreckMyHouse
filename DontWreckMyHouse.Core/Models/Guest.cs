using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Guest
    {
        public string Id { get; set; }  //simple sequential, string is ok since not adding or removing guests
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }   //string or int...
        public string State { get; set; }

        public Guest() { }

        public Guest(string id, string firstName, string lastName, string email, string phone, string state)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            State = state;
        }

        //consider any overrides
        public override bool Equals(object obj)
        {
            return obj is Guest guest &&
                   Id == guest.Id &&
                   FirstName == guest.FirstName &&
                   LastName == guest.LastName &&
                   Email == guest.Email &&
                   Phone == guest.Phone &&
                   State == guest.State;
        }

        public override int GetHashCode()       //helpful when equality testing the object
        {
            return HashCode.Combine(Id, FirstName, LastName, Email, Phone, State);
        }
    }
}
