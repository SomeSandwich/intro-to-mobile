using System.Reflection;
using System.Text.Json.Serialization;
using API.App;
using API.App.Extensions;
using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using MMS.GateApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var env = builder.Environment;

builder.Services.ConfigureDatabase(config, env);
builder.Services.ConfigureMinio(config, env);
builder.Services.ConfigureVersion();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureJwt(config);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.RegisterServices();
builder.Services.RegisterModules();

// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

ApiVersionSet versionSet = app.NewApiVersionSet()
    .HasApiVersion(1)
    .HasApiVersion(2)
    .ReportApiVersions()
    .Build();

app.MapEndpoints(versionSet);

if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        var descs = app.DescribeApiVersions();
        foreach (var desc in descs)
        {
            var url = $"/swagger/{desc.GroupName}/swagger.json";
            var name = desc.GroupName.ToUpperInvariant();
            opt.SwaggerEndpoint(url, name);
        }
    });

    app.UseReDoc(opt =>
    {
        opt.DocumentTitle = "Swagger Demo Documentation";
        opt.SpecUrl = "/swagger/v1/swagger.json";

        var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(@"API.App.Redoc.CustomIndex.ReDoc.index.html");

        var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        Console.WriteLine("--- Resource Name ---");
        foreach (string name in resourceNames)
        {
            Console.WriteLine($"--- {name} ---");
        }

        Console.WriteLine($"""
                *********
                Stream is null {stream is null}
                *********
            """);


        opt.IndexStream = () =>
            stream;

        // options.IndexStream = () => Assembly.GetExecutingAssembly()
        //     .GetManifestResourceStream(
        //         "CustomIndex.ReDoc.index.html"); // requires file to be added as an embedded resource
    });
}


app.ConfigureErrorResponse();

// app.UseResponseParser();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
