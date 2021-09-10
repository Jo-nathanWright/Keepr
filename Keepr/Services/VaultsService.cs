using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal Vault Get(int id)
    {
      Vault found = _repo.GetbyId(id);
      if(found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }
  }
}