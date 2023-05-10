using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // home page displaying 
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    // creating individual's arithmetic profile with data provided
    [HttpPost("/Home/Create")]
    public IActionResult CreateIndividual(Individual newPerson)
    {
        // checking if a Name was inputted
        if (newPerson.Name != null)
        {
                                            // Get    /    Set
            HttpContext.Session.SetString("PersonName", newPerson.Name);
            // declaring session for starting arbitrary Int point
            HttpContext.Session.SetInt32("PersonNumber", newPerson.Number);
        }
        // GETting Name's data
        string? sessionName = HttpContext.Session.GetString("PersonName");
        // GETting Number's data 
        int? sessionNumber = HttpContext.Session.GetInt32("PersonNumber");

        
        return View("Dashboard", newPerson);
    }

    // displaying individual's arithmetic profile 
    [HttpGet("/Dashboard")]
    public IActionResult Dashboard()
    {

        return View("Dashboard");
    }

    // this route handles the additiion data update(s)
    [HttpPost("/Home/Plus")]
    public IActionResult AddNumber()

    {
        int? sessionNumber = HttpContext.Session.GetInt32("PersonNumber");
        if (sessionNumber != null)
        {
            // updating the current Number's value 
            int incrementNumber = sessionNumber.Value + 1;
            // Setting the PersonNumber instance's value to incrementedNumber 
            HttpContext.Session.SetInt32("PersonNumber", incrementNumber);
            Console.WriteLine(incrementNumber);
        }
        // redirecting to dashboard to retain session count 
        return Redirect("/dashboard");
    }

    [HttpPost("/Home/Minus")]
    public IActionResult SubtractNumber()

    {
        int? sessionNumber = HttpContext.Session.GetInt32("PersonNumber");
        if (sessionNumber != null)
        {
            int incrementNumber = sessionNumber.Value - 1;
            HttpContext.Session.SetInt32("PersonNumber", incrementNumber);
            Console.WriteLine(incrementNumber);
        }

        return Redirect("/dashboard");
    }
    [HttpPost("/Home/Times")]
    public IActionResult Multiply()

    {
        int? sessionNumber = HttpContext.Session.GetInt32("PersonNumber");
        if (sessionNumber != null)
        {
            int incrementNumber = sessionNumber.Value * 2;
            HttpContext.Session.SetInt32("PersonNumber", incrementNumber);
            Console.WriteLine(incrementNumber);
        }

        return Redirect("/dashboard");
    }
    [HttpPost("/Home/Random")]
    public IActionResult AddRandomNumber()

    {
        int? sessionNumber = HttpContext.Session.GetInt32("PersonNumber");
        if (sessionNumber != null)
        {
            // Applying random to utilize the range between 1-10
            Random rand = new Random();
            int newRandom = rand.Next(1, 10);

            int incrementNumber = sessionNumber.Value + newRandom;
            HttpContext.Session.SetInt32("PersonNumber", incrementNumber);
            Console.WriteLine(incrementNumber);
        }

        return Redirect("/dashboard");
    }

    // session closure based on button's LogOut event 
    [HttpPost("/ClearSession")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        return Redirect("/");
    }

    // default 
    [HttpGet("/Home/Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
