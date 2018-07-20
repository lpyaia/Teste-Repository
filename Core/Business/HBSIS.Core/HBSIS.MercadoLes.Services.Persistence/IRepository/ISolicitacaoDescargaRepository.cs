namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface ISolicitacaoDescargaRepository<TEntity>
    {
        TEntity Get(long cdEntrega);
    }
}