using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using HBSIS.GE.FileImporter.Services.Commons.Base.Message;
using System;

namespace HBSIS.GE.FileImporter.Services.Messages.Message
{
    public class FileImporterMessage : BaseMessage<FileImporterMessage>
    {
        public string FileType { get; set; }

        public SpreadsheetLineBase Data { get; set; }

        public FileImporterMessage() { }
            
        public FileImporterMessage(string fileType, SpreadsheetLineBase data)
        {
            FileType = fileType;
            Data = data;
        }
    }
}