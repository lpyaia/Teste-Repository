namespace HBSIS.MercadoLes.Services.Commons.Integration
{
    public interface IIntegrationRequest : IIntegrationStatus
    {
        string EndPoint { get; set; }
        string EndPointName { get; set; }
        object Request { get; set; }
        object Response { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}