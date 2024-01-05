using Microsoft.Extensions.Configuration;

namespace TeqBench.System.Cors.Tests;

[TestClass]
public class PolicyManagerTests
{
    private Dictionary<string, string?> _inMemorySettings = new Dictionary<string, string?>();
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private IConfiguration _configuration;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [TestInitialize]
    public void Setup()
    {
        //Arrange
        this._inMemorySettings = new Dictionary<string, string?> {
            {"corsPolicies:0:policyName", "ApiServiceCorsPolicy"},
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
        PolicyManager policyManager = new(this._configuration, null);
    }
}

