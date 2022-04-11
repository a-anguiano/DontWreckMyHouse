using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core;

namespace DontWreckMyHouse.UI
{
    public class View
    {
        private readonly ConsoleIO io;

        public View(ConsoleIO io)
        {
            this.io = io;
        }

        public MainMenuOption SelectMainMenuOption()
        {
            DisplayHeader("Main Menu");
            int min = int.MaxValue;
            int max = int.MinValue;
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            for (int i = 0; i < options.Length; i++)
            {
                MainMenuOption option = options[i];
                    io.PrintLine($"{i}. {option.ToLabel()}");
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max}]: "; 
            return options[io.ReadInt(message, min, max)];
        }

        public string GetHostState()
        {
            return io.ReadRequiredString("Enter host state initials: "); //special consoleIO?
        }

        public string GetHostCity()
        {
            return io.ReadRequiredString("Enter host city: ");
        }

        public string GetHostPhone()
        {
            return io.ReadRequiredString("Enter host phone number: ");
        }

        public string GetGuestPhone()
        {
            return io.ReadRequiredString("Enter guest phone number: "); //special consoleIO?
        }

        public int GetReservationId()
        {
            return io.ReadInt("Reservation ID: ");
        }

        public string GetNewDate(string type, DateTime date)
        {
            var result = io.ReadString($"{type} ({date:d}): ");
            return result;
        }

        public Host ChooseHost(List<Host> hosts)
        {
            if (hosts == null || hosts.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                io.PrintLine("No hosts found");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }

            int index = 1;
            foreach (Host host in hosts.Take(25))
            {
                io.PrintLine($"ID: {index++}, City: {host.City}, Standard Rate: {host.StandardRate:c}, Weekend Rate: {host.WeekendRate:c}");
            }
            index--;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string message = $"Select a host by their index [1-{index}]: ";
            Console.ForegroundColor = ConsoleColor.White;

            index = io.ReadInt(message, 0, index);
            if (index <= 0)
            {
                return null;
            }
            return hosts[index - 1];
        }

        public Reservation MakeReservation(List<Reservation> reservations)     
        {
            Reservation reservation = new Reservation();
            
            DisplayReservations(reservations);
            reservation.StartDate = io.ReadDate("Start date [MM/dd/yyyy]: ");
            reservation.EndDate = io.ReadDate("End date [MM/dd/yyyy]: ");

            return reservation;
        }

        public Reservation MakeSummary(Reservation reservation)
        {
            DisplayHeader("Summary");
            DisplaySummary(reservation);
            bool response = io.ReadBool("Is this okay? [y/n]: ");  
            if (response)
            {
                return reservation;
            }
            else
            {
                return null;
            }
        }

        public void EnterToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            io.ReadString("Press [Enter] to continue.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // display only
        public void DisplayHeader(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayStatus(bool success, string message)
        {
            DisplayStatus(success, new List<string>() { message });
        }

        public void DisplayStatus(bool success, List<string> messages)
        {
            DisplayHeader(success ? "Success" : "Error");
            foreach (string message in messages)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                io.PrintLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void DisplayReservations(List<Reservation> reservations) 
        {
            if (reservations == null || reservations.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                io.PrintLine("No reservations found.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            foreach (Reservation reservation in reservations)
            {                                     
            io.PrintLine(string.Format("ID: {0}, {1} - {2}, Guest: {3}, {4}, Email: {5}",  
                        reservation.Id,
                        reservation.StartDate.ToString("d"),
                        reservation.EndDate.ToString("d"),
                        reservation.Guest.FirstName,
                        reservation.Guest.LastName,
                        reservation.Guest.Email));
            }
        }

        public void DisplaySummary (Reservation reservation)
        {
                io.PrintLine(
                    string.Format("Start: {0:d}\nEnd: {1:d}\nTotal: ${2:0.00}",
                        reservation.StartDate,
                        reservation.EndDate,
                        reservation.TotalCost));
        }

        public void DisplayInYellow(string type)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            io.PrintLine($"{type} not found.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
