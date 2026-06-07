namespace DotNetDashboard.API.Models;

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public UserInfo User { get; set; } = new();
}

public class RegisterRequest
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? FullName { get; set; }
}

public class UserInfo
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string Role { get; set; } = string.Empty;
}

public class DashboardMetricsResponse
{
    public MetricCard ActiveUsers { get; set; } = new();
    public MetricCard TotalRevenue { get; set; } = new();
    public MetricCard ConversionRate { get; set; } = new();
    public MetricCard PageLoadTime { get; set; } = new();
    public List<RecentActivity> RecentActivities { get; set; } = new();
    public ChartData ChartData { get; set; } = new();
}

public class MetricCard
{
    public string Title { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal Trend { get; set; }
    public bool IsPositiveTrend { get; set; }
}

public class RecentActivity
{
    public string User { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string? Details { get; set; }
}

public class ChartData
{
    public List<string> Labels { get; set; } = new();
    public List<ChartDataset> Datasets { get; set; } = new();
}

public class ChartDataset
{
    public string Label { get; set; } = string.Empty;
    public List<decimal> Data { get; set; } = new();
    public string BorderColor { get; set; } = string.Empty;
    public string BackgroundColor { get; set; } = string.Empty;
}
