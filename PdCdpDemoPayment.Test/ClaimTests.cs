using PdCdpDemoPayment.Models;
using PdCdpDemoPayment.Validators;
using MongoDB.Bson;

namespace PdCdpDemoPayment.Test;

using FluentValidation.TestHelper;

public class ClaimTests
{
    private ClaimValidator _validator = new ClaimValidator();
    
    [Fact]
    public void BookValidateIsbn()
    {
        var claim = new Claim()
        {
            Id = new ObjectId(),
            ClaimId = "invalid",
            Name = "John"
        };
        var result = _validator.TestValidate(claim);
        result.ShouldHaveValidationErrorFor(b => b.ClaimId);
    }
}
