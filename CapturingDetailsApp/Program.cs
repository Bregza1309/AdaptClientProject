using AspNetCoreHero.ToastNotification;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//add HttpClient for the communication with client Api
builder.Services.AddHttpClient(name: "Clients.WebApi",
  configureClient: options =>
  {
      options.BaseAddress = new Uri("https://localhost:7013/");
      options.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue(
        mediaType: "application/json", quality: 1.0));
  });
//add notification service
builder.Services.AddNotyf(opts =>
{
    opts.DurationInSeconds = 3;
    opts.IsDismissable = true;
    opts.Position = NotyfPosition.TopCenter;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
