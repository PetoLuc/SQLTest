namespace HR.Dol.Attributes {
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ColumnNameAttribute(string Name) : Attribute {
        public string Name { get; } = Name;
    }
}
