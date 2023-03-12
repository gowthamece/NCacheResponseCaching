using Alachisoft.NCache.Caching.Distributed;
using NCacheResponseCaching;
//using Alachisoft.NCache.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Extended.Meta;
using System.Configuration;
using Alachisoft.NCache.ResponseCaching;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddResponseCaching();

builder.Services.AddScoped<IHotelService, HotelService>();
//var cacheId=builder.Configuration["NCacheSettings:CacheName"];
//var cacheId = builder.Configuration.GetSection("NCacheSettings").GetSection("CacheName");

//NCacheConfiguration.Configure(cacheId.ToString(),DependencyType.SqlServer);
//NCacheConfiguration.ConfigureLogger();

builder.Services.AddNCacheDistributedCache(builder.Configuration.GetSection("NCacheSettings"));

builder.Services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("HotelConnectionString")));

//Read NCache specific configurations from appsettings.json

//builder.Services.AddOptions()
//                .Configure<NCacheConfiguration>(builder.Configuration.GetSection("NCacheSettings"));
//builder.Services.AddNCacheResponseCachingServices();

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
app.UseResponseCaching();
app.Use(async (ctx, next) =>
{
    if (ctx.Request.Method.Equals(HttpMethod.Get))
    {
        ctx.Response.GetTypedHeaders().CacheControl =
        new CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(10)
        };
        ctx.Response.Headers[HeaderNames.Vary] =
            new string[] { "Accept-Encoding" };
    }

    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
