using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace DontWreckMyHouse.UI
{
    internal class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }

        public static void Configure()
        {
            Kernel = new StandardKernel();

            //Kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            //Kernel.Bind<View>().To<View>();

            //string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //string forageFileDirectory = Path.Combine(projectDirectory, "data", "forage_data");
            //string foragerFilePath = Path.Combine(projectDirectory, "data", "foragers.csv");
            //string itemFilePath = Path.Combine(projectDirectory, "data", "items.txt");

            //Kernel.Bind<IForageRepository>().To<ForageFileRepository>().WithConstructorArgument(forageFileDirectory);
            //Kernel.Bind<IForagerRepository>().To<ForagerFileRepository>().WithConstructorArgument(foragerFilePath);
            //Kernel.Bind<IItemRepository>().To<ItemFileRepository>().WithConstructorArgument(itemFilePath);

            //Kernel.Bind<ForagerService>().To<ForagerService>();
            //Kernel.Bind<ForageService>().To<ForageService>();
            //Kernel.Bind<ItemService>().To<ItemService>();
        }
    }
}
