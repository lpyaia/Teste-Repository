namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface ISolicitacaoDescargaRepository<TEntity>
    {
        TEntity Get(long cdEntrega);
    }
}