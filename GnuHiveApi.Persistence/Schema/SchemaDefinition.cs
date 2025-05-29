namespace GnuHiveApi.Persistence.Schema;

public abstract class SchemaDefinition
{
    public abstract class ExampleSchema
    {
        public class ExampleTable
        {
            public int Id { get; set; }
        
            [IgnoreForPersistence]
            public int Name { get; set; }
        }
    }
}