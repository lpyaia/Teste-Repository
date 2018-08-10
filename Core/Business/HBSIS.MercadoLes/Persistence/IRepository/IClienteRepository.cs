namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IClienteRepository<TEntity>
    {
        TEntity Get(long cdCliente);
    }
}