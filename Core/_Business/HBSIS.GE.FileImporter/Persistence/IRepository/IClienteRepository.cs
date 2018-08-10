namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IClienteRepository<TEntity>
    {
        TEntity Get(long cdCliente);
    }
}