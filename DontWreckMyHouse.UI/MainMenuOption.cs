namespace DontWreckMyHouse.UI
{
    public enum MainMenuOption
    {
        Exit,
        ViewReservationsForAHost,
        MakeAReservation,
        EditAReservation,
        CancelAReservation
    }

    public static class MainMenuOptionExtensions
    {
        public static string ToLabel(this MainMenuOption option) => option switch
        {
            MainMenuOption.Exit => "Exit",
            MainMenuOption.ViewReservationsForAHost => "View Reservations For A Host",
            MainMenuOption.MakeAReservation => "Make A Reservation",
            MainMenuOption.EditAReservation => "Edit A Reservation",
            MainMenuOption.CancelAReservation => "Cancel A Reservation",
            _ => throw new NotImplementedException()
        };
    }
}
