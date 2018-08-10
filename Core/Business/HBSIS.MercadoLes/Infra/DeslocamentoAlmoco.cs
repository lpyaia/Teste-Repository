using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace HBSIS.MercadoLes.Infra
{
    [Serializable]
    public class DeslocamentoAlmoco : Deslocamento
    {
        public override int IdOcorrenciaInicio => 5;
        public override int IdOcorrenciaFim => 6;
    }
}
