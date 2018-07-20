namespace HBSIS.MercadoLes.Services.Commons.Integration.Config
{
    public class IntegrationConfig : IIntegrationConfig
    {
        public IntegrationConfig()
        {
            Interval = 5;
            Attempts = 3;
            Enabled = true;
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Url { get; set; }

        public string Url2 { get; set; }

        public int Interval { get; set; }

        public bool Enabled { get; set; }

        public int Attempts { get; set; }

        public string Token { get; set; }

        public string ExtensionData { get; set; }
    }
}