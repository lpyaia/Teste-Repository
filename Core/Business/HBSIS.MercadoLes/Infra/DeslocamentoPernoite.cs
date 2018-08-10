using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class DeslocamentoPernoite : Deslocamento
    {
        public override int IdOcorrenciaInicio => 10;
        public override int IdOcorrenciaFim => 11;
    }
}
