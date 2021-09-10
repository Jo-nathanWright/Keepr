using System;
using Keepr.Models;
using Keepr.Services;
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
  }
}