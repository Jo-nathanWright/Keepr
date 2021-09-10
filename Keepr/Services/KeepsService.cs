using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> Get()
    {
      return _repo.GetAll();
    }

    internal Keep Get(int id)
    {
      Keep keep = _repo.GetbyId(id);
      if(keep == null)
      {
        throw new Exception("Invalid Id");
      }
      return keep;
    }
    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal void Delete(int keepId, string accountId)
    {
      Keep delete = Get(keepId);
      if(delete.CreatorId != accountId)
      {
        throw new Exception("Access Denied");
      }
      _repo.Delete(keepId);
    }
  }
}