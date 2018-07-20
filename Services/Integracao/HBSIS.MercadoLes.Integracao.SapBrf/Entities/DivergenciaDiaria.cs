
namespace HBSIS.MercadoLes.Integracao.SapBrf.Entities
{
    public class DivergenciaDiaria : Ocorrencia
    {
        public int QuantidadeDiariaPrevista { get; set; }
        public int QuantidadeDiariaRealizada { get; set; }

        public DivergenciaDiaria()
        {
            Nome = "DivergenciaDiaria";
            Codigo = "1";
        }
    }
}