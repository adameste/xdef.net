﻿using System;
using System.CodeDom;
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
        protected readonly string _className;

        protected List<JavaMethod> _methods = new List<JavaMethod>();

        public CodeGenerator(string jarPath, string className)
        {
            _jarPath = jarPath;
            _className = className;
            ParsePublicMethods();
        }

        private void ParsePublicMethods()
        {
            var parserOutput = GetParserOutput();
            var regex = new Regex("public ?([^\\s]*) (.*) (.*)\\((.*)\\)");
            var results = regex.Matches(parserOutput);
            foreach (Match parsed in results)
            {
                _methods.Add(new JavaMethod()
                {
                    OriginalDefinition = parsed.Groups[0].Value,
                    IsStatic = parsed.Groups[1].Value == "static",
                    ReturnType = parsed.Groups[2].Value.Split('.').Last().Trim(),
                    Name = parsed.Groups[3].Value,
                    Arguments = parsed.Groups[4].Value.Split(',').Select(p => getArgumentType(p)).ToList()
                });
            }
            HandleDuplicitMethods();
        }

        private string getArgumentType(string argList)
        {
            if (argList.EndsWith("..."))
                return argList.Split('.', StringSplitOptions.RemoveEmptyEntries).Last() + "...";
            else
                return argList.Split('.').Last().Trim();
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

        private string GetParserOutput()
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
