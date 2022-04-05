using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;
using System.IO;

namespace DontWreckMyHouse.DAL
{
    public class HostFileRepo : IHostRepo
    {
        private readonly string filePath;

        public HostFileRepo(string filePath)
        {
            this.filePath = filePath;
        }
        public List<Host> FindByState(string stateAbbr)
        {
            throw new NotImplementedException();
        }

        //Deserialize
        //NOT ADDING SO NO NEED TO WRITE OR SERIALIZE
    }
}
