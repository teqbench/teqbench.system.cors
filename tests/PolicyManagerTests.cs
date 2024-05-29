using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace TeqBench.System.Cors.Tests;

[TestClass]
public class PolicyManagerTests
{
    public interface ITestServiceToMock
    {
        string TestName { get; set; }
    }

    private Dictionary<string, string?> _inMemorySettings = new Dictionary<string, string?>();
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private IConfiguration _configuration;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [TestInitialize]
    public void Setup()
    {
        //Arrange
        this._inMemorySettings = new Dictionary<string, string?> {
            {"corsPolicies:0:policyName", "TestCorsPolicy"},
            {"corsPolicies:0:allowedOrigins:0", "http://localhost:4200"},
            {"corsPolicies:0:allowedOrigins:1", "http://localhost:4201"},
            {"corsPolicies:0:allowCredentials", "true"}
        };

        this._configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(this._inMemorySettings)
            .Build();
    }

    [TestMethod]
    public void TestConstructor()
    {
        PolicyManager policyManager = new(this._configuration, "corsPolicies");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestConstructor1()
    {
        PolicyManager policyManager = new(this._configuration, "");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestConstructor2()
    {
        PolicyManager policyManager = new(this._configuration, " ");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestConstructor3()
    {
        PolicyManager policyManager = new(this._configuration, null!);
    }

    [TestMethod]
    public void TestBuildPolicies1()
    {
        // Arrange
        Dictionary<string, string?> settings = new Dictionary<string, string?>();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        IServiceCollection services = new ServiceCollection();
        PolicyManager policyManager = new(configuration, "corsPolicies");
        var mock = new Mock<ITestServiceToMock>();
        services.AddSingleton<ITestServiceToMock>(mock.Object);

        // Act
        policyManager.BuildPolicies(services);

        // Assert
        Assert.IsTrue(services.Count > 0);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var options = scope.ServiceProvider.GetService<IOptions<CorsOptions>>();
            Assert.IsNull(options);
        }
    }

    [TestMethod]
    public void TestBuildPolicies2()
    {
        // Arrange
        Dictionary<string, string?> settings = new Dictionary<string, string?>
        {
            {"corsPolicies:0:policyName", "TestCorsPolicy"},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        IServiceCollection services = new ServiceCollection();
        PolicyManager policyManager = new(configuration, "corsPolicies");
        var mock = new Mock<ITestServiceToMock>();
        services.AddSingleton<ITestServiceToMock>(mock.Object);

        // Act
        policyManager.BuildPolicies(services);

        // Assert
        Assert.IsTrue(services.Count > 0);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var options = scope.ServiceProvider.GetService<IOptions<CorsOptions>>();
            Assert.IsNotNull(options);
            Assert.IsNotNull(options.Value);

            var expectedPolicy = options.Value.GetPolicy("TestCorsPolicy");
            Assert.IsTrue(expectedPolicy.AllowAnyOrigin);
            Assert.IsTrue(expectedPolicy.AllowAnyMethod);
            Assert.IsTrue(expectedPolicy.AllowAnyHeader);
            Assert.IsFalse(expectedPolicy.SupportsCredentials);
        }
    }

    [TestMethod]
    public void TestBuildPolicies3()
    {
        // Arrange
        Dictionary<string, string?> settings = new Dictionary<string, string?>
        {
            {"corsPolicies:0:policyName", "TestCorsPolicy"},
            {"corsPolicies:0:allowedOrigins:0", "http://localhost:4200"},
            {"corsPolicies:0:allowedOrigins:1", "http://localhost:4201"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        IServiceCollection services = new ServiceCollection();
        PolicyManager policyManager = new(configuration, "corsPolicies");
        var mock = new Mock<ITestServiceToMock>();
        services.AddSingleton<ITestServiceToMock>(mock.Object);

        // Act
        policyManager.BuildPolicies(services);

        // Assert
        Assert.IsTrue(services.Count > 0);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var options = scope.ServiceProvider.GetService<IOptions<CorsOptions>>();
            Assert.IsNotNull(options);
            Assert.IsNotNull(options.Value);

            var expectedPolicy = options.Value.GetPolicy("TestCorsPolicy");
            Assert.IsFalse(expectedPolicy.AllowAnyOrigin);
            Assert.IsTrue(expectedPolicy.AllowAnyMethod);
            Assert.IsTrue(expectedPolicy.AllowAnyHeader);
            Assert.IsFalse(expectedPolicy.SupportsCredentials);
        }
    }

    [TestMethod]
    public void TestBuildPolicies4()
    {
        // Arrange
        Dictionary<string, string?> settings = new Dictionary<string, string?>
        {
            {"corsPolicies:0:policyName", "TestCorsPolicy"},
            {"corsPolicies:0:allowCredentials", "true" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        IServiceCollection services = new ServiceCollection();
        PolicyManager policyManager = new(configuration, "corsPolicies");
        var mock = new Mock<ITestServiceToMock>();
        services.AddSingleton<ITestServiceToMock>(mock.Object);

        // Act
        policyManager.BuildPolicies(services);

        // Assert
        Assert.IsTrue(services.Count > 0);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var options = scope.ServiceProvider.GetService<IOptions<CorsOptions>>();
            Assert.IsNotNull(options);
            Assert.IsNotNull(options.Value);

            var expectedPolicy = options.Value.GetPolicy("TestCorsPolicy");
            Assert.IsTrue(expectedPolicy.AllowAnyOrigin);
            Assert.IsTrue(expectedPolicy.AllowAnyMethod);
            Assert.IsTrue(expectedPolicy.AllowAnyHeader);
            Assert.IsTrue(expectedPolicy.SupportsCredentials);
        }
    }

    [TestMethod]
    public void TestBuildPolicies5()
    {
        // Arrange
        Dictionary<string, string?> settings = new Dictionary<string, string?>
        {
            {"corsPolicies:0:policyName", "TestCorsPolicy"},
            {"corsPolicies:0:allowedHeaders:0", "test header"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        IServiceCollection services = new ServiceCollection();
        PolicyManager policyManager = new(configuration, "corsPolicies");
        var mock = new Mock<ITestServiceToMock>();
        services.AddSingleton<ITestServiceToMock>(mock.Object);

        // Act
        policyManager.BuildPolicies(services);

        // Assert
        Assert.IsTrue(services.Count > 0);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var options = scope.ServiceProvider.GetService<IOptions<CorsOptions>>();
            Assert.IsNotNull(options);
            Assert.IsNotNull(options.Value);

            var expectedPolicy = options.Value.GetPolicy("TestCorsPolicy");
            Assert.IsTrue(expectedPolicy.AllowAnyOrigin);
            Assert.IsTrue(expectedPolicy.AllowAnyMethod);
            Assert.IsFalse(expectedPolicy.AllowAnyHeader);
            Assert.IsFalse(expectedPolicy.SupportsCredentials);
        }
    }

    [TestMethod]
    public void TestBuildPolicies6()
    {
        // Arrange
        Dictionary<string, string?> settings = new Dictionary<string, string?>
        {
            {"corsPolicies:0:policyName", "TestCorsPolicy"},
            {"corsPolicies:0:allowedMethods:0", "GET"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        IServiceCollection services = new ServiceCollection();
        PolicyManager policyManager = new(configuration, "corsPolicies");
        var mock = new Mock<ITestServiceToMock>();
        services.AddSingleton<ITestServiceToMock>(mock.Object);

        // Act
        policyManager.BuildPolicies(services);

        // Assert
        Assert.IsTrue(services.Count > 0);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var options = scope.ServiceProvider.GetService<IOptions<CorsOptions>>();
            Assert.IsNotNull(options);
            Assert.IsNotNull(options.Value);

            var expectedPolicy = options.Value.GetPolicy("TestCorsPolicy");
            Assert.IsTrue(expectedPolicy.AllowAnyOrigin);
            Assert.IsFalse(expectedPolicy.AllowAnyMethod);
            Assert.IsTrue(expectedPolicy.AllowAnyHeader);
            Assert.IsFalse(expectedPolicy.SupportsCredentials);
        }
    }

    [TestMethod]
    public void TestUsePolicies()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        PolicyManager policyManager = new(this._configuration, "corsPolicies");
        var mockService = new Mock<ITestServiceToMock>();
        var mockApp = new Mock<IApplicationBuilder>();

        services.AddSingleton<ITestServiceToMock>(mockService.Object);

        // Act
        policyManager.BuildPolicies(services);
        policyManager.UsePolicies(mockApp.Object);

        // Assert
        Assert.IsTrue(services.Count > 0);
    }
}

