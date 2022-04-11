using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core;
using DontWreckMyHouse.Core.Exceptions;
using DontWreckMyHouse.Core.Models;
using System.Linq;

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
            view.DisplayHeader(MainMenuOption.ViewReservationsForAHost.ToLabel());
            Host host = GetHosts();
            if (host == null)
            {
                view.DisplayInYellow("Host");
                return;
            }
            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            List<Reservation> reservations = reservationService.FindByHost(host);
            List<Reservation> orderedRes = GetGuestPropertyAndOrderReservationsByStartDate(reservations);

            view.DisplayReservations(orderedRes);
            view.EnterToContinue();
        }

        private void MakeAReservation()
        {
            view.DisplayHeader(MainMenuOption.MakeAReservation.ToLabel());
            Host host = GetHosts();
            if (host == null)
            {
                view.DisplayInYellow("Host");
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                view.DisplayInYellow("Guest");
                return;
            }

            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            List<Reservation> reservations = reservationService.FindByHost(host);
            List<Reservation> orderedRes = GetGuestPropertyAndOrderReservationsByStartDate(reservations);
            List<Reservation> futureOrderedRes = orderedRes.Where(r => r.StartDate >= DateTime.Today).ToList();

            Reservation reservation = view.MakeReservation(futureOrderedRes);
            reservation.Host = host;
            reservation.Guest = guest;

            decimal total = reservationService.CalculateTotal(reservation);
            reservation.TotalCost = total;
            reservation = view.MakeSummary(reservation);

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
            if (host == null)
            {
                view.DisplayInYellow("Host");
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                view.DisplayInYellow("Guest");
                return;
            }
            Console.WriteLine($"Guest Email: {guest.Email}");
            Console.WriteLine($"Host Email: {host.Email}\n");

            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            List<Reservation> reservations = reservationService.FindReservationsForHostAndGuest(host, guest);

            List<Reservation> orderedRes = GetGuestPropertyAndOrderReservationsByStartDate(reservations);
            view.DisplayReservations(orderedRes);

            int id = view.GetReservationId();
            res.Id = id;
            res.Host = host;
            Reservation reservation = reservationService.GetReservationById(res);

            view.DisplayHeader($"Editing Reservation {id}");
            var newStart = view.GetNewDate("Start", reservation.StartDate);
            var newEnd = view.GetNewDate("End", reservation.EndDate);

            if (!String.IsNullOrEmpty(newStart))
            {
                reservation.StartDate = DateTime.Parse(newStart);
            }
            else
            {
                reservation.StartDate = reservation.StartDate;
            }
            if (!String.IsNullOrEmpty(newEnd))
            {
                reservation.EndDate = DateTime.Parse(newEnd);
            }
            else
            {
                reservation.EndDate = reservation.EndDate;
            }
            Reservation editedRes = view.MakeSummary(reservation);

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
            if (host == null)
            {
                view.DisplayInYellow("Host");
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                view.DisplayInYellow("Guest");
                return;
            }
            Console.WriteLine($"Guest Email: {guest.Email}");
            Console.WriteLine($"Host Email: {host.Email}\n");

            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            List<Reservation> reservations = reservationService.FindReservationsForHostAndGuest(host, guest);

            List<Reservation> orderedRes = GetGuestPropertyAndOrderReservationsByStartDate(reservations);
            List<Reservation> futureOrderedRes = orderedRes.Where(r => r.StartDate >= DateTime.Today).ToList();
            view.DisplayReservations(futureOrderedRes);

            int id = view.GetReservationId();
            res.Id = id;
            res.Host = host;
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
            if (hosts.Count > 25)
            {
                string city = view.GetHostCity();
                List<Host> hostsRefined = hostService.FindByCity(stateAbbr, city);
                return view.ChooseHost(hostsRefined);
            }
            else
            {
                return view.ChooseHost(hosts);
            }
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

        private List<Reservation> GetGuestPropertyAndOrderReservationsByStartDate(List<Reservation> reservations)
        {
            List<Guest> guests = guestService.FindById(reservations); 
            
            for(int i = 0; i< guests.Count; i++)
            {
                reservations[i].Guest = guests[i];
            }
            return reservations.OrderBy(r => r.StartDate).ToList();
        }
    }
}
