using Firebase.Api.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Firebase.Api.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
  public HasPermissionAttribute(PermisoEnum permiso): base(policy: permiso.ToString())
   {
   }

}