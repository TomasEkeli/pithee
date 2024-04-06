using System.Data;

namespace Pithee.Persistence;

public interface IDataContext
{
    IDbConnection CreateConnection();
}