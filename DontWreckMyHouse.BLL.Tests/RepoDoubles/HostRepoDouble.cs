using System.Collections.Generic;
using System.Linq;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.BLL.Tests.RepoDoubles
{
    public class HostRepoDouble : IHostRepo
    {
        public static readonly Host HOST = new Host("3edda6bc-ab95-49a8-8962-d50b53f84b15", "Yearnes", "eyearnes0@sfgate.com",
            "(806) 1783815", "3 Nova Trail", "Amarillo", "TX", 79182, 340, 425);
        public static readonly Host HOST2 = new Host("a0d911e7-4fde-4e4a-bdb7-f047f15615e8", "Rhodes", 
            "krhodes1@posterous.com", "(478) 7475991", "7262 Morning Avenue", "Macon", "GA", 31296, 295, (decimal)368.75);
        public static readonly Host HOST3 = new Host("d491d4c3-e005-4494-9c52-4d3be265fd76", "Valasek", "hvalasek5@fastcompany.com",
            "(713) 3421887", "8113 Lunder Crossing", "Houston", "TX", 77005, 387, (decimal)483.75);

        private List<Host> hosts = new List<Host>();

        public HostRepoDouble()
        {
            hosts.Add(HOST);
            hosts.Add(HOST2);     //DAL is to get the right states????
            hosts.Add(HOST3);
        }

        public List<Host> FindByState(string stateAbbr)
        {
            return hosts.Where(h => h.State == stateAbbr).ToList();
        }

        public Host FindByPhone(string phone)
        {
            return hosts.First(h => h.Phone == phone);
        }
    }
}
