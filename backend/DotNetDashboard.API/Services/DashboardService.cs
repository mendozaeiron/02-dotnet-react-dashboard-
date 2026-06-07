using DotNetDashboard.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetDashboard.API.Services;

public class DashboardService
{
    private readonly AppDbContext _context;
    
    public DashboardService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<DashboardMetricsResponse> GetDashboardMetrics()
    {
        var metrics = await _context.Metrics.ToDictionaryAsync(m => m.Name);
        
        return new DashboardMetricsResponse
        {
            ActiveUsers = new MetricCard
            {
                Title = "Usuarios Activos",
                Value = metrics.ContainsKey("ActiveUsers") ? metrics["ActiveUsers"].Value : 145,
                Unit = "usuarios",
                Trend = 12.5m,
                IsPositiveTrend = true
            },
            TotalRevenue = new MetricCard
            {
                Title = "Ingresos Totales",
                Value = metrics.ContainsKey("TotalRevenue") ? metrics["TotalRevenue"].Value : 25430,
                Unit = "USD",
                Trend = 8.3m,
                IsPositiveTrend = true
            },
            ConversionRate = new MetricCard
            {
                Title = "Tasa de Conversi?n",
                Value = metrics.ContainsKey("ConversionRate") ? metrics["ConversionRate"].Value : 23.5m,
                Unit = "%",
                Trend = 2.1m,
                IsPositiveTrend = true
            },
            PageLoadTime = new MetricCard
            {
                Title = "Tiempo de Carga",
                Value = metrics.ContainsKey("PageLoadTime") ? metrics["PageLoadTime"].Value : 1.2m,
                Unit = "s",
                Trend = -0.3m,
                IsPositiveTrend = true
            },
            RecentActivities = await GetRecentActivities(5),
            ChartData = await GetChartData("ActiveUsers", 30)
        };
    }
    
    public async Task<ChartData> GetChartData(string metricName, int days)
    {
        var random = new Random();
        var labels = Enumerable.Range(0, days).Select(i => DateTime.Now.AddDays(-i).ToString("MM-dd")).Reverse().ToList();
        var data = Enumerable.Range(0, days).Select(i => (decimal)random.Next(50, 200)).ToList();
        
        return new ChartData
        {
            Labels = labels,
            Datasets = new List<ChartDataset>
            {
                new ChartDataset
                {
                    Label = metricName,
                    Data = data,
                    BorderColor = "rgb(75, 192, 192)",
                    BackgroundColor = "rgba(75, 192, 192, 0.2)"
                }
            }
        };
    }
    
    public async Task<List<RecentActivity>> GetRecentActivities(int limit)
    {
        return new List<RecentActivity>
        {
            new RecentActivity { User = "admin", Action = "Login", Timestamp = DateTime.Now.AddMinutes(-5) },
            new RecentActivity { User = "demo", Action = "View Dashboard", Timestamp = DateTime.Now.AddMinutes(-15) },
            new RecentActivity { User = "admin", Action = "Export Report", Timestamp = DateTime.Now.AddHours(-1) },
            new RecentActivity { User = "demo", Action = "Update Profile", Timestamp = DateTime.Now.AddHours(-2) }
        }.Take(limit).ToList();
    }
}
