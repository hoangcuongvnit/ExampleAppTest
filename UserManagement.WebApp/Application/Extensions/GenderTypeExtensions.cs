using System.ComponentModel;
using UserManagement.WebApp.Application.Enums;

namespace UserManagement.WebApp.Application.Extensions
{
    public static class GenderTypeExtensions
    {
        public static string GetDescription(this GenderType gender)
        {
            var type = gender.GetType();
            var memInfo = type.GetMember(gender.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return gender.ToString();
        }
    }
}
