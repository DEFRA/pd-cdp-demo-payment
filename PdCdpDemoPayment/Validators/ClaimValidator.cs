using PdCdpDemoPayment.Models;
using FluentValidation;

namespace PdCdpDemoPayment.Validators;

public class ClaimValidator : AbstractValidator<Claim>
{
    public ClaimValidator()
    {
        RuleFor(claim => claim.ClaimId)
            .Matches(@"^MINE\d{3}$")
            .WithMessage("Value was not a valid claimId");

        RuleFor(claim => claim.Name).NotEmpty();
    }
}
