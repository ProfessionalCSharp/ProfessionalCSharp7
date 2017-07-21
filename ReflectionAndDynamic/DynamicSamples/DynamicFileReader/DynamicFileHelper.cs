using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace DynamicFileReader
{
    public class DynamicFileHelper
    {
        public IEnumerable<dynamic> ParseFile(string fileName)
        {
            var retList = new List<dynamic>();
            var fileStream = OpenFile(fileName);
            if (fileStream != null)
            {
                string[] headerLine = fileStream.ReadLine().Split(',').Select(s => s.Trim()).ToArray();
                while (fileStream.Peek() > 0)
                {
                    string[] dataLine = fileStream.ReadLine().Split(',');
                    dynamic dynamicEntity = new ExpandoObject();
                    for (int i = 0; i < headerLine.Length; i++)
                    {
                        ((IDictionary<string, object>)dynamicEntity).Add(headerLine[i], dataLine[i]);
                    }
                    retList.Add(dynamicEntity);
                }
            }
            return retList;
        }

        private StreamReader OpenFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new StreamReader(File.OpenRead(fileName));
            }
            return null;
        }
    }
}
