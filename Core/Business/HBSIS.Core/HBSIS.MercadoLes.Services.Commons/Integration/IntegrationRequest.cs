using HBSIS.MercadoLes.Services.Commons.Enums;
using HBSIS.MercadoLes.Services.Commons.Helpers;
using System;

namespace HBSIS.MercadoLes.Services.Commons.Integration
{
    public class IntegrationRequest : IIntegrationStatus, IIntegrationRequest
    {
        public DateTime Date { get; set; }
        public string EndPoint { get; set; }
        public string EndPointName { get; set; }
        public object Request { get; set; }
        public object Response { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        public StatusLog Status { get; set; }

        public static IntegrationRequest Create(string endPointName, string endPoint, string userName, string password, object requestModel)
        {
            var ret = new IntegrationRequest();
            ret.Date = DateHelper.Now;
            ret.EndPoint = endPoint;
            ret.EndPointName = endPointName;
            ret.UserName = userName;
            ret.Password = password;
            ret.Request = requestModel;

            return ret;
        }

        public void SetSuccess(object responseModel = null)
        {
            Status = StatusLog.Success;
            Date = DateHelper.Now;
            Response = responseModel;
        }

        public void SetError(string errorMessage, object responseModel = null)
        {
            Status = StatusLog.Error;
            Date = DateHelper.Now;
            Message = errorMessage;
            Response = responseModel;
        }

        public void SetResponse(bool isValid, object responseModel)
        {
            var status = isValid ? StatusLog.Success : StatusLog.Error;
            SetResponse(status, responseModel);
        }

        public void SetResponse(StatusLog status, object responseModel)
        {
            this.Status = status;
            this.Response = responseModel;
        }
    }
}