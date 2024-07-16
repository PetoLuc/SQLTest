using HR.Dol.Attributes;
using System.Reflection;

namespace HR.Dal.Helpers {
    public static class MetadataHelper {
        public static string? GetColumnName<T>(string propertyName) {
            var property = typeof(T).GetProperty(propertyName);
            if (property != null) {
                var attribute = property.GetCustomAttribute<ColumnNameAttribute>();
                return attribute?.Name;
            }
            return null;
        }

        public static string? GetTableName<T>() {
            var attribute = typeof(T).GetCustomAttribute<TableNameAttribute>();
            if (attribute != null) {
                return attribute?.Name;
            }
            return null;
        }

        public static string? GetColumns<T>() {
            var properties = typeof(T).GetProperties();
            List<string> columnNames = [];

            foreach (var property in properties) {
                var nameAttrribute = property.GetCustomAttribute<ColumnNameAttribute>();
                if (nameAttrribute != null)
                    columnNames.Add(nameAttrribute.Name);
            }
            return string.Join(", ", columnNames);
        }
    }
}
