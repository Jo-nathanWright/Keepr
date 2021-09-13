using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
public class ProfilesController : ControllerBase
    {
    private readonly AccountService _acts;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(AccountService acts, KeepsService ks, VaultsService vs)
    {
      _acts = acts;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
        try
        {
        Profile prof = _acts.GetProfileById(id);
        return Ok(prof);
      }
        catch (Exception err)
        {
        throw new Exception(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _ks.GetKeepsByCreator(id);
        return keeps;
      }
      catch (Exception err)
        {
        throw new Exception(err.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaults(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vs.GetVaultsByCreator(id, userInfo);
        return vaults;
      }
      catch (Exception err)
        {
        throw new Exception(err.Message);
      }
    }
  }
}