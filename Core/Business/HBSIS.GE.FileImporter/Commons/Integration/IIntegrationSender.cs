using System;

namespace HBSIS.GE.FileImporter.Services.Commons.Integration
{
    public interface IIntegrationSender<TRequest, TResponse>
    {
        void SendSync(TRequest model);

        void SendAsync(TRequest model);

        void Resend(Guid idRequest);

        void ResendAll();
    }
}