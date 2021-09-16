using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
public class VaultKeepsController : ControllerBase
    {
    private readonly VaultKeepService _vks;
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;

    public VaultKeepsController(VaultKeepService vks, VaultsService vs, KeepsService ks)
    {
      _vks = vks;
      _vs = vs;
      _ks = ks;
    }

    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVK)
    {
        try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVK.CreatorId = userInfo.Id;
        VaultKeep vk = _vks.Create(newVK);
        newVK.Creator = userInfo;
        // Keep keep = _ks.Get(newVK.KeepId);
        // newVK.Creator = keep.Creator;
        return Ok(vk);
      }
        catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Delete(id, userInfo.Id);
        return Ok("VaultKeep Deleted");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}