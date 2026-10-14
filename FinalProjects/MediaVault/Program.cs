using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediaVault.Components;
using MediaVault.Components.Account;
using MediaVault.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// W06-01 & W06-02: Autenticación con Proveedores Externos (Google y GitHub)
var authBuilder = builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    });

// Configuración de Google OAuth
var googleClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "GOOGLE_CLIENT_ID_MOCK";
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "GOOGLE_CLIENT_SECRET_MOCK";

authBuilder.AddGoogle(options =>
{
    options.ClientId = googleClientId;
    options.ClientSecret = googleClientSecret;
});

// Configuración de GitHub OAuth
var gitHubClientId = builder.Configuration["Authentication:GitHub:ClientId"] ?? "GITHUB_CLIENT_ID_MOCK";
var gitHubClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"] ?? "GITHUB_CLIENT_SECRET_MOCK";

authBuilder.AddGitHub(options =>
{
    options.ClientId = gitHubClientId;
    options.ClientSecret = gitHubClientSecret;
});

authBuilder.AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=app.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();