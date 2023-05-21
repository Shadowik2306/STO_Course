using STOBusinessLogic.BusinessLogics;
using STOContracts.BusinessLogicsContracts;
using STOContracts.StoragesContracts;
using STODatabaseImplement.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ICarStorage, CarStorage>();
builder.Services.AddTransient<IEmployerStorage, EmployerStorage>();
builder.Services.AddTransient<IMaintenanceStorage, MaintenanceStorage>();
builder.Services.AddTransient<IServiceStorage, ServiceStorage>();
builder.Services.AddTransient<ISpareStorage, SpareStorage>();
builder.Services.AddTransient<IStorekeeperStorage, StorekeeperStorage>();
builder.Services.AddTransient<IWorkDurationStorage, WorkDurationStorage>();
builder.Services.AddTransient<IWorkStorage, WorkStorage>();

builder.Services.AddTransient<ICarLogic, CarLogic>();
builder.Services.AddTransient<IEmployerLogic, EmployerLogic>();
builder.Services.AddTransient<IMaintenanceLogic, MaintenanceLogic>();
builder.Services.AddTransient<IServiceLogic, ServiceLogic>();
builder.Services.AddTransient<ISpareLogic, SpareLogic>();
builder.Services.AddTransient<IStorekeeperLogic, StorekeeperLogic>();
builder.Services.AddTransient<IWorkDurationLogic, WorkDurationLogic>();
builder.Services.AddTransient<IWorkLogic, WorkLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
