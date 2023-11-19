using System.Net.Mime;
using CyberStrike.Errors;
using CyberStrike.Models.DTO.Clients;
using CyberStrike.Models.DTO.Login;
using CyberStrike.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CyberStrike.Controllers;
/// <summary>
/// Controls operations over account profile data.
/// </summary>
[Route("clients")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    // [Authorize(policy: Roles.ADMIN)]
    public ActionResult<ClientDto> Create(ClientDto client)
    {
        try
        {
            return StatusCode(StatusCodes.Status201Created, _clientService.Save(client));
        }
        catch (BadRequestException e)
        {
            return BadRequest(e);
        }
    }
    
    
    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]

    // [Authorize(policy: Roles.ADMIN)]
    public ActionResult<Response> Login(Request request)
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, _clientService.Login(request));
        }
        catch (BadRequestException e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPost("refresh-token")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]

    // [Authorize(policy: Roles.ADMIN)]
    public ActionResult<Response> Refresh(RefreshTokenDto refresh)
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, _clientService.RefreshToken(refresh));
        }
        catch (BadRequestException e)
        {
            return BadRequest(e);
        }
    }
}