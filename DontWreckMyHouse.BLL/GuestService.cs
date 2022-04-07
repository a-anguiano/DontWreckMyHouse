using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL
{
    public class GuestService
    {
        private readonly IGuestRepo repository;

        public GuestService(IGuestRepo repository)
        {
            this.repository = repository;
        }
        public Guest FindByPhone(string phone)        //string or int
        {
            return repository.FindByPhone(phone);
        }
    }
}
