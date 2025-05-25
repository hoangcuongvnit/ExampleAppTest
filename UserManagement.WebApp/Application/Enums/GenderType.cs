using System.ComponentModel;

namespace UserManagement.WebApp.Application.Enums
{
    public enum GenderType
    {
        [Description("Not Specified")]
        notSpecified = 0,
        [Description("Male")]
        male = 1,
        [Description("Female")]
        female = 2,
    }
}
