using System;

namespace HBSIS.Framework.Data.Mongo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MongoCollectionNameAttribute : System.Attribute
    {
        public string Name { get; private set; }

        public MongoCollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}