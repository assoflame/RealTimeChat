using Chat.API.Extensions;
using Chat.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureDatabase();

builder.Services.ConfigureRepoManager();

builder.Services.ConfigureServiceManager();

builder.Services.ConfigureConnectionsCollection();

builder.Services.AddControllers();

builder.Services.ConfigureCors();

builder.Services.AddSignalR();

builder.Services.ConfigureJWT();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseRouting();

//app.UseEndpoints(
//    endpoints =>
//    {
//        endpoints.MapHub<ChatHub>("hubs/chat");
//    });

app.MapHub<ChatHub>("hubs/chat");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();
