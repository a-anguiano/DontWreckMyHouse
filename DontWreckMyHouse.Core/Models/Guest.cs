namespace DontWreckMyHouse.Core.Models
{
    public class Guest
    {
        public string Id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
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

        public override int GetHashCode()       
        {
            return HashCode.Combine(Id, FirstName, LastName, Email, Phone, State);
        }
    }
}
