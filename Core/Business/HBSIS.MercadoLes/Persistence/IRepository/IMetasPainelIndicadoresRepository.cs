namespace HBSIS.MercadoLes.Persistence.IRepository
{
    internal interface IMetasPainelIndicadoresRepository<TEntity>
    {
        TEntity GetByUnidadeNegocio(string cdUnidadeNegocio);
    }
}