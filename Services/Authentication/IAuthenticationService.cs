using Firebase.Api.Models.Domain;
using Firebase.Api.Pagination;
using Firebase.Api.Vms;
using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UsuarioRegister;


namespace NetFirebase.Api.Services.Authentication;

public interface IAuthenticationService
{
  Task<string> RegisterAsync(UsuarioRegisterRequestDto request);

  Task<string> LoginAsync(LoginRequestDto request);

  Task<Usuario?> GetUserByEmail(string email);

  Task<PagedResults<Usuario>> GetPaginationVersion1(PaginationParams request);

  Task<PagedResults<UsuarioVm>> GetPaginationVersion2(PaginationParams request);

 }