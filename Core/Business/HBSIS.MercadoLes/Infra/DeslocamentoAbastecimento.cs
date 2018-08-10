using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class DeslocamentoAbastecimento : Deslocamento
    {
        public override int IdOcorrenciaInicio => 17;
        public override int IdOcorrenciaFim => 18;
    }
}
