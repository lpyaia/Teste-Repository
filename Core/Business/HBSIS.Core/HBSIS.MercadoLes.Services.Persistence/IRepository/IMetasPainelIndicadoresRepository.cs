namespace HBSIS.MercadoLes.Services.Persistence.IRepository
{
    internal interface IMetasPainelIndicadoresRepository<TEntity>
    {
        TEntity GetByUnidadeNegocio(string cdUnidadeNegocio);
    }
}