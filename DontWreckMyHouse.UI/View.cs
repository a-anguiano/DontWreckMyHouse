﻿using System;
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
                    io.PrintLine($"{i}. {option.ToLabel()}");
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max - 1}]: ";
            return options[io.ReadInt(message, min, max)];
        }

        public string GetHostState()
        {
            return io.ReadRequiredString("Enter state initials: "); //special consoleIO?
        }

        public string GetGuestPhone()
        {
            return io.ReadRequiredString("Enter guest phone number: "); //special consoleIO?
        }

        //Choose ___ if a list, only show a certain number before ask to refine search
        public Host ChooseHost(List<Host> hosts)
        {
            if (hosts == null || hosts.Count == 0)
            {
                io.PrintLine("No hosts found");
                return null;
            }

            int index = 1;
            foreach (Host host in hosts.Take(25))
            {
                io.PrintLine($"{index++}: City: {host.City} Standard Rate: {host.StandardRate} Weekend Rate: {host.WeekendRate}");
            }
            index--;

            if (hosts.Count > 25)
            {
                io.PrintLine("More than 25 hosts found. Showing first 25. Please refine your search.");
            }
            io.PrintLine("0: Exit");
            string message = $"Select a host by their index [0-{index}]: ";

            index = io.ReadInt(message, 0, index);
            if (index <= 0)
            {
                return null;
            }
            return hosts[index - 1];
        }

        public Reservation MakeReservation(Host host, Guest guest)      //or Create, params?
        {
            Reservation reservation = new Reservation();

            //Show current reservations at location
            //DisplayReservations();
            reservation.StartDate = io.ReadDate("Start date [MM/dd/yyyy]: ");
            reservation.EndDate = io.ReadDate("End date [MM/dd/yyyy]: ");

            DisplayHeader("Summary");
            //DisplaySummary();
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
                    string.Format("ID: {0}, {1} - {2}, Guest: {3}, {4}, {5} ",  
                        reservation.Id,
                        reservation.StartDate,
                        reservation.EndDate,
                        reservation.Guest.FirstName,
                        reservation.Guest.LastName,
                        reservation.Guest.Email));
            }
        }

        public void DisplaySummary (Reservation reservation)
        {
                io.PrintLine(
                    string.Format("Start: {0}\nEnd: {1}\nTotal: ${2:0.00}",
                        reservation.StartDate,
                        reservation.EndDate,
                        reservation.TotalCost));
        }
    }
}
