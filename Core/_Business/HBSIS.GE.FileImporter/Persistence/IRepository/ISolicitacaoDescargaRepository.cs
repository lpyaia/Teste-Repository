namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface ISolicitacaoDescargaRepository<TEntity>
    {
        TEntity Get(long cdEntrega);
    }
}