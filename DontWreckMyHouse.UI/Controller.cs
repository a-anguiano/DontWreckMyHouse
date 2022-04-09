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
                        ViewByHost();
                        break;
                    case MainMenuOption.MakeAReservation:
                        MakeAReservation();
                        break;
                    case MainMenuOption.EditAReservation:
                        EditAReservation();
                        break;
                    case MainMenuOption.CancelAReservation:
                        CancelAReservation();
                        break;
                }
            } while (option != MainMenuOption.Exit);
        }

        private void ViewByHost()
        {
            Host host = GetHosts();
            List<Reservation> reservations = reservationService.FindByHost(host);   //how to id
            view.DisplayReservations(reservations);
            view.EnterToContinue();
        }

        private void MakeAReservation()
        {
            view.DisplayHeader(MainMenuOption.MakeAReservation.ToLabel());
            Host host = GetHosts();
            if (host == null)
            {
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                return;
            }

            List<Reservation> reservations = reservationService.FindByHost(host);
            Reservation reservation = view.MakeReservation(reservations);
            reservation.Host = host;
            reservation.Guest = guest;


            decimal total = reservationService.CalculateTotal(reservation);
            reservation.TotalCost = total;
            reservation = view.MakeSummary(reservation);
            //ok y or n

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

        private void EditAReservation()
        {
            Reservation res = new Reservation();
            view.DisplayHeader(MainMenuOption.EditAReservation.ToLabel());
            Host host = GetHost();
            Guest guest = GetGuest();
            Console.WriteLine($"Guest Email: {guest.Email}");
            Console.WriteLine($"Host Email: {host.Email}\n");

            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            List<Reservation> reservations = reservationService.FindReservationsForHostAndGuest(host, guest);
            view.DisplayReservations(reservations);

            int id = view.GetReservationId();
            res.Id = id;
            Reservation reservation = reservationService.GetReservationById(res);

            view.DisplayHeader($"Editing Reservation {id}");
            var newStart = view.GetNewDate("Start", reservation.StartDate);
            var newEnd = view.GetNewDate("End", reservation.EndDate);

            if(newStart != null)
            {
                reservation.StartDate = newStart;
            }
            if(newEnd != null)
            {
                reservation.EndDate = newEnd;
            }
            Reservation editedRes = view.MakeSummary(reservation);
            //ok y or n

            Result<Reservation> result = reservationService.Edit(editedRes);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Reservation {result.Value.Id} updated.";
                view.DisplayStatus(true, successMessage);
            }
        }

        private void CancelAReservation()
        {
            Reservation res = new Reservation();
            view.DisplayHeader(MainMenuOption.CancelAReservation.ToLabel());
            Host host = GetHost();
            Guest guest = GetGuest();
            Console.WriteLine($"Guest Email: {guest.Email}");
            Console.WriteLine($"Host Email: {host.Email}\n");

            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            List<Reservation> reservations = reservationService.FindReservationsForHostAndGuest(host, guest);
            view.DisplayReservations(reservations);

            int id = view.GetReservationId();
            res.Id = id;
            Reservation reservation = reservationService.GetReservationById(res);

            Result<Reservation> result = reservationService.Cancel(reservation);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Reservation {result.Value.Id} cancelled.";
                view.DisplayStatus(true, successMessage);
            }
        }

        private Host GetHosts()
        {
            string stateAbbr = view.GetHostState();
            List<Host> hosts = hostService.FindByState(stateAbbr);
            return view.ChooseHost(hosts);
        }

        private Guest GetGuest()
        {
            string phoneNum = view.GetGuestPhone();
            return guestService.FindByPhone(phoneNum);
        }

        private Host GetHost()
        {
            string phone = view.GetHostPhone();
            return hostService.FindByPhone(phone);
        }
    }
}
