namespace Systore.Repositories.Interfaces;

public interface IReleaseRepository
{
    Task<bool> VerifyRelease(string clientId);
}