using PdCdpDemoPayment.Models;

namespace PdCdpDemoPayment.Services;

public interface IClaimService
{
    public Task<bool> CreateAsync(Claim book);

    public Task<Claim?> GetByClaimIdAsync(string claimId);
}
