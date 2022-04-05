using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.DAL;
using DontWreckMyHouse.Core.Interfaces;
using Ninject;

namespace DontWreckMyHouse.UI
{
    internal class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }

        public static void Configure()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            Kernel.Bind<View>().To<View>();

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationFileDirectory = Path.Combine(projectDirectory, "Data", "reservations");
            string guestFilePath = Path.Combine(projectDirectory, "Data", "guests.csv");
            string hostFilePath = Path.Combine(projectDirectory, "Data", "hosts.csv");

            Kernel.Bind<IReservationRepo>().To<ReservationFileRepo>().WithConstructorArgument(reservationFileDirectory);
            Kernel.Bind<IHostRepo>().To<HostFileRepo>().WithConstructorArgument(hostFilePath);
            Kernel.Bind<IGuestRepo>().To<GuestFileRepo>().WithConstructorArgument(guestFilePath);

            Kernel.Bind<ReservationService>().To<ReservationService>();
            Kernel.Bind<GuestService>().To<GuestService>();
            Kernel.Bind<HostService>().To<HostService>();
        }
    }
}
