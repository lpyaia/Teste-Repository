namespace HBSIS.GE.FileImporter.Services.Persistence.IRepository
{
    internal interface IMetasPainelIndicadoresRepository<TEntity>
    {
        TEntity GetByUnidadeNegocio(string cdUnidadeNegocio);
    }
}