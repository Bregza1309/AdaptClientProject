using AdaptClientProjectApi.Models;
using Microsoft.EntityFrameworkCore;
using AdaptClientProjectApi.Data.Repositories;
using AdaptClientProjectApi.Data.SeedData;
using AdaptClientProjectApi.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add DbContext using "ClientsDbConnection" string in the appSettings as the MSSQL database string
builder.Services.AddDbContext<ClientDbContext>(opts =>
{
	opts.UseSqlServer(builder.Configuration.GetConnectionString("ClientsDbConnection"));
});

//add CRUD operations 'IClientRepository' as a scoped service
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//app.UseSwagger();
	//app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//register the extension methods to app configuration
app.MapGets()
	.MapPosts();
//ENSURE Database has Seed Data
await SeedData.EnsureSeededAsync(app);

app.Run();
