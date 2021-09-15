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

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultKeep WHERE id = @id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultKeep WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
    internal VaultKeep findExisting(int vaultId, int keepId, string creatorId)
    {
      string sql = "SELECT * FROM vaultKeep WHERE vaultId = @vaultId AND keepId = @keepId AND creatorId = @creatorId;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultId, keepId, creatorId });
    }
  }
}