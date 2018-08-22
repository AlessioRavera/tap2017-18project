using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;

namespace MoreTravelCompanyTest
{
    [TestFixture]
    public class PrivateReadOnlyTravelCompanyTestSuite : TravelCompanyTestInitializer
    {
        protected string A, B, C, D, E, F, unreachable;
        protected ILegDTO ae1, ae2, ae3, ab, bf;
        protected int aeIndex, abIndex, bfIndex;

        [SetUp]
        public void InitializePaths()
        {

            A = "A";
            B = "B";
            C = "C";
            D = "D";
            E = "E";
            F = "G";
            unreachable = "UNREACHABLE";
            travelCompany = travelCompanyFactory.Get(ExampleName);
            aeIndex = travelCompany.CreateLeg(A, E, 10, 45, TransportType.Train);
            ae1 = travelCompany.GetLegDTOFromId(aeIndex);
            aeIndex = travelCompany.CreateLeg(A, F, 10, 45, TransportType.Plane);
            ae2 = travelCompany.GetLegDTOFromId(aeIndex);
            aeIndex = travelCompany.CreateLeg(A, C, 10, 45, TransportType.Bus);
            ae3 = travelCompany.GetLegDTOFromId(aeIndex);
            abIndex = travelCompany.CreateLeg(A, B, 2, 4, TransportType.Train);
            ab = travelCompany.GetLegDTOFromId(abIndex);
            bfIndex = travelCompany.CreateLeg(B, F, 5, 20, TransportType.Train);
            bf = travelCompany.GetLegDTOFromId(bfIndex);
            travelCompany.CreateLeg(C, D, 1, 4, TransportType.Train);
            travelCompany.CreateLeg(D, F, 2, 8, TransportType.Train);
            travelCompany.CreateLeg(D, A, 1, 4, TransportType.Ship);
        }


        [Test]
        public void FindLegsReadOnlyTravelCompanyReturnsOk_Plane_Train()
        {
            var result =readOnlyTravelCompany.FindLegs(l => l.From.Equals(A) && ( l.Type.Equals(TransportType.Train) || l.Type.Equals(TransportType.Plane)));
            Assert.Contains(ae1, result);
            Assert.Contains(ae2, result);
            Assert.Contains(ab, result);
            Assert.That(!result.Contains(bf));
            Assert.That(!result.Contains(ae3));
            Assert.AreEqual(3,result.Count);
        }

        [Test]
        public void FindLegsReadOnlyTravelCompanyReturnsOk()
        {
            var result = readOnlyTravelCompany.FindLegs(l => !l.Type.Equals(TransportType.Train) && !l.Type.Equals(TransportType.Plane) && !l.Type.Equals(TransportType.Ship));
            Assert.Contains(ae3, result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void FindLegsReadOnlyTravelCompanyReturnsOk_NoTrip()
        {
            var result = readOnlyTravelCompany.FindLegs(l => l.From.Equals(E));
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void FindDeparturesReadOnlyTravelCompanyReturnsOk_NoTrip()
        {
            var resultFindDepartures = readOnlyTravelCompany.FindDepartures(E, TransportType.Ship);
            Assert.AreEqual(0, resultFindDepartures.Count);
        }

        [Test]
        public void FindLegs_FindDepartures_ReadOnlyTravelCompanyReturnsOk_Bus_Plane_SameLeg()
        {
            var resultFindLegs = readOnlyTravelCompany.FindLegs(l => l.From.Equals(A) && (l.Type.Equals(TransportType.Bus) || l.Type.Equals(TransportType.Plane)));
            var resultFindDepartures = readOnlyTravelCompany.FindDepartures(A, TransportType.Bus | TransportType.Plane);

            foreach (var leg in resultFindDepartures)
            {
                Assert.Contains(leg, resultFindLegs);
            }
            
            Assert.Contains(ae2, resultFindDepartures);
            Assert.Contains(ae3, resultFindDepartures);

            Assert.AreEqual(2, resultFindLegs.Count);
            Assert.AreEqual(2, resultFindDepartures.Count);
        }
    }
}
