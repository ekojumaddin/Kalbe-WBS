using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using WBSBE.BussLogic.Custom;
using WBSBE.Common.ConfigurationModel;
using WBSBE.Common.Library;
using WBSBE.Common.Library.Interface;
using WBSBE.Library;
using WBSBE.Library.SwaggerAttribute;
using static KN2021_GlobalClient_NetCore.clsGlobalWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.ConfigureLoggerService();
builder.Services.AddControllers();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<AppConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<WSOHelperModel>(builder.Configuration.GetSection("WSOHelperConfiguration"));
builder.Services.Configure<RedisConfigAPI>(builder.Configuration.GetSection("RedisSettings"));

//config untuk global API
builder.Services.Configure<AppSettingsAPI>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<WSOHelperModelAPI>(builder.Configuration.GetSection("WSOHelperConfiguration"));

var redis = builder.Configuration.GetSection("RedisSettings").Get<RedisConfigAPI>();
//REDIS
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
    ConnectionMultiplexer.Connect(ClsRijndael.Decrypt(redis.RedisConnection)));
builder.Services.AddSingleton<ICacheService, ClsRedisCacheServices>();

// configure DI for application services             
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();// untuk inject terkait kebutuhan get value httpcontext di clsglobalclass
builder.Services.AddSingleton<ClsUserKnGlobalBl>();

builder.Services.AddControllers().AddNewtonsoftJson(
                 options =>
                 {
                     options.UseMemberCasing();
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 });

builder.Services.AddSwaggerGen(options =>
{
    // add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();

    options.OperationFilter<AddRequiredHeaderParameter>();
    options.ExampleFilters();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.ExampleFilters();

    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }
        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }
        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    options.DocInclusionPredicate((name, api) => true);

    //custom operation Id
    options.CustomOperationIds(id => $"{id.ActionDescriptor.RouteValues["controller"]}_{id.ActionDescriptor.RouteValues["action"]}");
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Default_Example>();

//api versioning
builder.Services.AddApiVersioning(c =>
{
    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
    c.ReportApiVersions = true;
    c.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddVersionedApiExplorer(c =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    c.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    c.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

IWebHostEnvironment env = builder.Environment;

var app = builder.Build();

IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//Custom error 
app.ConfigureCustomExceptionMiddleware();

app.UseAntiXssMiddleware();

//Injection To dlogin
ClsGlobalClass.Configure(app.Services.GetRequiredService<IHttpContextAccessor>(), env);
ClsGlobalClass.ConfigureCache(app.Services.GetRequiredService<ICacheService>());
AppServicesHelper.Services = app.Services;

//Injection to Global Api
clsGlobalClassAPI.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
AppServicesHelperAPI.Services = app.Services;

// custom jwt auth middleware
app.UseMiddleware<TokenHandlerMiddleware>();

var pathData = Path.GetFullPath("~/Data").Replace("~\\", "");
if (!Directory.Exists(pathData))
{
    Directory.CreateDirectory(pathData);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(env.ContentRootPath, "Data")),
    RequestPath = "/Data"
});


//if (env.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KN2022_DeskBookingBE v1"));
//} 

//app.UseAuthorization();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // build a swagger endpoint for each discovered API version
    foreach (var description in provider.ApiVersionDescriptions)
    {
        string title = "WBS API ";
        c.SwaggerEndpoint($"{description.GroupName}/swagger.json", title + description.GroupName.ToUpperInvariant());
    }
    //c.SwaggerEndpoint("v1/swagger.json", "ORACLE API v1");
});

//app.UseAuthorization();

app.MapControllers();

app.Run();
