using AutoMapper;
using BSAuth.Constants;
using BSAuth.Repositories;
using BSAuth.Repositories.Impl;
using BSAuth.Services;
using BSAuth.Services.Impl;
using BSAuth.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
/*
 * Configurations Section
 */
builder.Services.Configure<Security>(builder.Configuration.GetRequiredSection("Security"));

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
        opt.SerializerSettings.Formatting = Formatting.Indented;
        opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        opt.AllowInputFormatterExceptionMessages = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientLocationRepository, ClientLocationRepository>();
builder.Services.AddScoped<IClientTokenRepository, ClientTokenRepository>();

builder.Services.AddDbContext<CyberContext>((options) =>
{
    options.UseNpgsql(builder.Configuration["DbContext:ConnectionString"], options => options.UseNetTopologySuite());
});



var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
builder.Services.AddSingleton(mappingConfig.CreateMapper());

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(builder.Configuration["Security:Type"] ?? "Bearer").AddJwtBearer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope();
try
{
    var dataContext = scope.ServiceProvider.GetRequiredService<CyberContext>();
    dataContext.Database.Migrate();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
app.Run();