namespace GnuHiveApi.Persistence.Schema;

public class SchemaAttribute : Attribute
{
    public SchemaAttribute(string name) => this.Name = name;
    public string Name { get; }
}

public class IgnoreForPersistenceAttribute : Attribute { }