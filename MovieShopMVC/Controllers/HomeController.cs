using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly  IMovieService _movieService;

    // depend on higher level abstarction

    public HomeController(ILogger<HomeController> logger, IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
        // you want to have control over which implentation that you want to use
        // var homeController = new HomeController();   

    }

    public async Task< IActionResult> Index()
    {
        // T1
        // var homeController = new HomeController(new Lopgger(), );
        // home page
        // top 30 movies -> Movie Service
        // insatcne of MovieService class
        // newing up
        // refacor this code
        // var movieService = new MovieService();
       
        // I/O Bound operation, database over network, send the SQL and SQL wil be executed on DB
        // get back with results
        // T1 is waiting for this operation
        // 30 ms, 1 sec, 10 ,sec
        // 3 sec
        // for any I/O bound operation always perefer to use async/await pattern
        var movies = await _movieService.GetTopGrossingMovies();
        // 
        // method(int x, IMovieService service);

        // var movieService = new MovieService();
        // var movieService3 = new MovieTestService();

        // method(3, movieService3);

        // passing thr data from Controller/action method to the View
        return View(movies);
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

