using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Mongo;
using HBSIS.MercadoLes.Infra.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Services.Persistence.Repository
{
    public class EnvioXmlRepository : MongoRepository<EnvioXml, Guid>
    {
        public EnvioXmlRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public List<EnvioXml> GetByReenvio()
        {
            var filtro = Builders<EnvioXml>.Filter.Eq("Reenviar", true);
            return Collection.Find(filtro).ToList();
        }
    }
}
