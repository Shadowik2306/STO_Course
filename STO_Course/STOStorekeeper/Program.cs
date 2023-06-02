using STOBusinessLogic.BusinessLogics;
using STOBusinessLogic.OfficePackage.Implements;
using STOBusinessLogic.OfficePackage;
using STOContracts.BusinessLogicsContracts;
using STOContracts.StoragesContracts;
using STODatabaseImplement.Implements;
using STOBusinessLogic.Mail;
using STOContracts.BindingModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<MailWorker>();

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
builder.Services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
builder.Services.AddTransient<AbstractSaveToWord, SaveToWord>();
builder.Services.AddTransient<AbstractSaveToPdf, SaveToPdf>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

var mailSender = app.Services.GetService<MailWorker>();
mailSender?.MailConfig(new MailConfigBindingModel
{
    MailLogin = builder.Configuration?.GetSection("MailLogin")?.Value?.ToString() ?? string.Empty,
    MailPassword = builder.Configuration?.GetSection("MailPassword")?.Value?.ToString() ?? string.Empty,
    SmtpClientHost = builder.Configuration?.GetSection("SmtpClientHost")?.Value?.ToString() ?? string.Empty,
    SmtpClientPort = Convert.ToInt32(builder.Configuration?.GetSection("SmtpClientPort")?.Value?.ToString()),
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=IndexWork}/{id?}");

app.Run();