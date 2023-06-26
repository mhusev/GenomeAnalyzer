using GenomeAnalyzer.Domain.DTO;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenomeAnalyzer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DistributionController : Controller
{
    private readonly IDistributionService _distributionService;

    public DistributionController(IDistributionService distributionService)
    {
        _distributionService = distributionService;
    }
    
    [HttpPut]
    public async Task<IActionResult> Distribute([FromBody] DistributionParamsDTO dto)
    {
        var response = await _distributionService.Distribute(dto);

        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(response);
        }
        
        return BadRequest(new { description = response.Description });
    }
}