using Microsoft.AspNetCore.Mvc;
using AdaptClientProjectApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using AdaptClientProjectApi.Data.Repositories;
using static Microsoft.AspNetCore.Http.StatusCodes;
namespace AdaptClientProjectApi.Extensions
{
	public static class WebApplicationExtensionsPost
	{
		public static WebApplication MapPosts(this WebApplication app)
		{
			//Post : api/clients 
			app.MapPost("/api/clients" , async Task<Results<Created,BadRequest<string>>> ([FromBody] Client client , [FromServices] IClientRepository clientRepository) =>
			{
				try
				{
					var result =  await clientRepository.CreateClientAsync(client);
					return result ? TypedResults.Created() : TypedResults.BadRequest<string>("Internal Server Error : Client not created");
				}catch(Exception ex)
				{
					return TypedResults.BadRequest<string>(ex.Message);
				}
			})
				.WithDescription("Adds new client")
				.WithOpenApi()
				.Produces(Status201Created)
				.Produces(Status400BadRequest);
			return app;
		}
	}
}
