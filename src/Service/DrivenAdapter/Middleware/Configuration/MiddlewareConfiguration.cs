namespace Service.DrivenAdapter.Middleware.Configuration;


public static class MiddlewareConfiguration
{
    public static void UseWriteUidMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<WriteUIdMiddleware>();
    }
}