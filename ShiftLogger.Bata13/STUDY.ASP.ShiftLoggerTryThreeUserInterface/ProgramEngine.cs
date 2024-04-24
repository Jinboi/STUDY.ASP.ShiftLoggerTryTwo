using Spectre.Console;
using STUDY.ASP.ShiftLoggerTryThree.Models;
using System.Net.Http.Json;

namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface;
internal class ProgramEngine
{
    public static void ViewAllShiftLogs(HttpClient client, string ApiBaseUrl)
    {
        var shiftLogs = client.GetFromJsonAsync<ShiftLogger[]>(ApiBaseUrl).Result;
        if (shiftLogs != null)
        {
            foreach (var shiftLog in shiftLogs)
            {
                TimeSpan duration = shiftLog.ClockOut - shiftLog.ClockIn;
                AnsiConsole.WriteLine($@"
                    Id: {shiftLog.Id}, 
                    EmployeeFirstName: {shiftLog.EmployeeFirstName},
                    EmployeeLastName: {shiftLog.EmployeeLastName}, 
                    ClockIn: {shiftLog.ClockIn}, 
                    ClockOut: {shiftLog.ClockOut}, 
                    Duration: {duration}");
            }
        }
        else
        {
            AnsiConsole.WriteLine("No shift logs found.");
        }

        Helper.ReturnToMainMenu();
    }   
    public static void ViewSpecificShiftLog(HttpClient client, string ApiBaseUrl)
    {
        var id = AnsiConsole.Ask<string>("Enter Shift Log Id:");
        var specificShiftApiUrl = $"{ApiBaseUrl}/{id}";
        var shiftLog = client.GetFromJsonAsync<ShiftLogger>(specificShiftApiUrl).Result;

        if (shiftLog != null)
        {
            TimeSpan duration = shiftLog.ClockOut - shiftLog.ClockIn;
            AnsiConsole.WriteLine($@"
                Id: {shiftLog.Id}, 
                EmployeeFirstName: {shiftLog.EmployeeFirstName}, 
                EmployeeLastName: {shiftLog.EmployeeLastName}, 
                ClockIn: {shiftLog.ClockIn}, 
                ClockOut: {shiftLog.ClockOut}, 
                Duration: {duration}");
        }
        else
        {
            AnsiConsole.WriteLine($"Shift log with Id {id} not found.");
        }

        Helper.ReturnToMainMenu();
    }

    public static void AddShiftLog(HttpClient client, string ApiBaseUrl)
    {
        var employeeFirstName = AnsiConsole.Ask<string>("Enter Employee FirstName:");
        var employeeLastName = AnsiConsole.Ask<string>("Enter Employee LastName:");
        var clockIn = AnsiConsole.Ask<DateTime>("Enter Clock In time (yyyy-MM-dd HH:mm:ss):");
        var clockOut = AnsiConsole.Ask<DateTime>("Enter Clock Out time (yyyy-MM-dd HH:mm:ss):");

        var shiftLog = new ShiftLogger
        {
            EmployeeFirstName = employeeFirstName,
            EmployeeLastName = employeeLastName,
            ClockIn = clockIn,
            ClockOut = clockOut
        };

        var response = client.PostAsJsonAsync(ApiBaseUrl, shiftLog).Result;
        if (response.IsSuccessStatusCode)
        {
            AnsiConsole.WriteLine("Shift log added successfully.");
        }
        else
        {
            AnsiConsole.WriteLine("Failed to add shift log. Please try again.");
        }

        Helper.ReturnToMainMenu();
    }

    public static void UpdateShiftLog()
    {
        throw new NotImplementedException();
    }

    public static void DeleteShiftLog()
    {
        throw new NotImplementedException();
    }
}
