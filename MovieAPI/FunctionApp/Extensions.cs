using FunctionApp.Database.Models;
using System;
using System.Linq;

namespace FunctionApp {
    public static class ObjectExtensions {
        public static object GetPropertyValue<T>(this T obj, string propertyName) {
            return typeof(T).GetProperty(propertyName)?.GetValue(obj, null);
        }

        public static bool HasProperty<T>(this T obj, string propertyName) {
            var properties = typeof(Movie).GetProperties().Select(p => p.Name);
            return properties.Any(p => string.Equals(p, propertyName, StringComparison.OrdinalIgnoreCase));
        }
    }

    public static class TypeExtensions {
        public static bool IsNumericType(this Type type) {
            return type == typeof(byte) ||
               type == typeof(sbyte) ||
               type == typeof(short) ||
               type == typeof(ushort) ||
               type == typeof(int) ||
               type == typeof(uint) ||
               type == typeof(long) ||
               type == typeof(ulong) ||
               type == typeof(float) ||
               type == typeof(double) ||
               type == typeof(decimal);
        }
    }
}