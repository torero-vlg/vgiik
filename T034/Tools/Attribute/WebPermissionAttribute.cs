namespace T034.Tools.Attribute
{
    public class WebPermissionAttribute : System.Attribute
    {
        private readonly string _name;

        public WebPermissionAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}