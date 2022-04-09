using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Interfaces;

namespace DontWreckMyHouse.BLL
{
    public class HostService
    {
        private readonly IHostRepo repository;

        public HostService(IHostRepo repository)
        {
            this.repository = repository;
        }

        public List<Host> FindByState(string stateAbbr)
        {
            return repository.FindByState(stateAbbr);
        }

        public Host FindByPhone(string phone)
        {
            return repository.FindByPhone(phone);
        }
    }
}
