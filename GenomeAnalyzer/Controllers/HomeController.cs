using System.Diagnostics;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GenomeAnalyzer.Models;
using GenomeAnalyzer.Services.Interfaces;

namespace GenomeAnalyzer.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        return View(_homeService.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGenomeViewModel model)
    {
        var response = await _homeService.Create(model);

        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetGenome(long id)
    {
        var response = await _homeService.GetGenome(id);

        return PartialView(response.Data);
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