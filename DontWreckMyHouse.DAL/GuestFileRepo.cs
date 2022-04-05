using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL
{
    public class GuestFileRepo : IGuestRepo
    {
        public Guest FindByPhone(string phone)        //string or int
        {
            throw new NotImplementedException();
        }
    }
}
