using AdaptClientProjectApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdaptClientProjectApi.Data.Repositories
{
	public class ClientRepository(ClientDbContext clientDbContext) : IClientRepository
	{
		readonly ClientDbContext context = clientDbContext;
		public async Task<bool> CreateClientAsync(Client client)
		{
			//check if client with same name exist
			var existingClient = context.Clients.SingleOrDefault(c => c.ClientName.ToLower() == client.ClientName.ToLower());
			if (existingClient != null)
			{
				throw new Exception("Client with same name already exist");
			}
			//add client
			await context.Clients.AddAsync(client);
			int affected = await context.SaveChangesAsync();
			return affected == 1;
			
		}

		public async Task<Dictionary<DateOnly,int>> GetNumberOfClientsCreatedPerDateAsync()
		{
			Dictionary<DateOnly, int> result = new Dictionary<DateOnly, int>();
			//get all dates where Clients where created
			var creationDates = await context.Clients
				.Select(c => DateOnly.FromDateTime(c.DateRegistered)).Distinct().ToListAsync();
			
			foreach(var filterDate in creationDates)
			{
				//getAll clients created at a particualer date
				var clients = await context.Clients
					.Where(c => DateOnly.FromDateTime(c.DateRegistered) == filterDate).ToListAsync();
				//add the DateOnly as  key and Client count as value 
				result.Add(filterDate, clients.Count);
			}
			return result;
		}

		public async Task<int> GetNumberOfUsersOverallAsync()
		{
			var numOfusers = context.Clients.Sum(c => c.NumberOfUsers);
			return await Task.FromResult(numOfusers);
		}

		public  async Task<Dictionary<string,int>> GetNumberOfUsersPerLocationAsync()
		{
			Dictionary<string, int> result = new();
			var locations = await context.Clients.Select(c => c.Location).Distinct().ToListAsync();
			foreach (var location in locations)
			{
				//get number of users per location  
				var numOfUsersPerLocation = context.Clients
					.Where(c => c.Location == location)
					.Sum(c => c.NumberOfUsers);
				//store the key value pairs in the result dictionary 
				result.Add(location, numOfUsersPerLocation);
			}
			return await Task.FromResult(result);


		}
	}
}
