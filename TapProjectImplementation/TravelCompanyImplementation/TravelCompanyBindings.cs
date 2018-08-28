using System.Security;
using Ninject.Modules;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{

    [SecurityCritical]
    public class TravelCompanyBindings : NinjectModule
    {
        [SecurityCritical]
        public override void Load()
        {
            Bind<ITravelCompanyBrokerFactory>().To<TravelCompanyBrokerFactory>();
        }
    }
}
