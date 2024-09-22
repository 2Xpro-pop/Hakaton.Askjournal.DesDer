using System.Text.Json.Serialization;
using DesDer.Middlewares;
using DesDer.Extensions;
using DesDer.Models;
using DesDer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using DesDer.SpaAdmin.ViewModels;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("Smarter");

builder.Services.Configure<SupportedCultureOptions>( builder.Configuration.GetSection("SupportedCultureOptions"));
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<KostylTokenProvider>();
builder.Services.AddReact();

builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
    .AddV8();

//Validators
builder.Services.AddScoped<IValidator<UserViewModel>, UserViewModelValidator>();
builder.Services.AddScoped<IValidator<PostViewModel>, PostViewModelValidator>();
builder.Services.AddScoped<IValidator<RouteViewModel>, RouteViewModelValidator>();
builder.Services.AddScoped<IValidator<TableViewModel>, TableViewModelValidator>();

builder.Services.AddRazorPages(options =>
            {
                options.Conventions.Add(new CultureTemplatePageRouteModelConvention());
                options.Conventions.AuthorizePage("/DesDer");
            });
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddXssSecurity();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

builder.Services.AddIdentity<User, IdentityRole>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin";
});

builder.Services.AddTransient<VisitsSourceService>();
builder.Services.AddTransient<RoutesService>();

builder.Services.AddTransient<IUserService, UserServiceIdentity>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFileUploaderService, FileUploaderService>();
builder.Services.AddTransient<IPostAnalyticsService, PostAnalyticsService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IHeaderService, HeaderRestServices>();
builder.Services.AddTransient<IComponentDataProvider, ComponentDataProvider>();
builder.Services.AddTransient<ICustomTableService, CustomTableService>();

builder.Services.AddTransient<ISecuredRunner, MatToastSecuredRunner>();

builder.Services.AddScoped<IPostContainerService, PostContainerService>();


builder.Services.AddPostBlockConverters();
builder.Services.AddPostBuilder();

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization();

builder.Services.AddSpaAdmin();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

#if DEBUG

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endif

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseReact(config =>
{
    config
      .AddScript("~/React/Cards.jsx")
      .AddScript("~/React/Carousel.jsx")
      .AddScript("~/React/Header.jsx")
      .AddScript("~/React/Paragraph.jsx")
      .AddScript("~/React/PostBuilder.jsx")
      .AddScript("~/React/HeaderSubbar.jsx")
      .AddScript("~/React/Image.jsx")
      .AddScript("~/React/Table.jsx")
      .AddScript("~/React/Quote.jsx")
      .AddScript("~/React/BigCards.jsx")
      .AddScript("~/React/Button.jsx")
      .AddScript("~/React/Navmenu.jsx");

    //config.AllowJavaScriptPrecompilation = true;
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSpaAdmin();
app.UseDesDer2();

app.MapControllers();
app.UseCultureRedirect();


app.MapFallbackToPage("/{culture}/{*pattern:nonfile}", "/PostsView");

//DataSeeder
var services = app.Services.CreateAsyncScope();
await DataSeeder.InitializeAsync(
    services.ServiceProvider.GetRequiredService<UserManager<User>>(),
    services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>()
);

#if DEBUG
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endif

app.Run();