namespace Temp.Models.Exceptions
{
    public static class EnumExtensions
    {
        public static string GetEnumDisplayText(this Enum value)
        {
            string output = null;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            var attrs = fi.GetCustomAttributes(typeof(DisplayText), false) as DisplayText[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }
        public static string GetLabelId(this Enum value)
        {
            string output = null;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            var attrs = fi.GetCustomAttributes(typeof(EnumLabelId), false) as EnumLabelId[];
            if (attrs?.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }
    }
    public class DisplayText : System.Attribute
    {
        private readonly string _value;

        public DisplayText(string value)
        {
            _value = value;
        }

        public string Value => _value;
    }

    public class EnumLabelId : System.Attribute
    {
        private readonly string _value;

        public EnumLabelId(string value)
        {
            _value = value;
        }

        public string Value => _value;
    }
}
