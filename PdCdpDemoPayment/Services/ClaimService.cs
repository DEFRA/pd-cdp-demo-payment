using PdCdpDemoPayment.Data;
using PdCdpDemoPayment.Models;
using MongoDB.Driver;

namespace PdCdpDemoPayment.Services;

public class ClaimService : MongoService<Claim>, IClaimService
{
    public ClaimService(IMongoDbClientFactory connectionFactory, ILoggerFactory loggerFactory) : base(connectionFactory,
        "claims", loggerFactory)
    {
    }

    public async Task<bool> CreateAsync(Claim claim)
    {
        var existingClaim = await GetByClaimIdAsync(claim.ClaimId);
        if (existingClaim is not null) return false;

        await Collection.InsertOneAsync(claim);
        return true;
    }

    public async Task<Claim?> GetByClaimIdAsync(string claimId)
    {
        var result = await Collection.Find(b => b.ClaimId == claimId).ToListAsync();
        return result?.FirstOrDefault();
    }

    protected override List<CreateIndexModel<Claim>> DefineIndexes(IndexKeysDefinitionBuilder<Claim> builder)
    {
        var options = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<Claim>(builder.Ascending(b => b.ClaimId), options);
        return new List<CreateIndexModel<Claim>> { indexModel };
    }
}
