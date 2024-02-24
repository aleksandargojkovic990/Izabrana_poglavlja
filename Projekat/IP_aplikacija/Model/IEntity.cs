using Microsoft.Data.SqlClient;

namespace Model
{
    public interface IEntity
    {
        string TableName { get; }
        string TableAlias { get; }
        string InsertValues { get; }
        string JoinTable { get; }
        string Where { get; }
        Dictionary<string, object> Set { get; }
        object SelectValues { get; }
        List<IEntity> GetEntities(SqlDataReader reader);
        string GetWhere();
        string GetSet();
    }
}
