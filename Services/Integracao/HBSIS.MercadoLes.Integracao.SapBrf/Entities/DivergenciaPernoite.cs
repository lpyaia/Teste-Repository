namespace HBSIS.MercadoLes.Integracao.SapBrf.Entities
{
    public class DivergenciaPernoite : Ocorrencia
    {
        public int Quantidade { get; set; }

        public DivergenciaPernoite()
        {
            Nome = "DivergenciaPernoite";
            Codigo = "2";
        }
    }
}