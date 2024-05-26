using Firebase.Api.Models.Domain;
using Firebase.Api.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UsuarioRegister;
using NetFirebase.Api.Services.Authentication;

namespace NetFirebase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public UsuarioController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(
        [FromBody] UsuarioRegisterRequestDto request
    )
    {
        return await _authenticationService.RegisterAsync(request);
    }

  [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] LoginRequestDto request
    )
    {
        return await _authenticationService.LoginAsync(request);
    }

  

    [AllowAnonymous]
    [HttpGet("paginationv1")]
    public async Task<ActionResult<PagedResults<Usuario>>> GetPaginationV1(
        [FromQuery] PaginationParams paginationQuery
    )
    {
        var resultados = await _authenticationService.GetPaginationVersion1(paginationQuery);
        return Ok(resultados);
    }

    [AllowAnonymous]
    [HttpGet("paginationv2")]
    public async Task<ActionResult<PagedResults<Usuario>>> GetPaginationV2(
        [FromQuery] PaginationParams paginationQuery
    )
    {
        var resultados = await _authenticationService.GetPaginationVersion2(paginationQuery);
        return Ok(resultados);
    }


}