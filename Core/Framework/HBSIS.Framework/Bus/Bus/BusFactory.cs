using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Exceptions;
using System;

namespace HBSIS.Framework.Bus.Bus
{
    public abstract class BusFactory
    {
        private static readonly object _lock = new object();
        private static BusFactory _factory;

        public abstract IBusContext CreateContext();

        public static BusFactory Current
        {
            get
            {
                lock (_lock)
                {
                    if (_factory == null)
                    {
                        var typeName = Configuration.Actual.GetBusFactoryTypeName();

                        if (typeName != null)
                        {
                            var type = Type.GetType(typeName);

                            if (type != null)
                            {
                                _factory = Activator.CreateInstance(type) as BusFactory;
                            }
                        }

                        if (_factory == null)
                        {
                            throw new HBBusException("BusFactory not defined.");
                        }
                    }

                    return _factory;
                }
            }
        }

        public static IBusContext CreateBusContext()
        {
            var bus = Current.CreateContext();
            bus.Connect();

            return bus;
        }

        public static IBusContext TryCreateBusContext()
        {
            try
            {
                var bus = Current.CreateContext();
                bus.Connect();

                return bus;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}