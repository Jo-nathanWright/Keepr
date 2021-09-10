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

    internal Vault Get(int id, bool checkPrivate = true)
    {
      Vault found = _repo.GetbyId(id);
      if(found == null || (checkPrivate == false && found.IsPrivate == true) )
      {
        throw new Exception("Access Denied");
      }
      return found;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault Update(Vault updateVault)
    {
      Vault original = Get(updateVault.Id, false);
      if(original.CreatorId != updateVault.CreatorId)
      {
        throw new Exception("Access Denied");
      }
      original.Name = updateVault.Name ?? original.Name;
      original.Description = updateVault.Description ?? original.Description;
      original.IsPrivate = updateVault.IsPrivate ?? original.IsPrivate;
      _repo.Update(original);
      return original;
    }

    internal void Delete(int vaultId, string accountId)
    {
      Vault deleted = Get(vaultId, true);
      if(deleted.CreatorId != accountId)
      {
        throw new Exception("Access Denied");
      }
      _repo.Delete(vaultId);
    }
  }
}