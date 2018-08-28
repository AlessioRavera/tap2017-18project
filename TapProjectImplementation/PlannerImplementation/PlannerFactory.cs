using TAP2017_2018_PlannerInterface;

namespace PlannerImplementation
{
    class PlannerFactory : IPlannerFactory
    {
        public IPlanner CreateNew()
        {
            return new Planner();
        }
    }
}
