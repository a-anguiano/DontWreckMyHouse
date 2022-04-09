using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Exceptions;

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
            var hosts = new List<Host>();

            if (!File.Exists(filePath))
            {
                return hosts;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines("hosts.csv");
            }
            catch (IOException ex)
            {
                throw new RepoExceptions("Could not read hosts.", ex);
            }

            for (int i = 1; i < lines.Length; i++) 
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Host host = Deserialize(fields);
                if (host != null && host.State == stateAbbr)
                {
                    hosts.Add(host);
                }
            }
            return hosts;
        }

        public Host FindByPhone(string phone)
        {
            var hostCheck = new Host();

            if (!File.Exists(filePath))
            {
                return hostCheck;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines("hosts.csv");
            }
            catch (IOException ex)
            {
                throw new RepoExceptions("Could not read hosts.", ex);
            }

            for (int i = 1; i < lines.Length; i++) 
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Host host = Deserialize(fields);
                if (host != null && host.Phone == phone)
                {
                    return host;
                }
            }
            return null;
        }

        private Host Deserialize(string[] fields)
        {
            if (fields.Length != 10)
            {
                return null;
            }

            Host result = new Host();
            result.Id = fields[0];      
            result.LastName = fields[1];
            result.Email = fields[2];
            result.Phone = fields[3];
            result.Address = fields[4];
            result.City = fields[5];
            result.State = fields[6];
            result.Zip = int.Parse(fields[7]);
            result.StandardRate = decimal.Parse(fields[8]);
            result.WeekendRate = decimal.Parse(fields[9]);
            return result;
        }
    }
}
