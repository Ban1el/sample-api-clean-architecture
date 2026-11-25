using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace SampleApi.Api.Filters;

public class IdempotencyFilter : IAsyncActionFilter
{
    private readonly IMemoryCache _cache;

    private static readonly ConcurrentDictionary<Guid, SemaphoreSlim> Locks = new();

    public IdempotencyFilter(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        const string headerName = "Idempotency-Key";

        if (!context.HttpContext.Request.Headers.TryGetValue(
                headerName, out var headerValue)
            || !Guid.TryParse(headerValue, out var key))
        {
            context.Result = new BadRequestObjectResult(
                new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid Idempotency Key",
                    Detail = "A valid Idempotency-Key GUID is required."
                });

            return;
        }

        var cacheKey = $"idempotency:{key}";

        // Check if the request has already been processed.
        if (_cache.TryGetValue(cacheKey, out IActionResult? cachedResult)
            && cachedResult != null)
        {
            context.Result = cachedResult;
            return;
        }

        // Prevent simultaneous requests with the same key
        // from executing the action at the same time.
        var semaphore = Locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

        await semaphore.WaitAsync();

        try
        {
            // Check again after acquiring the lock.
            if (_cache.TryGetValue(cacheKey, out cachedResult)
                && cachedResult != null)
            {
                context.Result = cachedResult;
                return;
            }

            // Execute the controller action.
            var executedContext = await next();

            if (executedContext.Exception == null
                || executedContext.ExceptionHandled)
            {
                var result = executedContext.Result;

                if (result != null && IsSuccessfulResult(result))
                {
                    _cache.Set(
                        cacheKey,
                        result,
                        TimeSpan.FromMinutes(5)
                    );
                }
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    private static bool IsSuccessfulResult(IActionResult result)
    {
        var statusCode = result switch
        {
            ObjectResult objectResult =>
                objectResult.StatusCode ?? StatusCodes.Status200OK,

            StatusCodeResult statusCodeResult =>
                statusCodeResult.StatusCode,

            _ => StatusCodes.Status200OK
        };

        return statusCode >= 200 && statusCode < 300;
    }
}