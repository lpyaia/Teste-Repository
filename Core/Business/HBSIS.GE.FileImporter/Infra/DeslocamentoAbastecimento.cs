using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    public class DeslocamentoAbastecimento : Deslocamento
    {
        public override int IdOcorrenciaInicio => 17;
        public override int IdOcorrenciaFim => 18;
    }
}
