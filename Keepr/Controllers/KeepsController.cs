using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
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
    public ActionResult<List<Keep>> Get(){
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
  }
}