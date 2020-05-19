using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;

namespace xdef.codegen
{
    public abstract class CodeGenerator
    {
        private readonly string _jarPath;
        private readonly string _className;

        protected List<JavaMethod> _methods = new List<JavaMethod>();

        public CodeGenerator(string jarPath, string className)
        {
            _jarPath = jarPath;
            _className = className;
            ParsePublicMethods();
        }

        private void ParsePublicMethods()
        {
            var parserOutput = GerParserOutput();
            using (var reader = new StringReader(parserOutput))
            {
                reader.ReadLine(); // Compiled from
                reader.ReadLine(); // classdef
                var regex = new Regex(".*\\s(.*)\\s(.*).*\\((.*)\\)");
                while (true)
                {
                    string line = reader.ReadLine();
                    // Skip consts and public fields
                    if (line.ToLower().Contains("final") && !line.ToLower().Contains("(")) continue;
                    if (line.StartsWith("}")) break;
                    var parsed = regex.Match(line);
                    _methods.Add(new JavaMethod()
                    {
                        OriginalDefinition = line,
                        ReturnType = parsed.Groups[1].Value.Split('.').Last(),
                        Name = parsed.Groups[2].Value,
                        Arguments = parsed.Groups[3].Value.Split(',').Select(p => p.Split('.').Last()).ToList()
                    }) ;
                }
            }
            HandleDuplicitMethods();
        }

        private void HandleDuplicitMethods()
        {
            var methodNames = _methods.Select(p => p.Name).Distinct();
            foreach (var name in methodNames)
            {
                var methods = _methods.Where(p => p.Name == name);
                if (methods.Count() > 1)
                {
                    int i = 1;
                    foreach (var method in methods)
                    {
                        method.Overload = i++;
                    }
                }
            }
        }

        private string GerParserOutput()
        {
            var process = Process.Start(new ProcessStartInfo()
            {
                FileName = "javap",
                Arguments = $"-cp {_jarPath} {_className}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            });

            return process.StandardOutput.ReadToEnd();

        }

        public abstract string GetCode();
    }
}
