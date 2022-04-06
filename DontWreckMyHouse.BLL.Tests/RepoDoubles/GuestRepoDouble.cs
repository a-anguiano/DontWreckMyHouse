using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL.Tests.RepoDoubles
{
    public class GuestRepoDouble //: //IGuestRepo
    {
        //1,Sullivan,Lomas,slomas0@mediafire.com,(702) 7768761,NV
        public static readonly Guest GUEST = new Guest("1", "Smith", "Thomas", "email.msn", "(972) 8675309", "TX");
        private List<Guest> guests = new List<Guest>();

        //public HostRepoDouble()
        //{
        //    hosts.Add(HOST);
        //}

        //public Guest FindByPhone(string phone)        //string or int
        //{
        //    return new Guest(guests);
        //}
    }
}
