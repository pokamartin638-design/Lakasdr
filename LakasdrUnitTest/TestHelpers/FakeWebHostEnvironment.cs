using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Lakasdr.Tests.TestHelpers;

public class FakeWebHostEnvironment : IWebHostEnvironment
{
    public string ApplicationName { get; set; } = "Lakasdr.Tests";
    public IFileProvider WebRootFileProvider { get; set; } = null!;
    public string WebRootPath { get; set; } = Path.Combine(Path.GetTempPath(), "LakasdrTestsWebRoot");
    public string EnvironmentName { get; set; } = "Development";
    public string ContentRootPath { get; set; } = Path.GetTempPath();
    public IFileProvider ContentRootFileProvider { get; set; } = null!;
}
