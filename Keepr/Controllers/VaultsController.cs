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
    public class VaultsController : ControllerBase
    {
    private readonly VaultsService _rs;

    public VaultsController(VaultsService rs)
    {
      _rs = rs;
    }

    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
        try
        {
        Vault vault = _rs.Get(id);
        return Ok(vault);
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
  }
}