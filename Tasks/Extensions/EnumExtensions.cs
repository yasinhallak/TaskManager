using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TaskManager
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
            {
                return string.Empty;
            }
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .FirstOrDefault()?
                       .GetCustomAttribute<DisplayAttribute>()?
                       .GetName() ?? enumValue.ToString();
        }

        public static T GetEnumValue<T>(this string displayName)
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }
            return Enum.GetValues(enumType)
                   .Cast<T>()
                   .FirstOrDefault(v =>
                   {
                       var name = Enum.GetName(enumType, v);
                       if (name != null)
                       {
                           var field = enumType.GetField(name);
                           if (field != null)
                           {
                               return ((DisplayAttribute)Attribute.GetCustomAttribute(field,
                                   typeof(DisplayAttribute)))?.GetName() == displayName;
                           }
                       }
                       return false;
                   });
        }

        public static T[] GetEnumValues<T>(this string[] displayNames)
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }

            return displayNames.Select(displayName => Enum.GetValues(enumType).Cast<T>().FirstOrDefault(v =>
            {
                var name = Enum.GetName(enumType, v);
                if (name == null)
                {
                    return false;
                }
                var field = enumType.GetField(name);
                if (field != null)
                {
                    return ((DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)))?.GetName() == displayName;
                }
                return false;
            })).ToArray();
        }
    }
}