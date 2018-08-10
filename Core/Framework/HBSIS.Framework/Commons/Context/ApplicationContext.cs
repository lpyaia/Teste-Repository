using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Exceptions;
using System;

namespace HBSIS.Framework.Commons.Context
{
    public sealed class ApplicationContext
    {
        private static readonly object _lock = new object();

        private static IApplicationContext current;

        public ApplicationContext()
        {
        }

        public static IApplicationContext Current
        {
            get
            {
                lock (_lock)
                {
                    if (current == null)
                    {
                        var typeName = Configuration.Actual.GetContextPersisterTypeName();

                        if (typeName != null)
                        {
                            var type = Type.GetType(typeName);

                            if (type == null)
                                throw new HBException("ApplicationContextType not defined.");

                            current = (IApplicationContext)Activator.CreateInstance(type);
                        }
                        else
                        {
                            current = new ThreadContext();
                        }
                    }

                    if (current == null)
                    {
                        throw new HBException("ApplicationContext not defined.");
                    }

                    return current;
                }
            }
        }
    }
}