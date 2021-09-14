using System.Collections.Generic;
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
      (name, description, isPrivate, img, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }

    public Vault Update(Vault updatedVault)
    {
      string sql = @"
      UPDATE vaults
      SET
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @IsPrivate
      WHERE id = @Id
      ;";
      _db.Execute(sql, updatedVault);
      return updatedVault;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal List<Vault> GetByCreator(string creatorId)
    {
      string sql = @"
      SELECT
        a.*,
        v.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @creatorId;
      ";
      return _db.Query<Profile, Vault, Vault>(sql, (p, v) =>
      {
        v.Creator = p;
        return v;
      }, new { creatorId }, splitOn: "id").ToList<Vault>();
    }
  }
}