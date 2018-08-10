using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Exceptions;
using System;
using System.Collections.Generic;

namespace HBSIS.Framework.Commons.Data
{
    public static class FactoryProvider
    {
        private static readonly object _lock = new object();
        private static Dictionary<string, IFactory> _factories = new Dictionary<string, IFactory>();

        public static IFactory CurrentFactory
        {
            get
            {
                lock (_lock)
                {
                    var typeName = Configuration.Actual.GetDataFactoryTypeName();
                    return GetOrCreateFactory(typeName);
                }
            }
        }

        public static T GetFactory<T>()
             where T : class, IFactory
        {
            lock (_lock)
            {
                var typeName = typeof(T).AssemblyQualifiedName;
                return GetOrCreateFactory(typeName) as T;
            }
        }

        private static Dictionary<string, IFactory> Fatories
        {
            get
            {
                lock (_lock)
                {
                    return _factories;
                }
            }
        }

        #region GetFactory

        private static IFactory GetOrCreateFactory(string typeName)
        {
            typeName = typeName ?? "default";

            var factory = Fatories.ContainsKey(typeName) ? Fatories[typeName] : null;

            if (factory == null)
            {
                if (typeName != null)
                {
                    var type = Type.GetType(typeName);
                    factory = type != null ? Activator.CreateInstance(type) as IFactory : null;
                }

                if (factory == null)
                    throw new HBDataException("DataFactory not defined.");

                Fatories[typeName] = factory;
            }

            return factory;
        }
    }

    #endregion GetFactory
}