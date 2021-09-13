using System;
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

    public ProfilesController(AccountService acts)
    {
      _acts = acts;
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
  }
}