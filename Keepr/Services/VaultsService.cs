using System;
using System.Collections.Generic;
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

    internal Vault Get(int id, string accountId, bool checkPrivate = false)
    {
      Vault found = _repo.GetbyId(id);
      if(found == null || (checkPrivate == true && found.IsPrivate == true) || (found.CreatorId != accountId && found.CreatorId != null) )
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
      Vault original = Get(updateVault.Id, updateVault.CreatorId, true);
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
      Vault deleted = Get(vaultId, accountId,  false);
      if(deleted.CreatorId != accountId)
      {
        throw new Exception("Access Denied");
      }
      _repo.Delete(vaultId);
    }

    internal List<Vault> GetVaultsByCreator(string id, Account userInfo)
    {
      List<Vault> vaults = _repo.GetByCreator(id);
      if(userInfo == null)
      {
        vaults = vaults.FindAll(v => v.IsPrivate == false);
      }
      return vaults;
    }
  }
}