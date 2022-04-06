using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public enum MainMenuOption
    {
        Exit,
        ViewReservationsForAHost,
        MakeAReservation,
        EditAReservation,
        CancelAReservation
        //Report1
        //Report2
        //Generate
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
            //MainMenuOption.ReportKgPerItem => "Report: Kilograms of Item",
            //MainMenuOption.ReportCategoryValue => "Report: Item Category Value",
            //MainMenuOption.Generate => "Generate Random Forages",
            _ => throw new NotImplementedException()
        };

        //public static bool IsHidden(this MainMenuOption option) => option switch
        //{
        //    MainMenuOption.Generate => true,
        //    _ => false
        //};
    }
}
