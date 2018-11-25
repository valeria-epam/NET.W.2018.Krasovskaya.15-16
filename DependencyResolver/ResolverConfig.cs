using System.IO;
using System.Reflection;
using BLL.Interface.Interfaces;
using BLL.ServiceImplementation;
using DAL.Fake.Repositories;
using DAL.Interface.Interfaces;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            var appFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var storageFolder = Path.Combine(appFolder, "Storage");
            Directory.CreateDirectory(storageFolder);
            var path = Path.Combine(storageFolder, "BankAccountStorage");

            kernel.Bind<IAccountStorage>().To<AccountStorage>().WithConstructorArgument(path);
            kernel.Bind<IAccountManager>().To<AccountManager>();
            kernel.Bind<ICalculateBonus>().To<CalculateBonus>().InSingletonScope();
            kernel.Bind<IGenerateNumber>().To<GenerateNumber>().InSingletonScope();
        }
    }
}
