using Health.Services;
using Health.Services.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Local"))
{
    // Use a stub if you don't have access to the EMPI API yet
    builder.Services.AddClientRegistryServiceStub();
}
else
{
    builder.Services.AddClientRegistryService(builder.Configuration.GetSection(ClientRegistrySettings.SectionName));
}

// Add services to the container.
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
