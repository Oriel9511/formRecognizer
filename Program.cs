using Growth_Strategy_Form_Recognizer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// var blobEndpoint = new Uri(builder.Configuration["AzureBlobStorageEndpoint"]!);
var sasUrl = builder.Configuration.GetValue<string>("AzureBlobStorageSasUrl")!;
var fRSKey = builder.Configuration["AzureFormRecognizerServiceKey"]!;
var fRSEndpoint = builder.Configuration["AzureFormRecognizerServiceEndpoint"]!;
var modelName = builder.Configuration["ModelName"]!;
var patientModelName = builder.Configuration["PatientModel"]!;
var generalModelName = builder.Configuration["GeneralModel"]!;
var logsheetModelName = builder.Configuration["LogsheetModel"]!;
var prebuildCardModelName = builder.Configuration["PrebuiltCardModel"]!;
var inventoryLogsModelName = builder.Configuration["InventoryLogsModel"]!;
var containerName = builder.Configuration["ContainerName"]!;
builder.Services.AddSingleton<IDataStorage>(new DataStorage(sasUrl, containerName));
builder.Services.AddSingleton<IFormAnalysis>(new FormAnalysis( "EmployerModel",fRSKey, fRSEndpoint, modelName));
builder.Services.AddSingleton<IFormAnalysis>(new FormAnalysis( "PatientModel",fRSKey, fRSEndpoint, patientModelName));
builder.Services.AddSingleton<IFormAnalysis>(new FormAnalysis( "GeneralModel",fRSKey, fRSEndpoint, generalModelName));
builder.Services.AddSingleton<IFormAnalysis>(new FormAnalysis( "LogsheetModel",fRSKey, fRSEndpoint, logsheetModelName));
builder.Services.AddSingleton<IFormAnalysis>(new FormAnalysis( "PrebuiltCardModel",fRSKey, fRSEndpoint, prebuildCardModelName));
builder.Services.AddSingleton<IFormAnalysis>(new FormAnalysis( "InventoryLogsModel",fRSKey, fRSEndpoint, inventoryLogsModelName));

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
