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
      string sql = "SELECT * FROM keeps";
      return _db.Query<Keep>(sql).ToList();
    }
  }
}