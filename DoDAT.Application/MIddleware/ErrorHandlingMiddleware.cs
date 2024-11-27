using Microsoft.AspNetCore.Http;

namespace DoDAT.Application.MIddleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            //catch (DbUpdateException ex)
            //{
            //    Console.WriteLine(ex.ToString());

            //    context.Response.StatusCode = 400;
            //    await context.Response.WriteAsync("Database update failed: " + ex.Message);
            //}
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.ToString());

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("An error occurred: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occurred: " + ex.Message);
            }
        }
    }
}
