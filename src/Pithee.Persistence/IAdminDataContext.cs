using System.Data;

namespace Pithee.Persistence;

public interface IAdminDataContext
{
    IDbConnection CreateAdminConnection();
}