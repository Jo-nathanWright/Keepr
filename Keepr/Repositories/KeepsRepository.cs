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

    public Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps(name, description, img, views, shares, keeps, creatorId)
      VALUES(@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }
  }
}