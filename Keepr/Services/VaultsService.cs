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
  }
}