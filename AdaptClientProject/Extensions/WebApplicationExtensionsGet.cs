using Microsoft.AspNetCore.Mvc;
using AdaptClientProjectApi.Models;
using AdaptClientProjectApi.Data.Repositories;
using static Microsoft.AspNetCore.Http.StatusCodes;//for StatusCode200Ok , etc
namespace AdaptClientProjectApi.Extensions
{
	public static class WebApplicationExtensionsGet
	{
		public static WebApplication MapGets(this WebApplication app)
		{
			//Get : /api/clientsCreatedPerDate
			app.MapGet("/api/clientsCreatedPerDate",async Task<Dictionary<DateOnly, int>> ([FromServices] IClientRepository clientRepository) =>
			{
				var result = await clientRepository.GetNumberOfClientsCreatedPerDateAsync();
				return result;
			})
				.WithDescription("Get Clients Created Per Date")
				.WithOpenApi()
				.Produces<Dictionary<DateOnly, int>>(Status200OK);

			//Get /api/numberOfUsersPerLocation
			app.MapGet("/api/numberOfUsersPerLocation", async Task<Dictionary<string , int>> ([FromServices] IClientRepository clientRepository) =>
			{
				var result = await clientRepository.GetNumberOfUsersPerLocationAsync();
				return result;
			})
				.WithDescription("Get Number of Users Per Location")
				.WithOpenApi()
				.Produces<Dictionary<string,int>>(Status200OK);

			//Get /api/numberOfUsersOverall
			app.MapGet("/api/numberOfUsersOverall" , async Task<int> ([FromServices] IClientRepository clientRepository) =>
			{
				var result = await clientRepository.GetNumberOfUsersOverallAsync();
				return result;
			})
				.WithDescription("Get Number of Users overally across all clients")
				.WithOpenApi()
				.Produces<int>(Status200OK);
			return app;
		}
	}
}
