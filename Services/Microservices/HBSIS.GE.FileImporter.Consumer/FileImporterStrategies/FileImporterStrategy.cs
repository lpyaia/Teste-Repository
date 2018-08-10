using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Consumer.FileImporterStrategies
{
    public abstract class FileImporterStrategy<T> where T : SpreadsheetLineBase
    {
        public abstract void ImportData(T data);
    }
}
