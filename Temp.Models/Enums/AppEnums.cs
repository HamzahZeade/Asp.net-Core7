namespace Temp.Models.Enums
{



    public enum Language
    {
        Arabic = 1,
        English = 2,
    }

    public enum OrderByEnum : byte
    {
        Desc = 1,
        Asc = 2
    }

    public enum PermissionsEnum : int
    {
        Create = 1,
        View = 2,
        Edit = 3,
        Delete = 4,
    }
    [Flags]
    public enum AppRolesEnum : int
    {
        SuperAdmin = 1, // مدير النظام
        Admin = 2, //متابع جهة
        Basic = 3, // قائد  جهة
    }


    public enum AuditTypeEnum : byte
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }


}
