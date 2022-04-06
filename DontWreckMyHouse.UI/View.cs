using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (!option.IsHidden())
                {
                    io.PrintLine($"{i}. {option.ToLabel()}");
                }
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max - 1}]: ";
            return options[io.ReadInt(message, min, max)];
        }

        //Get something to identify or narrow down list of ___

        //Choose ___ if a list, only show a certain number before ask to refine search

        public Reservation MakeReservation()      //or Create
        {
            //Guest guest, Host host, DateTime startDate

            Reservation reservation = new Reservation();

            //guest id
            //host id

            //Show cuurent reservations at location
            reservation.StartDate = io.ReadDate("Start date [MM/dd/yyyy]: ");
            reservation.EndDate = io.ReadDate("End date [MM/dd/yyyy]: ");

            //display summary
            //check if ok
            //if not, do not create/make

            return reservation;
        }

        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }

        // display only
        public void DisplayHeader(string message)
        {
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
        }

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
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
                io.PrintLine(message);
            }
        }

        public void DisplayReservations(List<Reservation> reservations)
        {
            if (reservations == null || reservations.Count == 0)
            {
                io.PrintLine("No reservations found.");
                return;
            }

            foreach (Reservation reservation in reservations)
            {
                io.PrintLine(
                    string.Format("ID: {0}, {1} - {2}, Guest: {3}, {4}, {5} ",    //Value: ${4:0.00}
                        reservation.Id,
                        reservation.StartDate,
                        reservation.EndDate,
                        reservation.Guest.FirstName,
                        reservation.Guest.LastName,
                        reservation.Guest.Email));
            }
        }
    }
}
