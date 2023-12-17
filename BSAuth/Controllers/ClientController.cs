using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using BSAuth.Errors;
using BSAuth.Models.DTO.Clients;
using BSAuth.Models.DTO.Login;
using BSAuth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace BSAuth.Controllers;
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
    
    [HttpPost("active")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]

    // [Authorize(policy: Roles.ADMIN)]
    public ActionResult<Response> Activate([Required]string token)
    {
        try
        {
            return StatusCode(StatusCodes.Status200OK, _clientService.ActiveAccount(token));
        }
        catch (BadRequestException e)
        {
            return BadRequest(e);
        }
    }
}