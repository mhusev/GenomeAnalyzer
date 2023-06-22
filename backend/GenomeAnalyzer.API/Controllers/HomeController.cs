using GenomeAnalyzer.Domain.Distribution;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Domain.ViewModels;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenomeAnalyzer.API.Controllers;


public class HomeController : Controller
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        return Ok(_homeService.GetAll());
    }
}