using HBSIS.Framework.Commons.Data;
using HBSIS.Framework.Data;
using HBSIS.Framework.Data.Mongo;
using HBSIS.GE.FileImporter.Infra.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Services.Persistence.Repository
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
