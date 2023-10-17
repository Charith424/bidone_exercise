using CustomerInfo.Filters;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Added Global Exception filter
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
//Inject file location via DI
builder.Services.AddScoped<ICustomerRepository>(provider =>
{
    var fileLocation = builder.Configuration.GetSection("FileLocation").Value;
    return new CustomerDetailsFromFile(fileLocation);
});
//builder.Services.AddScoped<ICustomerRepository, CustomerDetailsFromFile>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //Configure different Error Page
    app.UseExceptionHandler("/Error/Index");    
    app.UseHsts();
}
// Configure the custom 404 page
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");

app.Run();

