using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;        //加入

namespace WinFormLab.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescription<T>(this T value) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
                return "";

            var fi = value.GetType().GetField(value.ToString());
            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return ((DescriptionAttribute)attributes[0]).Description;
            else
                return value.ToString();
        }


    }
}
