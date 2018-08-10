namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IClienteCelularRepository<TEntity>
    {
        TEntity Get(long cdCliente);
    }
}