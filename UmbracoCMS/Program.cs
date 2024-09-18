using Microsoft.Extensions.Azure;
using UmbracoCMS.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Email-provider
builder.Services.AddScoped<EmailService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration["ServiceBus:ConnectionString"];
    var queueName = configuration["ServiceBus:QueueName"];

    return new EmailService(connectionString, queueName);
});
// ----- //

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
