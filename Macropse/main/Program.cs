﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using macropse.Parser;

namespace macropse
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new ScriptParser();
            var macrosList = parser.Parse(IO.File.FileReader.ReadScript("test.xml"));
            macrosList.ForEach(x => x.Run());
            Console.ReadKey();
        }
    }
}
