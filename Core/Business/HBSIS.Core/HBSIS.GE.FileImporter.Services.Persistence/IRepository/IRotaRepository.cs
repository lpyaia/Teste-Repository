namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IRotaRepository<TEntity>
    {
        TEntity GetRotaIndicadoresFluxoLES(long cdRota);
    }
}