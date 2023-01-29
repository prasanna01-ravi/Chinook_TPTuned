using AutoMapper;
using Chinook;
using Chinook.Areas.Identity;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Models;
using Chinook.Services.Implementation;
using Chinook.Services.Interface;
using Chinook.Services.Mapping;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ChinookContext>(opt => opt.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ChinookUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ChinookContext>();

builder.Services.AddSingleton((provider) => new MapperConfiguration(cfg => cfg.AddProfile(new TransformationDataMappingProfile())).CreateMapper());

builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>((provider) => 
    new PlaylistRepository(provider.GetService<ChinookContext>().Set<Playlist>()));
builder.Services.AddScoped<IPlaylistService, PlaylistService>();

builder.Services.AddScoped<IUserPlaylistRepository, UserPlaylistRepository>((provider) => 
    new UserPlaylistRepository(provider.GetService<ChinookContext>().Set<UserPlaylist>()));
builder.Services.AddScoped<IUserPlaylistService, UserPlaylistService>();

builder.Services.AddScoped<IBaseRepository<Track>, BaseRepository<Track>>((provider) =>
            new BaseRepository<Track>(provider.GetService<ChinookContext>().Set<Track>()));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>((provider) => new UnitOfWork(provider.GetService<ChinookContext>()));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ChinookUser>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ChinookContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
