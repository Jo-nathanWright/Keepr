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
    public class KeepsController : ControllerBase
    {
    private readonly KeepsService _ks;

    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
        try
        {
        List<Keep> keep = _ks.Get();
        return Ok(keep);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
        try
        {
        Keep keep = _ks.Get(id);
        return Ok(keep);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep){
        try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.CreatorId = userInfo.Id;
        Keep created = _ks.Create(newKeep);
        return Ok(created);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep editedKeep)
    {
        try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editedKeep.CreatorId = userInfo.Id;
        editedKeep.Id = id;
        Keep keep = _ks.Update(editedKeep);
        return Ok(editedKeep);
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
        _ks.Delete(id, userInfo.Id);
        return Ok("Deleted Keep");
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
  }
}