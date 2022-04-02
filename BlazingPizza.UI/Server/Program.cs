using BlazingPizza.UI.Server.Data;
using BlazingPizza.UI.Server.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PizzaStoreContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Dev"));
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie().AddTwitter(TwitterOptions =>
{

    TwitterOptions.CallbackPath = "/";
    TwitterOptions.ConsumerKey = "8FIkPymD0wWvUkNAFPwiC0km2";
    TwitterOptions.ConsumerSecret = "ka9qXYOCcICOCiLoI76uwbMVoDEmhzA3TmK8N5iWg9GrjEWt37";
    TwitterOptions.Events.OnRemoteFailure = context =>
    {
        context.HandleResponse();
        return context.Response.WriteAsync("<script>window.close();</script>");
    };
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();