using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
    private readonly VaultsRepository _vr;
    private readonly KeepsRepository _kr;

    public VaultsService(VaultsRepository vr, KeepsRepository kr)
    {
      _vr = vr;
      _kr = kr;
    }

    internal Vault Get(int id, bool checkPrivate = false)
    {
      Vault found = _vr.GetbyId(id);
      if(found == null || (checkPrivate == true && found.IsPrivate == true))
      {
        throw new Exception("Access Denied");
      }
      return found;
    }

    internal Vault Create(Vault newVault)
    {
      return _vr.Create(newVault);
    }

    internal Vault Update(Vault updateVault)
    {
      Vault original = Get(updateVault.Id, true);
      if(original.CreatorId != updateVault.CreatorId)
      {
        throw new Exception("Access Denied");
      }
      original.Name = updateVault.Name ?? original.Name;
      original.Description = updateVault.Description ?? original.Description;
      original.IsPrivate = updateVault.IsPrivate ?? original.IsPrivate;
      _vr.Update(original);
      return original;
    }

    internal void Delete(int vaultId, string accountId)
    {
      Vault deleted = Get(vaultId, false);
      if(deleted.CreatorId != accountId)
      {
        throw new Exception("Access Denied");
      }
       List<KeepViewModel> keep = _kr.GetAll(vaultId);
       if(keep != null){
         for (int i = 0; i < keep.Count; i++)
      {
        keep[i].Keeps--;
        _kr.Update(keep[i]);
      }
       }
      _vr.Delete(vaultId);
    }

    internal List<Vault> GetVaultsByCreator(string id, Account userInfo)
    {
      List<Vault> vaults = _vr.GetByCreator(id);
      if(userInfo == null)
      {
        vaults = vaults.FindAll(v => v.IsPrivate == false);
      } else {
        for (int i = 0; i < vaults.Count; i++)
        {
          Vault vault = _vr.GetbyId(vaults[i].Id);
          if(vault.CreatorId != userInfo.Id){
            vaults = vaults.FindAll(v => v.IsPrivate == false);
          }
        }
      }
      return vaults;
    }
  }
}