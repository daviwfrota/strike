using AutoMapper;
using CyberStrike.Repositories;
using CyberStrike.Repositories.Impl;
using CyberStrike.Services;
using CyberStrike.Services.Impl;
using CyberStrike.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddDbContext<CyberContext>((options) =>
{
    options.UseNpgsql(builder.Configuration["DbContext:ConnectionString"]);
});

var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
builder.Services.AddSingleton(mappingConfig.CreateMapper());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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