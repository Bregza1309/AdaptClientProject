using DisplayClientAnalysisApp.Components;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

//add HttpClient for the communication with client Api
builder.Services.AddHttpClient(name: "Clients.WebApi",
  configureClient: options =>
  {
	  options.BaseAddress = new Uri("https://localhost:7013/");
	  options.DefaultRequestHeaders.Accept.Add(
		new MediaTypeWithQualityHeaderValue(
		mediaType: "application/json", quality: 1.0));
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
