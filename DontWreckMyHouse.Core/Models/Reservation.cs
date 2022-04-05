using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public string Id { get; set; }  //simple sequential, but filename for res is GUID
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public Host Host { get; set; }
        public Guest Guest { get; set; }     

        //send in date for each day reserved
        //figure out cost of a single day?
        //is the issue current the loop within a model for some reason?

    public decimal TotalCost         //cost of ONE day or cost of TOTAL     //readonly?
        {
            get
            {
                decimal total;
                for (DateTime date = StartDate; date.Date <= EndDate.Date; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        total += Host.WeekendRate;
                    }
                    else
                    {
                        total += Host.StandardRate;
                    }
                }
                return total;
            }
        }

        //consider any LINQ
        //d => d.DayOfWeek == d.Saturday || d.DayOfWeek == d.Sunday //meh
        //StayDays.Where(d => Weekday(d) == 1 || Weekday(d) == 7)
    }
}
