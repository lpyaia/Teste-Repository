using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Message;
using System;
using System.ComponentModel.DataAnnotations;

namespace HBSIS.MercadoLes.Services.Commons.Logging
{
    public class LogEvent : SpecializedMessage<LogEvent>
    {
        public DateTime Date { get; set; }

        [StringLength(255)]
        public string Thread { get; set; }

        public string Level { get; set; }

        [StringLength(255)]
        public string Logger { get; set; }

        [StringLength(4000)]
        public string Message { get; set; }

        [StringLength(2000)]
        public string Exception { get; set; }
    }
}