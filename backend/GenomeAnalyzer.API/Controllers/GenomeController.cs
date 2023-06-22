using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.ViewModels;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenomeAnalyzer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GenomeController : Controller
{
    private readonly IHomeService _homeService;

    public GenomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGenomeViewModel model)
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
        
        return Ok(response.Data);
    }
    
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] GenomeEntity entity)
    {
        var response = await _homeService.Edit(entity);
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        var response = await _homeService.Delete(id);
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }
}