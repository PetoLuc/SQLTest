namespace HR.Dol.Attributes {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TableNameAttribute(string name) : Attribute {
        public string Name { get; } = name;        
    }
}
