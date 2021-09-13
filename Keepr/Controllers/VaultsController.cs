using System;
using System.Collections.Generic;
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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _rs;
    private readonly KeepsService _ks;

    public VaultsController(VaultsService rs, KeepsService ks)
    {
      _rs = rs;
      _ks = ks;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> Get(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if(userInfo == null){
          Vault unAuth = _rs.Get(id, true);
          return Ok(unAuth);
        }
        Vault vault = _rs.Get(id, false);
        if(vault.CreatorId != userInfo.Id && vault.IsPrivate == true)
        {
          throw new Exception("Access Denied");
        }
        //Get account and see if null, if null yell at person, if not let checkPrivate = false
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<KeepViewModel>>> GetKeeps(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _rs.Get(id);
        if(vault.IsPrivate == true && userInfo == null)
        {
          throw new Exception("Access Denied");
        }
        List<KeepViewModel> vaultKeep = _ks.getByVaultId(id);
        return Ok(vaultKeep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = userInfo.Id;
        Vault vault = _rs.Create(newVault);
        newVault.Creator = userInfo; //Better way to Append Creator Onto Vault. Justin guided me!
        return Ok(newVault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault updateVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updateVault.CreatorId = userInfo.Id;
        updateVault.Id = id;
        Vault vault = _rs.Update(updateVault);
        updateVault.Creator = userInfo;
        return Ok(updateVault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _rs.Delete(id, userInfo.Id);
        return Ok("Vault Deleted");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}