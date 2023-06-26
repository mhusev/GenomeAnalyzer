using GenomeAnalyzer.Domain.DTO;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenomeAnalyzer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GenomeController : Controller
{
    private readonly IGenomeService _genomeService;

    public GenomeController(IGenomeService genomeService)
    {
        _genomeService = genomeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGenomeDTO dto)
    {
        var response = await _genomeService.Create(dto);

        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetGenome(long id)
    {
        var response = await _genomeService.Get(id);
        
        return Ok(response.Data);
    }
    
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] GenomeEntity entity)
    {
        var response = await _genomeService.Edit(entity);
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        var response = await _genomeService.Delete(id);
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(new { description = response.Description });
        }

        return BadRequest(new { description = response.Description });
    }
}