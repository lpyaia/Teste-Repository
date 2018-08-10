namespace HBSIS.Framework.Commons.Attribute
{
    public class DisplayStringAttribute : System.Attribute
    {
        public string Name { get; private set; }

        public DisplayStringAttribute(string name)
        {
            Name = name;
        }
    }
}