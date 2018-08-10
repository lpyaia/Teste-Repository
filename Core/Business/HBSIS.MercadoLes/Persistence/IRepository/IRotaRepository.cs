namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IRotaRepository<TEntity>
    {
        TEntity GetRotaIndicadoresFluxoLES(long cdRota);
    }
}