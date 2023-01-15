using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Rewrite;
using QEnergy.API;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseSession();

app.Use(async (context, next) =>
{
    await next();
    var bearerAuth = context.Request.Headers["Authorization"]
        .FirstOrDefault()?.StartsWith("Bearer ") ?? false;

    if (context.Response.StatusCode == StatusCodes.Status401Unauthorized && !context.User.Identity.IsAuthenticated && !bearerAuth)
        await context.ChallengeAsync("smart");
});

app.UseAuthorization();

var option = new RewriteOptions();
option.AddRedirect("^$", "Projects/List");
app.UseRewriter(option);
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=index}/{id?}");
});

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
    //c.RoutePrefix = null;
    c.DocExpansion(DocExpansion.None);
});

app.Run();

