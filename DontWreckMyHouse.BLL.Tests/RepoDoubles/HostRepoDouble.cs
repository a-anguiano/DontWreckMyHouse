using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.BLL.Tests.RepoDoubles
{
    public class HostRepoDouble : IHostRepo
    {
        //3edda6bc-ab95-49a8-8962-d50b53f84b15,Yearnes,eyearnes0@sfgate.com,(806) 1783815,3 Nova Trail,Amarillo,TX,79182,340,425
        public static readonly Host HOST = new Host("guid", "Smith", "email.msn", "(972) 8675309",
            "15 Sunny Drive", "Dallas", "TX", 75079, 340, 425);
        private List<Host> hosts = new List<Host>();

        public HostRepoDouble()
        {
            hosts.Add(HOST);
        }

        public List<Host> FindByState(string stateAbbr)
        {
            return new List<Host>(hosts);
        }
    }
}
