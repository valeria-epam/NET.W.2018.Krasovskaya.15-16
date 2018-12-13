using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BLL.Interface.Interfaces;
using BLL.ServiceImplementation;
using DAL;
using DAL.Interface.Interfaces;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;

namespace WEB
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ApplicationContext>().ToSelf().InRequestScope();
            kernel.Bind<IAccountStorage>().To<AccountStorage>().InRequestScope();
            kernel.Bind<IAccountManager>().To<AccountManager>().InRequestScope();
            kernel.Bind<INotificationService>().To<NotificationService>().InRequestScope();
            kernel.Bind<IAccountTypeManager>().To<AccountTypeManager>().InRequestScope();
            kernel.Bind<ICalculateBonus>().To<CalculateBonus>().InSingletonScope();
            kernel.Bind<IGenerateNumber>().To<GenerateNumber>().InSingletonScope();

            return kernel;
        }
    }
}
