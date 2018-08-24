using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using TAP2017_2018_PlannerInterface;

namespace PlannerImplementation
{
    [SecurityCritical]
    public class PlannerBinding : NinjectModule
    {
        [SecurityCritical]
        public override void Load()
        {
            this.Bind<IPlannerFactory>().To<PlannerFactory>();
        }
    }
}
