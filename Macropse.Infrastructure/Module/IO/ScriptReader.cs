using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.IO
{
    public static class ScriptReader
    {
        public static Script ReadScript(string path)
        {
            try
            {
                return new Script(System.IO.Path.GetFileName(path), System.IO.File.ReadAllText(path));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
