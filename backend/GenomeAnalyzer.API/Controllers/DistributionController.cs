using GenomeAnalyzer.Domain.Distribution;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenomeAnalyzer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DistributionController : Controller
{
    private readonly IHomeService _homeService;

    public DistributionController(IHomeService homeService)
    {
        _homeService = homeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Distribute(long id)
    {
        var response = await _homeService.Distribute(id);

        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return Ok(response.Data);
        }
        
        return BadRequest(new { description = response.Description });
    }
    
    [HttpPut]
    public async Task<IBaseResponse<DistributionData>> GetDistributionData([FromBody] DistributionParams distributionParams)
    {
        return await _homeService.GetDistributionData(distributionParams);
    }
}