namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IClienteRepository<TEntity>
    {
        TEntity Get(long cdCliente);
    }
}