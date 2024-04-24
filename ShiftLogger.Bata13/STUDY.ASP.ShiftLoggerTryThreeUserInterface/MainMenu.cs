using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;
using STUDY.ASP.ShiftLoggerTryThree.Models;

namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface;

//add user input validation
//add no negative date validation
//add update and delete
//clear consoles and use table 


class MainMenu
{
    static HttpClient client = new HttpClient();
    const string ApiBaseUrl = "https://localhost:7188/api/shiftlogger"; 
    public static void ShowMainMenu()
    {
        // Your user interface logic goes here
        Console.WriteLine("Welcome to Shift Logger User Interface");

        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices(@"View All Shift Logs",
                        "View Specific Shift Log",
                        "Add Shift Log",
                        "Update Shift Log",
                        "Delete Shift Log",
                        "Quit"));

            switch (choice)
            {
                case "View All Shift Logs":
                    ProgramEngine.ViewAllShiftLogs(client, ApiBaseUrl);
                    break;
                case "View Specific Shift Log":
                    ProgramEngine.ViewSpecificShiftLog(client, ApiBaseUrl);
                    break;
                case "Add Shift Log":
                    ProgramEngine.AddShiftLog(client, ApiBaseUrl);
                    break;
                case "Update Shift Log":
                    ProgramEngine.UpdateShiftLog();
                    break;
                case "Delete Shift Log":
                    ProgramEngine.DeleteShiftLog();
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
}
