using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Models;
using System.IO;

namespace DontWreckMyHouse.DAL
{
    public class GuestFileRepo : IGuestRepo
    {
        private readonly string filePath;

        public GuestFileRepo(string filePath)
        {
            this.filePath = filePath;
        }

        public Guest FindByPhone(string phone)        //string or int
        {
            throw new NotImplementedException();
        }

        //NOT ADDING SO NO NEED TO WRITE OR SERIALIZE
    }
}
