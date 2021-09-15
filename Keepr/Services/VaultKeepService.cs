using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepService
  {
    private readonly VaultKeepRepository _vkr;
    private readonly VaultsRepository _vr;
    private readonly KeepsRepository _kr;

    public VaultKeepService(VaultKeepRepository vkr, VaultsRepository vr, KeepsRepository kr)
    {
      _vkr = vkr;
      _vr = vr;
      _kr = kr;
    }

    internal VaultKeep Create(VaultKeep newVK)
    {
      Vault vault = _vr.GetbyId(newVK.VaultId);
      if(vault.CreatorId != newVK.CreatorId )
      {
        throw new Exception("Access Denied");
      }
      VaultKeep found = _vkr.findExisting(newVK.VaultId, newVK.KeepId, newVK.CreatorId);
      if(found != null)
      {
        throw new Exception("You're Already in this VaultKeep");
      }
      return _vkr.Create(newVK); //Use vaultId to cross refernce owner.
    }

    private VaultKeep GetById(int id)
    {
      VaultKeep found = _vkr.GetById(id);
      if(found == null)
        {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep toDelete = GetById(id);
      if(toDelete.CreatorId != userId)
      {
        throw new Exception("Access Denied");
      }
      _vkr.Delete(id);
    }
  }
}