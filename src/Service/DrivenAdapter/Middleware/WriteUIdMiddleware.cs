using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Service.DrivenAdapter.Middleware;

public class WriteUIdMiddleware
{
   private readonly RequestDelegate _next;

   public WriteUIdMiddleware(RequestDelegate next)
   {
      _next = next;
   }

   public async Task InvokeAsync(HttpContext context)
   {
      if (!context.Request.Query.ContainsKey("writeUId"))
      {
         context.Response.StatusCode = StatusCodes.Status400BadRequest;
         await context.Response.WriteAsync("writeUid parameter is required.");
         return;
      }

      string writeUIdValue = context.Request.Query["writeUId"].FirstOrDefault() ?? string.Empty;
      context.Items["writeUId"] = writeUIdValue;

      await _next(context);
   }
}