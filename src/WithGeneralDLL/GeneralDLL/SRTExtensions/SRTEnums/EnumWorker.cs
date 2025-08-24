// Ignore Spelling: SRT Enums Enum

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GeneralDLL.SRTExtensions.SRTEnums
{
    public static class EnumWorker
    {
        public class EnumItem<T>
            where T : System.Enum
        {
            public int Cost { get => Value.SRT_Enum_ToInt(); }
            public T Value { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
        }
        /// <summary>
        /// Get Description Of Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetDescription<T>(T Value)
            where T : System.Enum
        {
            var lst = GetData<T>(null);

            return lst.FirstOrDefault(q => q.Value.ToString() == Value.ToString())?.Description ?? "-";
        }

        /// <summary>
        /// Get Description Of Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetName<T>(T Value)
            where T : System.Enum
        {
            var lst = GetData<T>(null);

            return lst.First(q => q.Value.ToString() == Value.ToString()).Name;
        }

        /// <summary>
        /// Get List Of Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumItem<T>> GetData<T>(List<T> lstRemove)
            where T : System.Enum
        {
            try
            {
                var lst = Enum.GetValues(typeof(T))
                            .Cast<Enum>()
                            .Select(value => new
                            {
                                (Attribute.GetCustomAttribute
                                (value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                                value
                            })
                            .OrderBy(item => item.value)
                            .ToList();

                if (lstRemove != null)
                {
                    var lr = lstRemove.Cast<Enum>()
                            .Select(value => new
                            {
                                (Attribute.GetCustomAttribute
                                (value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                                value
                            })
                            .OrderBy(item => item.value)
                            .ToList();

                    lst.RemoveAll(q => lr.Any(r => r.Description == q.Description));
                }

                var lstRet = new List<EnumItem<T>>();
                lst.ForEach(q => lstRet.Add(new EnumItem<T>()
                {
                    Value = (T)q.value,
                    Description = q.Description,
                    Name = q.value.ToString()
                }));

                return lstRet;
            }
            catch
            {
                var lst = Enum.GetValues(typeof(T))
                            .Cast<Enum>().ToList();

                var lstRet = new List<EnumItem<T>>();
                lst.ForEach(q => lstRet.Add(new EnumItem<T>()
                {
                    Value = (T)q,
                    Description = q.ToString(),
                    Name = q.ToString()
                }));

                return lstRet;
            }
        }
    }
}
