using AdaptClientProjectApi.Models;
namespace AdaptClientProjectApi.Data.Repositories
{
	public interface IClientRepository
	{
		Task<bool> CreateClientAsync(Client client);
		Task<Dictionary<string, int>> GetNumberOfUsersPerLocationAsync();
		Task<int> GetNumberOfUsersOverallAsync();
		Task<Dictionary<DateOnly, int>> GetNumberOfClientsCreatedPerDateAsync();
		
	}
}
