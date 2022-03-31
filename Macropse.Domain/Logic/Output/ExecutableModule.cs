using Macropse.Domain.Logic.Macro;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Output
{
    public class ExecutableModule
    {
        public ExecutableModule(Header root, List<Macros> macros)
        {
            Root = root;
            Macros = macros;
        }

        public Header Root { get; }

        public List<Macros> Macros { get; }
    }
}
