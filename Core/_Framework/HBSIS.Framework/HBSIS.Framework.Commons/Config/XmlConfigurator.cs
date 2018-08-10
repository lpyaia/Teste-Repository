using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Context;
using HBSIS.Framework.Commons.Helper;
using System;
using System.Xml.Linq;

namespace HBSIS.Framework.Commons.Config
{
    public abstract class XmlConfigurator<TModel> : Configurator<TModel>
          where TModel : class
    {
        private const string ConfiguratorKey = "FWK_CONFIGURATOR";

        public XmlConfigurator(string key)
            : this(key, null)
        {
        }

        public XmlConfigurator(string key, string fileName)
        {
            Key = key;
            KeyName = $"{ConfiguratorKey}_{key}";
            FileName = fileName ?? GetPathName();
        }

        protected string Key { get; }
        protected string KeyName { get; }
        protected string FileName { get; }

        protected TModel Current
        {
            get
            {
                return ApplicationContext.Current[KeyName] as TModel;
            }
            private set { ApplicationContext.Current[KeyName] = value; }
        }

        public sealed override TModel GetCurrent()
        {
            var value = Current;

            if (value != null) return value;

            var document = ReadDocument(FileName);

            if (document != null)
            {
                value = CreateModel(document);
                Current = value;
            }

            return value;
        }

        protected abstract TModel CreateModel(XDocument document);

        private XDocument ReadDocument(string fileName)
        {
            try
            {
                return XDocument.Load(fileName);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex);
                return null;
            }
        }
    }
}