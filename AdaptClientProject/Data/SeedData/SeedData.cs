using AdaptClientProjectApi.Models;
using Microsoft.EntityFrameworkCore;
namespace AdaptClientProjectApi.Data.SeedData
{
	public static class SeedData
	{
		public static async Task EnsureSeededAsync(WebApplication app)
		{
			//get the dbContext from the service container
			var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ClientDbContext>();

			if(context != null)
			{
				//check for any pending migrations
				if(context.Database.GetPendingMigrations().Count()  > 0)
				{
					await context.Database.MigrateAsync();
				}
				//check if any clients exist and create some dummy clients to make sure database is not empty 
				if(context.Clients.Count() == 0)
				{
					List<Client> dummyClients = [];
					string[] clientNames = ["Sam Holdings" , "Shoprite" , "Ackermans" , "Pep Store" , "Markham"];
					string[] locations = ["Johannesburg", "Pretoria", "Cape Town", "Johannesburg", "Durban"];
					int[] numberOfUsers = [14,17,10,13,20];

					//create the dummy clients using values above
					for(int i = 0; i < clientNames.Length; i++)
					{
						dummyClients.Add(new Client()
						{
							ClientName = clientNames[i],
							Location = locations[i],
							NumberOfUsers= numberOfUsers[i],
						});
					}
					//add created clients to the database to the database 
					await context.Clients.AddRangeAsync(dummyClients);
					await context.SaveChangesAsync();


				}

			}
			else
			{
				throw new Exception("Database Context is null ");
			}
		}
	}
}
