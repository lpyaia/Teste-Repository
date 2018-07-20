using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.MercadoLes.Services.Commons.Integration.Config
{
    public class IntegrationConfigCollection<T> : IntegrationConfigCollection, IEnumerable<T>
        where T : IIntegrationConfig
    {
        protected List<T> List { get; } = new List<T>();

        public T this[int index]
        {
            get
            {
                return List.ElementAt(index);
            }
        }

        public T this[string name]
        {
            get
            {
                return List.FirstOrDefault(x => x.Name.ToUpper() == name?.ToUpper());
            }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            List.Add(item);
        }
    }
}