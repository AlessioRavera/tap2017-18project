using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TravelCompanyImplementation;

namespace MoreTravelCompanyTest
{
    [TestFixture()]
    public class TravelCompanyTestDataBase
    {
        private readonly string _stringaConnessione = "DbTestTravelCompany";

        private LegDB CreazioneLeg(string partenza, string destinazione, int costo, int distanza, TransportType mezzo)
        {
            return new LegDB()
            {
                To = destinazione,
                From = partenza,
                Cost = costo,
                Distance = distanza,
                TransportT = mezzo
            };
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                var database = context.Database;
                database.Create();
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                var database = context.Database;
                database.Delete();
            }
        }

        [Test]
        [TestCase("Genova", "Milano", 5, 100, TransportType.Bus)]
        [TestCase("Genova", "Milano", 5, 100, TransportType.Plane)]
        [TestCase("Genova", "Milano", 5, 10, TransportType.Bus)]
        [TestCase("Genova", "Milano", 50, 100, TransportType.Bus)]
        [TestCase("Genova", "Savona", 5, 100, TransportType.Bus)]
        [TestCase("Savona", "Milano", 5, 100, TransportType.Bus)]
        public void TestConstrainUnique_OK(string partenza, string destinazione, int costo, int distanza, TransportType mezzo)
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(partenza, destinazione, costo, distanza, mezzo));
                context.SaveChanges();
            }
        }

        [Test]
        public void TestContrainUnique_Error()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(context.legs.Add(CreazioneLeg("Genova", "Savona", 5, 100, TransportType.Bus)));
                context.legs.Add(context.legs.Add(CreazioneLeg("Genova", "Savona", 5, 100, TransportType.Bus)));
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbUpdateException>());
            }
        }

        [Test]
        public void TestLunghezzaNomeCittaPartenzaTroppoCorto_Error()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(new string('a', DomainConstraints.NameMinLength - 1), "Milano", 5, 10, TransportType.Bus));
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }

        [Test]
        public void TestLunghezzaMinimaNomeCittaPartenza_OK()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(new string('a', DomainConstraints.NameMinLength), "Milano", 5, 10, TransportType.Bus));
                context.SaveChanges();
            }
        }

        [Test]
        public void TestLunghezzaNomeCittaDestinazioneTroppoCorto_Error()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg("Milano", new string('a', DomainConstraints.NameMinLength - 1), 5, 10, TransportType.Bus));
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }

        [Test]
        public void TestLunghezzaMinimaNomeCittaDestinazione_OK()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg("Milano", new string('a', DomainConstraints.NameMinLength), 5, 10, TransportType.Bus));
                context.SaveChanges();
            }
        }


        [Test]
        public void TestLunghezzaNomeCittaPartenzaTroppoLungo_Error()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(new string('a', DomainConstraints.NameMaxLength + 1), "Milano", 5, 10, TransportType.Bus));
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }

        [Test]
        public void TestLunghezzaMassimaNomeCittaPartenza_OK()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(new string('a', DomainConstraints.NameMaxLength), "Milano", 5, 10, TransportType.Bus));
                context.SaveChanges();
            }
        }

        [Test]
        public void TestLunghezzaNomeCittaDestinazioneTroppoLungo_Error()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg("Milano", new string('a', DomainConstraints.NameMaxLength + 1), 5, 10, TransportType.Bus));
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }

        [Test]
        public void TestLunghezzaMassimaNomeCittaDesitinazione_OK()
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg("Milano", new string('a', DomainConstraints.NameMaxLength), 5, 10, TransportType.Bus));
                context.SaveChanges();
            }
        }

        [Test]
        [TestCase("Genova", "Milano", -1, 100, TransportType.Bus)]
        [TestCase("Genova", "Milano", 0, 100, TransportType.Bus)]
        public void TestCostoStrettamentePositivo_Error(string partenza, string destinazione, int costo, int distanza,
            TransportType mezzo)
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(partenza, destinazione, costo, distanza, mezzo));
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }

        [Test]
        [TestCase("Genova", "Milano", 1, 100, TransportType.Bus)]
        [TestCase("Genova", "Milano", int.MaxValue, 100, TransportType.Bus)]
        public void TestCostoStrettamentePositivo_OK(string partenza, string destinazione, int costo, int distanza,
            TransportType mezzo)
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(partenza, destinazione, costo, distanza, mezzo));
                context.SaveChanges();
            }
        }

        [Test]
        [TestCase("Genova", "Milano", 5, -1, TransportType.Bus)]
        [TestCase("Genova", "Milano", 5, 0, TransportType.Bus)]
        public void TestDistanzaStrettamentePositivo_Error(string partenza, string destinazione, int costo, int distanza,
            TransportType mezzo)
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(partenza, destinazione, costo, distanza, mezzo));
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }

        [Test]
        [TestCase("Genova", "Milano", 5, 1, TransportType.Bus)]
        [TestCase("Genova", "Milano", 5, int.MaxValue, TransportType.Bus)]
        public void TestDistanzaStrettamentePositivo_OK(string partenza, string destinazione, int costo, int distanza,
            TransportType mezzo)
        {
            using (var context = new TravelCompanyContext(_stringaConnessione))
            {
                context.legs.Add(CreazioneLeg(partenza, destinazione, costo, distanza, mezzo));
                context.SaveChanges();
            }
        }
    }
}
