﻿using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace xdef.codegen
{
    public class JavaCodeGenerator : CodeGenerator
    {
        private string InstanceName
        {
            get
            {
                var className = _className.Split('.').Last();
                if (className.StartsWith("XD"))
                    className = "xd" + className.Substring(2);
                else
                    className = className.First().ToString().ToLower() + className.Substring(1);
                return className;
            }
        }

        private Dictionary<string, string> _argumentReaderTable = new Dictionary<string, string>()
        {
            { "int", "reader.readInt()" },
            { "String", "reader.readSharpString()" },
            { "URL", "new URL(reader.readSharpString()" },
            { "InputStream", "new RemoteInputStream(new RemoteStreamWrapper(client, reader.readInt()))" },
            { "OutputStream", "new RemoteOutputStream(new RemoteStreamWrapper(client, reader.readInt()))" },
            { "ReportWriter", "(ReportWriter) client.getLocalObject(reader.readInt())"},
            { "ReportReader", "(ReportReader) client.getLocalObject(reader.readInt())"},
            { "File", "new File(reader.readSharpString())" },
            { "Element", "reader.readElement()" }
        };

        private Dictionary<string, string> _responseType = new Dictionary<string, string>()
        {
            { "int", "reader.readInt()" },
            { "String", "reader.readSharpString()" },
            { "boolean", "reader.readBoolean()" },
            { "InputStream", "new RemoteInputStream(new RemoteStreamWrapper(client, reader.readInt()))" },
            { "ReportWriter", "(ReportWriter) client.getLocalObject(reader.readInt())"},
            { "ReportReader", "(ReportReader) client.getLocalObject(reader.readInt())"},
            { "File", "new File(reader.readSharpString())" },
            { "Element", "reader.readElement()" }
        };

        private List<string> _nonWrappingTypes = new List<string>()
        {
            "int", "boolean", "String", "byte", "Element", "Document", "void"
        };

        public JavaCodeGenerator(string jarPath, string className) : base(jarPath, className)
        {
        }

        public override string GetCode()
        {
            using (var writer = new StringWriter())
            {
                GenerateConsts(writer);
                writer.WriteLine();
                GenerateHandleSwitch(writer);
                GenerateFunctions(writer);
                return writer.ToString();
            }
        }

        private void GenerateHandleSwitch(StringWriter writer)
        {
            writer.Write($@"
    // Autogenerated handler method
    @Override
    public Response handleRequest(final Request request) {{
        final BinaryDataReader reader = request.getReader();
        try {{
            switch (request.getFunction()) {{
");
            foreach (var it in _methods)
            {
                writer.Write($@"
                case {it.ConstName}:
                    return {it.JavaName}(reader);");
            }
            writer.WriteLine($@"
                default:
                    return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION, ""XDFactory: Unknown function."");
            }}
        }} catch (final Exception ex) {{
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST, ex.getMessage());
        }}
    }}
");
        }


        private void GenerateFunctions(StringWriter writer)
        {
            foreach (var it in _methods)
            {
                writer.Write($@"
    // Autogenerated method
    //{it.OriginalDefinition}
    public Response {it.JavaName} (BinaryDataReader reader) throws IOException
    {{
        // Read params here");
                var i = 1;
                foreach (var arg in it.Arguments.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    writer.Write($@"
        {arg} arg{i++} = {(_argumentReaderTable.TryGetValue(arg, out var x) ? x : "reader.read" + arg + "()")};");
                }
                writer.Write($@"
        // Do actions");
                if (_nonWrappingTypes.Contains(it.ReturnType))
                {
                    writer.Write($@"
        {it.ReturnType} res = {InstanceName}.{it.Name}({string.Join(",", Enumerable.Range(1, it.Arguments.Where(x => !string.IsNullOrWhiteSpace(x)).Count()).Select(p => $"arg{p}"))});");
                }
                else
                {

                    writer.Write($@"
        {it.ReturnType}Wrapper wrap = new {it.ReturnType}Wrapper(client, {InstanceName}.{it.Name}({string.Join(",", Enumerable.Range(1, it.Arguments.Where(x => !string.IsNullOrWhiteSpace(x)).Count()).Select(p => $"arg{p}"))}));");
                }
                writer.Write($@"
        BinaryDataBuilder builder = new BinaryDataBuilder();");
                if (_nonWrappingTypes.Contains(it.ReturnType))
                {
                    writer.Write($@"
        builder.add(res);");
                }
                else
                {
                    writer.Write($@"
        builder.add(client.registerRemoteObject(wrap));");
                }
                writer.Write($@"
        return new Response(builder.build());
    }}
");

            }
        }

    private void GenerateConsts(StringWriter writer)
    {
        int i = 1;
        foreach (var it in _methods)
        {
            writer.WriteLine($"private static final int {it.ConstName} = {i++};");
        }
    }
}
}
