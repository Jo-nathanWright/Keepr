using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Keep> GetAll()
    {
      string sql = @"
      SELECT
        k.*,
        a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id").ToList();
    }
    internal List<KeepViewModel> GetAll(int vaultId)
    {
      string sql = @"
      SELECT
        a.*,
        k.*,
        vk.id AS vaultKeepId
      FROM vaultKeep vk
      JOIN accounts a ON vk.creatorId = a.id
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId = @vaultId;
      ";
      return _db.Query<Profile, KeepViewModel, KeepViewModel>(sql, (p, k) => {
        k.Creator = p;
        return k;
      }, new {vaultId}, splitOn: "id").ToList();
    }

    public Keep GetbyId(int id)
    {
      string sql = @"
      SELECT
        k.*,
        a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new{id}, splitOn: "id").FirstOrDefault();
    }

    internal List<Keep> GetByCreator(string creatorId)
    {
      string sql = @"
      SELECT
        a.*,
        k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @creatorId;
      ";
      return _db.Query<Profile, Keep, Keep>(sql, (p, k) =>
      {
        k.Creator = p;
        return k;
      }, new { creatorId }, splitOn: "id").ToList<Keep>();
    }

    public Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps(name, description, img, views, shares, keeps, creatorId)
      VALUES(@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return GetbyId(newKeep.Id);
    }

    public Keep Update(Keep editedKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        img = @Img
      WHERE id = @Id
      ";
      _db.Execute(sql, editedKeep);
      return editedKeep;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}