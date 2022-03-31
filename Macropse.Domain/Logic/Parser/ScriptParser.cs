using Macropse.Domain.External;
using Macropse.Domain.Logic.Macro;
using Macropse.Domain.Logic.Output;
using Macropse.Infrastructure.Module.IO;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Macropse.Domain.Logic.Parser
{
    public sealed class ScriptParser
    {
        private OutputPackage<XDocument> LoadDoc(Script script)
        {
            try
            {
                var doc = XDocument.Parse(script.Content, LoadOptions.SetLineInfo);
                return new OutputPackage<XDocument>(item: doc, errorMessage: default);
            }
            catch (Exception e)
            {
                return new OutputPackage<XDocument>(item: default,
                                                     errorMessage: new IncorrectScriptMessage(script.Name, e.Message));
            }
        }

        public OutputPackage<ExecutableModule> Parse(Script script)
        {
            var docPac = LoadDoc(script);

            if (docPac.HasError)
            {
                return new OutputPackage<ExecutableModule>(item: default, errorMessage: docPac.ErrorMessage);
            }

            var headerPac = new ScriptRootBuilder().BuildObject(docPac.Item.Root);

            if (headerPac.HasError)
            {
                return new OutputPackage<ExecutableModule>(item: default, errorMessage: headerPac.ErrorMessage);
            }

            var macros = new List<Macros>();
            var macroBuilder = new MacroBuilder();

            foreach (var macroNode in docPac.Item.Element("root").Elements())
            {
                var macroPac = macroBuilder.BuildObject(macroNode);
                if (macroPac.HasError)
                {
                    return new OutputPackage<ExecutableModule>(item: default, errorMessage: macroPac.ErrorMessage);
                }
                macros.Add(macroPac.Item);
            }

            return new OutputPackage<ExecutableModule>(item: new ExecutableModule(headerPac.Item, macros), errorMessage: default);
        }
    }
}
