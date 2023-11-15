using System.Net.Mime;
using CyberStrike.Errors;
using CyberStrike.Models.DTO;
using CyberStrike.Services;
using Microsoft.AspNetCore.Mvc;

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
    private IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
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
}