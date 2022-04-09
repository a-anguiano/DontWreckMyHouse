namespace DontWreckMyHouse.Core
{
    public class Host
    {
        public string Id { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public decimal StandardRate { get; set; }
        public decimal WeekendRate { get; set; }                

        public Host() { }

        public Host(string id, string lastName, string email, string phone, string address, 
            string city, string state, int zip, decimal standardRate, decimal weekendRate)
        {
            Id = id;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            StandardRate = standardRate;
            WeekendRate = weekendRate;
        }

        public override bool Equals(object obj)
        {
            return obj is Host host &&
                   Id == host.Id &&
                   Email == host.Email &&
                   LastName == host.LastName &&
                   Phone == host.Phone &&
                   Address == host.Address &&
                   City == host.City &&
                   State == host.State &&
                   Zip == host.Zip &&
                   StandardRate == host.StandardRate &&
                   WeekendRate == host.WeekendRate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email, LastName, Phone, Address, City, State, Zip);
        }
    }
}