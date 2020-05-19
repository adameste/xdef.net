using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xdef.codegen
{
    public class JavaMethod
    {
        public string OriginalDefinition { get; set; }
        public string ReturnType { get; set; }
        public string Name { get; set; }
        public List<string> Arguments { get; set; }
        public int? Overload { get; set; }

        public string ConstName => Overload == null ? $"FUNCTION_{Name.ToUpper()}" : $"FUNCTION_{Name.ToUpper()}_{Overload}";
        public string SharpName => Name.First().ToString().ToUpper() + Name.Substring(1);
        public string JavaName => Overload == null ? Name : $"{Name}{Overload}";
    }
}
