using HBSIS.Framework.Bus.Bus;

namespace HBSIS.Framework.Bus.Mock
{
    public class MockBusFactory : Bus.BusFactory
    {
        public override IBusContext CreateContext()
        {
            return new MockBusContext();
        }
    }
}