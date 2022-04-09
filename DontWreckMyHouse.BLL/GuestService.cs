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
        public Guest FindByPhone(string phone)
        {
            return repository.FindByPhone(phone);
        }
    }
}
