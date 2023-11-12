using System.Reflection;
using Mapster;
using MapsterMapper;
using RegistrantApp.Server.BLL;
using RegistrantApp.Server.Database;
using RegistrantApp.Server.Database.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RaContext, LiteContext>();
builder.Services.AddSwaggerGen();
builder.Services.AddMapster();


/* Config adapters with controllers */
builder.Services.AddScoped<SecurityAdapter>();
builder.Services.AddScoped<AccountAdapter>();
builder.Services.AddScoped<AutoAdapter>();
builder.Services.AddScoped<ContragentsAdapter>();
builder.Services.AddScoped<DocumentAdapter>();
builder.Services.AddScoped<FilesAdapter>();
builder.Services.AddScoped<OrderDetailsAdapter>();
builder.Services.AddScoped<OrdersAdapter>();
builder.Services.AddScoped<AuditAdapter>();

builder.Configuration.AddJsonFile("Properties\\options.json")
    .AddJsonFile("Properties\\message.json");

/* MAPSTER CONFIGS */
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
// scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
// register the mapper as Singleton service for my application
var mapperConfig = new Mapper(typeAdapterConfig);

builder.Services.AddSingleton<IMapper>(mapperConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    /* Options Swagger*/
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();