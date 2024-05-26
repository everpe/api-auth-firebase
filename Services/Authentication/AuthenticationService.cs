
using Firebase.Api.Models.Domain;
using Firebase.Api.Pagination;
using Firebase.Api.Vms;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using NetFirebase.Api.Data;
using NetFirebase.Api.Dtos.Login;
using NetFirebase.Api.Dtos.UsuarioRegister;
using NetFirebase.Api.Models;



namespace NetFirebase.Api.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly IPagedList _paginacion;

    private readonly DatabaseContext _context;
    public AuthenticationService(
                    HttpClient httpClient,
                    DatabaseContext context,
                    IPagedList paginacion
                    )
    {
        _httpClient = httpClient;
        _context = context;
        _paginacion = paginacion;
    }

    public async Task<PagedResults<Usuario>> GetPaginationVersion1(PaginationParams request)
    {
        var query = _context.Usuarios.Include(x => x.Roles)!.ThenInclude(x => x.Permisos);

        return await _paginacion.CreatePagedGenericResults<Usuario>(query,
             request.PageNumber,
             request.PageSize,
             request.OrderBy!,
             request.OrderAsc
         );

    }

    public async Task<PagedResults<UsuarioVm>> GetPaginationVersion2(PaginationParams request)
    {

        var query = _context.Database.SqlQuery<UsuarioVm>(@$"
                    SELECT 
                    usr.""Id"",
                    usr.""Email"",
                    usr.""FullName"",
                    string_agg(rol.""Name"", ',') as ""Role"",
                    string_agg(perm.""Nombre"", ',') as ""Permiso""
                    FROM ""Usuarios"" as usr
                    LEFT JOIN ""UsuarioRole"" as usrol
                        ON usr.""Id""=usrol.""UsuarioId""
                    LEFT JOIN ""Roles"" as rol
                        ON rol.""Id""=usrol.""RoleId""
                    LEFT JOIN ""RolePermiso"" as rolePermiso
                        ON rolePermiso.""RoleId"" = rol.""Id""
                    LEFT JOIN ""Permisos"" as perm
                        ON perm.""Id"" = rolePermiso.""PermisoId""
                    Group By usr.""Id""
                  ");

                return await _paginacion.CreatePagedGenericResults(
                    query,
                    request.PageNumber,
                    request.PageSize,
                    request.OrderBy!,
                    request.OrderAsc
                    );
    }

    public async Task<Usuario?> GetUserByEmail(string email)
    {
        return await _context.Usuarios.Where(x => x.Email == email).FirstOrDefaultAsync();
    }

    public async Task<string> LoginAsync(LoginRequestDto request)
    {
        var credentials = new
        {
            request.Email,
            request.Password,
            returnSecureToken = true,
        };

        var response = await _httpClient.PostAsJsonAsync("", credentials);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Credenciales erroneas");
        }


        var authFirebaseObject = await response.Content.ReadFromJsonAsync<AuthFirebase>();



        return authFirebaseObject!.IdToken!;
    }

    public async Task<string> RegisterAsync(UsuarioRegisterRequestDto request)
    {
        var userArgs = new UserRecordArgs
        {
            DisplayName = request.FullNombre,
            Email = request.Email,
            Password = request.Password
        };

        var usuario = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

        _context.Usuarios.Add(new Usuario
        {
            Email = request.Email,
            FullName = request.FullNombre,
            FirebaseId = usuario.Uid
        });

        await _context.SaveChangesAsync();


        return usuario.Uid;
    }
}