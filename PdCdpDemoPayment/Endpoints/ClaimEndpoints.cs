using PdCdpDemoPayment.Models;
using PdCdpDemoPayment.Services;
using FluentValidation;
using FluentValidation.Results;

namespace PdCdpDemoPayment.Endpoints;

public static class ClaimEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Claim";
    private const string BaseRoute = "claim";

    public static void UseClaimEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, CreateClaimAsync)
            .WithName("CreateClaim")
            .Accepts<Claim>(ContentType)
            .Produces<Claim>(201).Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/{{claimId}}", GetClaimByClaimIdAsync)
            .WithName("GetClaim")
            .Produces<Claim>().Produces(404)
            .WithTags(Tag);
    }

    private static async Task<IResult> CreateClaimAsync(
        Claim claim, IClaimService claimService, IValidator<Claim> validator)
    {
        var validationResult = await validator.ValidateAsync(claim);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var created = await claimService.CreateAsync(claim);
        if (!created)
            return Results.BadRequest(new List<ValidationFailure>
            {
                new("claimId", "A claim for this claimId already exists")
            });

        return Results.Created($"/{BaseRoute}/{claim.ClaimId}", claim);
    }

    private static async Task<IResult> GetClaimByClaimIdAsync(
        string claimId, IClaimService claimService)
    {
        var claim = await claimService.GetByClaimIdAsync(claimId);
        return claim is not null ? Results.Ok(claim) : Results.NotFound();
    }
}
