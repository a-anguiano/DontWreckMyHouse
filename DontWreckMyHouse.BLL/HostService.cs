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
            return repository.FindByState(stateAbbr)
                .OrderBy(h => h.City)
                .ThenBy(h => h.StandardRate).ToList();
        }

        public List<Host> FindByCity(string stateAbbr, string city)
        {
            var hostsByState = repository.FindByState(stateAbbr);
            var ret = hostsByState.Where(h => h.City == city)
                .OrderBy(h => h.StandardRate).ToList();
            return ret;
        }

        public Host FindByPhone(string phone)
        {
            return repository.FindByPhone(phone);
        }
    }
}
