using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
  [ApiController]
    [Route("/api/[controller]")]
    public class VaultsService : ControllerBase
    {
    private readonly VaultsService _vs;

    public VaultsService(VaultsService vs)
    {
      _vs = vs;
    }
  }
}