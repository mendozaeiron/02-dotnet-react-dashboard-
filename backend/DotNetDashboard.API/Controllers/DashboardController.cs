using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetDashboard.API.Services;
using DotNetDashboard.API.Models;

namespace DotNetDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _dashboardService;
    
    public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }
    
    [HttpGet("metrics")]
    public async Task<IActionResult> GetMetrics()
    {
        var metrics = await _dashboardService.GetDashboardMetrics();
        return Ok(metrics);
    }
}
