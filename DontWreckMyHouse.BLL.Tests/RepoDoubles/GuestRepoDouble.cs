using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL.Tests.RepoDoubles
{
    public class GuestRepoDouble : IGuestRepo
    {
        public static readonly Guest GUEST = new Guest("1", "Sullivan", "Lomas", "slomas0@mediafire.com", "(702) 7768761", "NV");
        //public static readonly Guest GUEST2 = new Guest("2", "Olympie", "Gecks", "ogecks1@dagondesign.com", "(202) 2528316", "DC");

        //private List<Guest> guests = new List<Guest>();

        //public GuestRepoDouble()
        //{
        //    guests.Add(GUEST);
        //    guests.Add(GUEST2);
        //}

        //DAL take cares of finding phone

        public Guest FindByPhone(string phone)        //string or int
        {
            return GUEST;
        }
}
}
