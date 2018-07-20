namespace HBSIS.MercadoLes.Services.Commons.Integration.Config
{
    public interface IIntegrationConfig
    {
        string Name { get; set; }

        string UserName { get; set; }

        string Token { get; set; }

        string Password { get; set; }

        string Url { get; set; }

        string Url2 { get; set; }

        int Interval { get; set; }

        bool Enabled { get; set; }

        int Attempts { get; set; }

        string ExtensionData { get; set; }
    }
}