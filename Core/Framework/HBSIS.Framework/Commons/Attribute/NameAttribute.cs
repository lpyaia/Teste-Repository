using System;

namespace HBSIS.Framework.Commons.Attribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class NameAttribute : System.Attribute
    {
        public string Name { get; private set; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}