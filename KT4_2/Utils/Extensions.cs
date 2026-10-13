using Application.Utils;
using Mapster;
using System.Security.Claims;

namespace WebApi.Utils
{
    public static class Extensions
    {
        public static ResponseContract<T> ToDto<T>(this object obj)
        {
            return obj.Adapt<ResponseContract<T>>();
        }

        public static PaginationContract<T> ToPagination<T>(this object obj)
        {
            return obj.Adapt<PaginationContract<T>>();
        }


        public static Guid GetId(this ClaimsPrincipal claim)
        {
            Guid.TryParse(claim.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid UserId);

            return UserId;
        }
    }
}
