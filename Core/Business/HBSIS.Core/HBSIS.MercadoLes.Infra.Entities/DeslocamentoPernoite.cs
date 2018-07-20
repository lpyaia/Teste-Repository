using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra.Entities
{
    public class DeslocamentoPernoite : Deslocamento
    {
        public override int IdOcorrenciaInicio => 10;
        public override int IdOcorrenciaFim => 11;
    }
}
