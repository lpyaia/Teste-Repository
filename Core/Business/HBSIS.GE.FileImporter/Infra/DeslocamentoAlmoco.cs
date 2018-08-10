using HBSIS.Framework.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace HBSIS.GE.FileImporter.Infra.Entities
{
    [Serializable]
    public class DeslocamentoAlmoco : Deslocamento
    {
        public override int IdOcorrenciaInicio => 5;
        public override int IdOcorrenciaFim => 6;
    }
}
