using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class DeslocamentoPernoite : Deslocamento
    {
        public override int IdOcorrenciaInicio => 10;
        public override int IdOcorrenciaFim => 11;
    }
}
