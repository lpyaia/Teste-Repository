using HBSIS.Framework.Data.Mongo;
using HBSIS.MercadoLes.Integracao.SapBrf.IntegracaoCutoffWebService;

namespace HBSIS.MercadoLes.Integracao.SapBrf
{
    public class IntegracaoMongoMap : MongoMap
    {
        public override void Map()
        {
            base.Map();

            RegisterIfNot<SI_RET_TRANSPORTERequest>();
            RegisterIfNot<SI_RET_TRANSPORTEResponse>();
        }
    }
}