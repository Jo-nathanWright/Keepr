using Keepr.Repositories;

namespace Keepr.Controllers
{
    public class VaultsController
    {
    private readonly VaultsRepository _repo;

    public VaultsController(VaultsRepository repo)
    {
      _repo = repo;
    }
  }
}