using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using STUDY.ASP.ShiftLoggerTryThree.Data;
using STUDY.ASP.ShiftLoggerTryThree.Models;
using STUDY.ASP.ShiftLoggerTryThree.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IShiftLoggerService, ShiftLoggerService>();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Start ASP.NET Core application in a separate thread
Task.Run(() => StartWebAPI(app));

// CLI Functionality
Console.WriteLine("Web API starting... Press Enter to start Shift Logger CLI.");
Console.ReadLine();

StartShiftLoggerCLI();

void StartWebAPI(WebApplication app)
{
    app.Run();
}

void StartShiftLoggerCLI()
{
    // CLI Functionality
    using var httpClient = new HttpClient();
    const string ApiBaseUrl = "https://localhost:7188/api/shiftlogger";

    AnsiConsole.WriteLine("Welcome to Shift Logger CLI");

    while (true)
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices("View All Shift Logs", "Add Shift Log", "Quit"));

        switch (choice)
        {
            case "View All Shift Logs":
                ViewAllShiftLogs(httpClient, ApiBaseUrl);
                break;
            case "Add Shift Log":
                AddShiftLog(httpClient, ApiBaseUrl);
                break;
            case "Quit":
                Environment.Exit(0);
                break;
            default:
                AnsiConsole.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

void ViewAllShiftLogs(HttpClient httpClient, string apiBaseUrl)
{
    var shiftLogs = httpClient.GetFromJsonAsync<List<ShiftLogger>>(apiBaseUrl).Result;
    if (shiftLogs != null)
    {
        foreach (var shiftLog in shiftLogs)
        {
            AnsiConsole.WriteLine($"Id: {shiftLog.Id}, EmployeeId: {shiftLog.EmployeeId}, ClockIn: {shiftLog.ClockIn}, ClockOut: {shiftLog.ClockOut}");
        }
    }
    else
    {
        AnsiConsole.WriteLine("No shift logs found.");
    }
}

void AddShiftLog(HttpClient httpClient, string apiBaseUrl)
{
    var employeeId = AnsiConsole.Ask<int>("Enter Employee Id:");
    var clockIn = AnsiConsole.Ask<DateTime>("Enter Clock In time (yyyy-MM-dd HH:mm:ss):");
    var clockOut = AnsiConsole.Ask<DateTime>("Enter Clock Out time (yyyy-MM-dd HH:mm:ss):");

    var shiftLog = new ShiftLogger
    {
        EmployeeId = employeeId,
        ClockIn = clockIn,
        ClockOut = clockOut
    };

    var response = httpClient.PostAsJsonAsync(apiBaseUrl, shiftLog).Result;
    if (response.IsSuccessStatusCode)
    {
        AnsiConsole.WriteLine("Shift log added successfully.");
    }
    else
    {
        AnsiConsole.WriteLine("Failed to add shift log. Please try again.");
    }
}
