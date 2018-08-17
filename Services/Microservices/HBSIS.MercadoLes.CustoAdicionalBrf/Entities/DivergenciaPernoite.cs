namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class DivergenciaPernoite : Ocorrencia
    {
        public int QuantidadeRealizada { get; set; }
        public int QuantidadePrevista { get; set; }

        public DivergenciaPernoite()
        {
            Nome = "DivergenciaPernoite";
            Codigo = "02";
        }

        public override string ToString()
        {
            return $"<{QuantidadePrevista};{QuantidadeRealizada}>";
        }
    }
}