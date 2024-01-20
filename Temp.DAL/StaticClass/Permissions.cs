namespace Temp.DAL.StaticClass
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }
        public static class Spekerses
        {
            public const string View = "Permissions.Spekers.View";
            public const string Create = "Permissions.Spekers.Create";
            public const string Edit = "Permissions.Spekers.Edit";
            public const string Delete = "Permissions.Spekers.Delete";
        }
    }
}
