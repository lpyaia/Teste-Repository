namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IRotaRepository<TEntity>
    {
        TEntity GetRotaIndicadoresFluxoLES(long cdRota);
    }
}