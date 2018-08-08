using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{
    class TravelCompanyBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IReadOnlyTravelCompany>().To<ReadOnlyTravelCompany>();
            Bind<ITravelCompany>().To<TravelCompany>();
            Bind<ITravelCompanyBroker>().To<TravelCompanyBroker>();
            Bind<ITravelCompanyBrokerFactory>().To<TravelCompanyBrokerFactory>();
            Bind<IReadOnlyTravelCompanyFactory>().To<ReadOnlyTravelCompanyFactory>();
            Bind<IReadOnlyTravelCompany>().To<ReadOnlyTravelCompany>();
        }
    }
}
