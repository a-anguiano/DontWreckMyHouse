using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }  //simple sequential, but filename for res is GUID
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public Host Host { get; set; }      //hmmm
        public Guest Guest { get; set; }

        //MAYBE ACTUALLY HAVE GUESTID HOSTID
        public decimal TotalCost { get; set; }

    }
}
