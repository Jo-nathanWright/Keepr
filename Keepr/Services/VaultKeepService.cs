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
      return _vkr.Create(newVK);
    }
  }
}