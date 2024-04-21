using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Spectre.Console;
using System.Collections.Generic;
using STUDY.ASP.ShiftLoggerTryThree.Models;

class Program
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string ApiBaseUrl = "https://localhost:7188/api";

    static async Task Main(string[] args)
    {
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
                    await ViewAllShiftLogs();
                    break;
                case "Add Shift Log":
                    await AddShiftLog();
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

    private static async Task ViewAllShiftLogs()
    {
        var shiftLogs = await _httpClient.GetFromJsonAsync<List<ShiftLogger>>(ApiBaseUrl + "/ShiftLogger");
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

    private static async Task AddShiftLog()
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

        var response = await _httpClient.PostAsJsonAsync(ApiBaseUrl + "/ShiftLogger", shiftLog);
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.WriteLine("Shift log added successfully.");
        }
        else
        {
            AnsiConsole.WriteLine("Failed to add shift log. Please try again.");
        }
    }
}
