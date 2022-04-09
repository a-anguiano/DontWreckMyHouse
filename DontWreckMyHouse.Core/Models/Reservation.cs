namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }  
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public Host Host { get; set; }      
        public Guest Guest { get; set; }

        public decimal TotalCost { get; set; }

    }
}
