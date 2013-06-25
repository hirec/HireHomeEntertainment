using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireHomeEntertainment.Model
{
    public class Argument
    {
        public String Name { get; set; }
        public String DefaultValue { get; set; }
        public String Description { get; set; }
    }

    public class ArgumentList : List<Argument> { }

    public class ArgumentExample
    {
        public String Description { get; set; }
        public String Example { get; set; }
    }
}
