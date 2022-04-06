using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Exceptions;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.UI
{
    public class Controller
    {
        private readonly ReservationService reservationService;
        private readonly GuestService guestService;
        private readonly HostService hostService;
        private readonly View view;

        public Controller(ReservationService reservationService, GuestService guestService, HostService hostService, View view)
        {
            this.reservationService = reservationService;
            this.guestService = guestService;
            this.hostService = hostService;
            this.view = view;
        }

        public void Run()
        {
            view.DisplayHeader("Welcome to Sustainable Foraging");
            try
            {
                RunAppLoop();
            }
            catch (RepoExceptions ex)
            {
                view.DisplayException(ex);
            }
            view.DisplayHeader("Goodbye.");
        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch (option)
                {
                    case MainMenuOption.ViewReservationsForAHost:
                        //view.DisplayStatus(false, "NOT IMPLEMENTED");
                        //view.EnterToContinue();
                        ViewByHost();
                        break;
                    case MainMenuOption.MakeAReservation:
                        view.DisplayStatus(false, "NOT IMPLEMENTED");
                        view.EnterToContinue();
                        //MakeAReservation();
                        break;
                    case MainMenuOption.EditAReservation:
                        view.DisplayStatus(false, "NOT IMPLEMENTED");
                        view.EnterToContinue();
                        //EditAReservation();
                        break;
                    case MainMenuOption.CancelAReservation:
                        view.DisplayStatus(false, "NOT IMPLEMENTED");
                        view.EnterToContinue();
                        //CancelAReservation();
                        break;
                    //case MainMenuOption.Report1:
                    //    view.DisplayStatus(false, "NOT IMPLEMENTED");
                    //    view.EnterToContinue();
                    //    break;
                    //case MainMenuOption.Report2:
                    //    view.DisplayStatus(false, "NOT IMPLEMENTED");
                    //    view.EnterToContinue();
                    //    break;
                    //case MainMenuOption.Generate:
                    //    Generate();
                    //    break;
                }
            } while (option != MainMenuOption.Exit);
        }

        private void ViewByHost()
        {
            Host host = GetHost();
            List<Reservation> reservations = reservationService.FindByHost(host);   //how to id
            view.DisplayReservations(reservations);
            view.EnterToContinue();
        }

        private void MakeAReservation()
        {
            view.DisplayHeader(MainMenuOption.MakeAReservation.ToLabel());
            Host host = GetHost();
            if (host == null)
            {
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                return;
            }
            Reservation reservation = view.MakeReservation(host, guest);
            Result<Reservation> result = reservationService.Create(reservation);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Reservation {result.Value.Id} created.";
                view.DisplayStatus(true, successMessage);
            }
        }

        //private void Generate()
        //{
        //    ReservationGenerationRequest request = view.GetReservationGenerationRequest();
        //    if (request != null)
        //    {
        //        int count = reservationService.Generate(request.Start, request.End, request.Count);
        //        view.DisplayStatus(true, $"{count} forages generated.");
        //    }
        //}
        // support methods

        //Get Guest

        private Host GetHost()
        {
            string stateAbbr = view.GetHostState();
            List<Host> hosts = hostService.FindByState(stateAbbr);
            return view.ChooseHost(hosts);
        }
    }

}
