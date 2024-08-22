using AfterNoonV2.Application.Validators.Products;
using AfterNoonV2.Persistance;
using FluentValidation.AspNetCore;
using AfterNoonV2.Infrastructure.Filters;
using AfterNoonV2.Infrastructure;
using AfterNoonV2.Infrastructure.Services.Storage.Local;
using AfterNoonV2.SignalR;
using AfterNoonV2.Infrastructure.Services.Storage.Azure;
using AfterNoonV2.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Security.Claims;
using Serilog.Context;
using AfterNoonV2.WebApi.Extensions;
using AfterNoonV2.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

//Controllers with valitation
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<RolePermissionFilter>();
})
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


//Persistance services
builder.Services.AddServiceRegestration(builder.Configuration);

//Application services
builder.Services.AddApplicationService();

//SignalR services
builder.Services.AddSignalRService();

//Http context accessor
builder.Services.AddHttpContextAccessor();


Logger logerCfg = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    //.WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PSql"), "logs", needAutoCreateTable: true)
    .WriteTo.MongoDB(builder.Configuration.GetConnectionString("MongoDb")!, "log", LogEventLevel.Error)
    .Enrich.FromLogContext()
    .CreateLogger();



//Logging
builder.Host.UseSerilog(logerCfg);

//Infrastructure services
builder.Services.AddInfrastructureService();
builder.Services.AddStorage<LocalStorage>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", options =>
    {
        options.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});

//Authenticaton services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", op =>
{
    op.TokenValidationParameters = new()
    {
        ValidateAudience = true, // It is the value by which we determine who/which origins/sites will use the token value to be craeted.
        ValidateIssuer = true, // This is the field where we determine who distrubutes the token value to be created. 
        ValidateLifetime = true, // It is the vreifcation that will check the duration of the created token value.
        ValidateIssuerSigningKey = true, // Unique security key

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SigningKey"]!)),
        ClockSkew = TimeSpan.Zero,

        NameClaimType = ClaimTypes.Name
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseSerilogRequestLogging();


app.UseStaticFiles();

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();
app.AddHubResgtartion();

app.Run();
