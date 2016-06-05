using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebApplication1.Repository
{
    public abstract class CsvRepository
    {
        internal string _source;
        public CsvRepository(string source)
        {
            _source = String.Format(@"Repository/csv/{0}.csv", source);
            if (!File.Exists(_source))
            {
                throw new ArgumentException(String.Format("The requested repository {0} doesn't exist", source));
            }
        }        
    }
}