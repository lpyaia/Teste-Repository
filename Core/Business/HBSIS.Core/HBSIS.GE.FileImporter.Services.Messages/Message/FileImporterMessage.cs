﻿using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using HBSIS.GE.FileImporter.Services.Commons.Base.Message;
using System;

namespace HBSIS.GE.FileImporter.Services.Messages.Message
{
    public class FileImporterMessage : BaseMessage<FileImporterMessage>
    {
        public string FileType { get; set; }

        public SpreadsheetLineBase Data { get; set; }

        public int CountRows { get; set; }

        public int TotalFileRows { get; set; }

        public string FileGuid { get; set; }

        public FileImporterMessage() { }
            
        public FileImporterMessage(string fileType, SpreadsheetLineBase data, string fileGuid, int countRows, int totalFileRows)
        {
            FileType = fileType;
            Data = data;
            FileGuid = fileGuid;
            CountRows = countRows;
            TotalFileRows = totalFileRows;
        }
    }
}