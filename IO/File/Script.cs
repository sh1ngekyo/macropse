using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.File
{
    public struct Script
    {
        public string Name { get; private set; }
        public string Content { get; private set; }

        public Script(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
