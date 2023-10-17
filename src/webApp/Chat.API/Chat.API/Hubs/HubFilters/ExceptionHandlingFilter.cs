using Entities.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs.HubFilters
{
    public class ExceptionHandlingFilter : IHubFilter
    {
        public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            Console.WriteLine($"Calling hub method '{invocationContext.HubMethodName}'");
            try
            {
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception calling '{invocationContext.HubMethodName}': {ex}");



                return ValueTask.CompletedTask;
            }
        }
    }
}
