using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
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
