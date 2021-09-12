using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{

  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep newVK)
    {
      string sql = "INSERT INTO vaultKeep (creatorId, vaultId, keepId) VALUES (@CreatorId, @VaultId, @KeepId); SELECT LAST_INSERT_ID();";
      newVK.Id = _db.ExecuteScalar<int>(sql, newVK);
      return newVK;
    }
  }
}