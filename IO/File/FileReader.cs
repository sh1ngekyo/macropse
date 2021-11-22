using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace IO.File
{
    public static class FileReader
    {
        public static Script ReadScript(string path)
        {
            try
            {
                return new Script(Path.GetFileName(path), System.IO.File.ReadAllText(path));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
