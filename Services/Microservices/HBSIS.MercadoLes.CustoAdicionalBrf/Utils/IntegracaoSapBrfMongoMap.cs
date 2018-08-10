using HBSIS.Framework.Data.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Utils
{
    public class IntegracaoSapBrfMongoMap : MongoMap
    {
        public override void Map()
        {
            base.Map();

            RegisterIfNot<SI_CUSTO_ADICIONAL_FRETE_OUTService.SI_CUSTO_ADICIONAL_FRETE_OUTRequest>();
        }
    }
}
