using Infrastructure.Contexts;
using Microsoft.AspNetCore.Localization;
using Service.API;
using Service.API.SwaggerConfigs;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjections(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("*", x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
SwaggerConfigs.SwaggerBuilder(builder.Services);

var app = builder.Build();

var supportedCultures = new[] {
    new CultureInfo("pt-BR")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var readContext = scope.ServiceProvider.GetRequiredService<WriteContext>();
    readContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthyz").AllowAnonymous();

app.MapControllers();

app.Run();
