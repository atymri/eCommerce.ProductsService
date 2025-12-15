using eCommerce.API.Endpoints;
using eCommerce.API.Middlewares;
using eCommerce.BusinessLogicLayer;
using eCommerce.DataAccessLayer;
using Org.BouncyCastle.Pkix;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

// enabling model binder to read values from json to enum
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// add swaggerr
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        var origins = builder.Configuration.GetSection("CORS:Origins").Get<string[]>();
        policy.WithOrigins(origins ?? Array.Empty<string>())
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandlingMiddlewarre();

app.UseRouting();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();

app.AddProductEndpoints();

app.Run();
