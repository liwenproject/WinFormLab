using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;        //Add
using System.Windows.Forms;         //Add

namespace WinFormLab.Helpers
{
    public static class ComboBoxHelper
    {
        /// <summary>將 Enum 載入 Combox
        /// ComboBox 的值是 Enum 的項目
        /// ComboBox 的顯示是 Enum 項目的 [Description]，若無[Description]則顯示項目
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="cb">ComboBox</param>
        public static void LoadEnum<T>(this ComboBox cb) where T : struct, System.IConvertible
        {
            if (!typeof(T).IsEnum)
                return;

            var tmp = Enum.GetValues(typeof(T)).Cast<Enum>()
                .Select(o => new
                {
                    Value = o,
                    Attrs = o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)
                });

            var data = tmp.Select(o => new
            {
                Value = o.Value,
                Desc = o.Attrs.Length == 0 ? o.Value.ToString() : ((DescriptionAttribute)o.Attrs[0]).Description.ToString()
            }).ToList();

            cb.DisplayMember = "Desc";
            cb.ValueMember = "Value";
            cb.DataSource = data;
        }

    }
}
