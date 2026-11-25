using Microsoft.AspNetCore.Mvc;
using SampleApi.Api.Filters;

namespace SampleApi.Api.Attributes;

public class IdempotencyAttribute : TypeFilterAttribute
{
    public IdempotencyAttribute()
        : base(typeof(IdempotencyFilter))
    {
    }
}