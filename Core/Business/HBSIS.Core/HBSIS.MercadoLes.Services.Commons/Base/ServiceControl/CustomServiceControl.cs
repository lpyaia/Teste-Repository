//using Topshelf;

namespace HBSIS.MercadoLes.Services.Commons.Base.ServiceControl
{
    //public class CustomServiceControl : Topshelf.ServiceControl
    public class CustomServiceControl
    {
        public virtual bool Start()
        {
            return true;
        }

        public virtual bool Stop()
        {
            return true;
        }
    }
}