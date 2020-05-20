﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Navigation;

namespace xdef.codegen
{
    public class SharpCodeGenerator : CodeGenerator
    {
        private Dictionary<string, string> _returnDeserializationTable = new Dictionary<string, string>()
        {
            { "String", "return reader.ReadString();" },
            { "int", "return reader.ReadInt32();" },
            { "boolean", "return reader.ReadBoolean();" },
            { "byte", "return reader.ReadByte();" },
            { "void", "return;" },
            { "String[]", "return reader.ReadStringArray();" },
            { "Element", "return XElement.Parse(reader.ReadString());" }
        };

        public SharpCodeGenerator(string jarPath, string className) : base(jarPath, className)
        {
        }

        public override string GetCode()
        {
            using (var writer = new StringWriter())
            {
                GenerateConsts(writer);
                writer.WriteLine();
                GenerateFunctions(writer);
                return writer.ToString();
            }
        }

        private void GenerateFunctions(StringWriter writer)
        {
            foreach (var it in _methods)
            {
                writer.Write($@"
    // Autogenerated method
    //{it.OriginalDefinition}
    public {it.ReturnType} {it.SharpName}({string.Join(", ", it.Arguments.Where(p => !string.IsNullOrWhiteSpace(p)).Select((p, idx) => $"{p} arg{idx}"))})
    {{
        using (var builder = new BigEndianDataBuilder())
        {{
            // Serialize args here");
                foreach (var arg in it.Arguments.Where(p => !string.IsNullOrWhiteSpace(p)).Select((p, idx) => $"arg{idx}"))
                {
                    writer.Write($@"
            builder.Add({arg});");
                }
                writer.Write($@"
            var res = SendRequestWithResponse(new Request({it.ConstName}, builder.Build(), ObjectId));
            using (var reader = res.Reader)
            {{");
                if (_returnDeserializationTable.TryGetValue(it.ReturnType, out var val))
                {
                    writer.Write($@"
                {val}");
                }
                else
                {
                    writer.Write($@"
                // Read response here
                return new {it.ReturnType}(reader.ReadInt32(), _client);");
                }
                writer.Write($@"
            }}
        }}
    }}
");
            }
        }

        private void GenerateConsts(StringWriter writer)
        {
            int i = 1;
            foreach (var it in _methods)
            {
                writer.WriteLine($"private const int {it.ConstName} = {i++};");
            }
        }
    }
}