
namespace HBSIS.MercadoLes.CustoAdicionalBrf.Entities
{
    public class DivergenciaDiaria : Ocorrencia
    {
        public int QuantidadeDiariaPrevista { get; set; }
        public int QuantidadeDiariaRealizada { get; set; }

        public DivergenciaDiaria()
        {
            Nome = "DivergenciaDiaria";
            Codigo = "01";
        }
    }
}