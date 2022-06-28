using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CsArtemis.Models;

namespace CsArtemis.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MessageProducer _producer;

    public HomeController(ILogger<HomeController> logger, MessageProducer producer)
    {
        _logger = logger;
        _producer = producer;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async void Send(){
        await _producer.SendAsync<string>("Hello world!");
    }

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
