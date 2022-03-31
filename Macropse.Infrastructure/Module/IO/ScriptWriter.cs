using System;

namespace Macropse.Infrastructure.Module.IO
{
    public static class FileWriter
    {
        public static void WriteScript(Script script)
        {
            try
            {
                System.IO.File.WriteAllText(script.Name, script.Content);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
