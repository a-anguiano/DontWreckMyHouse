using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.DAL
{
    public class HostFileRepo : IHostRepo
    {
        public List<Host> FindByState(string stateAbbr)
        {
            throw new NotImplementedException();
        }
    }
}
