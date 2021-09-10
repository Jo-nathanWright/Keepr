using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
    {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Vault GetbyId(int id)
    {
      string sql = @"
      SELECT
        v.*,
        a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
      {
        v.Creator = p;
        return v;
      }, new{id}, splitOn: "id").FirstOrDefault();
    }

    public Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }
  }
}