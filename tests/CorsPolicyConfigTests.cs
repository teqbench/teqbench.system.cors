using TeqBench.System.Cors.Config;

namespace TeqBench.System.Cors.Tests;

[TestClass]
public class CorsPolicyConfigTests
{
    [TestMethod]
    public void TestPolicyName()
    {
        CorsPolicyConfig config = new CorsPolicyConfig() { PolicyName = "A Name" };
        Assert.AreEqual(config.PolicyName, "A Name");

        config.PolicyName = "second name test";
        Assert.AreEqual(config.PolicyName, "second name test");
    }

    [TestMethod]
    public void TestAllowedOrigins()
    {
        CorsPolicyConfig config = new CorsPolicyConfig() { PolicyName = "A Name" };

        // By default list is not null and empty.
        Assert.IsNotNull(config.AllowedOrigins);
        Assert.IsTrue(config.AllowedOrigins.Count == 0);

        // Try assigning null
        config.AllowedOrigins = null;
        Assert.IsNotNull(config.AllowedOrigins);
        Assert.IsTrue(config.AllowedOrigins.Count == 0);

        // Assign new list
        config.AllowedOrigins = new List<string>();
        Assert.IsNotNull(config.AllowedOrigins);
        Assert.IsTrue(config.AllowedOrigins.Count == 0);
    }

    [TestMethod]
    public void TestAllowedHeaders()
    {
        CorsPolicyConfig config = new CorsPolicyConfig() { PolicyName = "A Name" };

        // By default list is not null and empty.
        Assert.IsNotNull(config.AllowedHeaders);
        Assert.IsTrue(config.AllowedHeaders.Count == 0);

        // Try assigning null
        config.AllowedHeaders = null;
        Assert.IsNotNull(config.AllowedHeaders);
        Assert.IsTrue(config.AllowedHeaders.Count == 0);

        // Assign new list
        config.AllowedHeaders = new List<string>();
        Assert.IsNotNull(config.AllowedHeaders);
        Assert.IsTrue(config.AllowedHeaders.Count == 0);
    }

    [TestMethod]
    public void TestAllowedMethods()
    {
        CorsPolicyConfig config = new CorsPolicyConfig() { PolicyName = "A Name" };

        // By default list is not null and empty.
        Assert.IsNotNull(config.AllowedMethods);
        Assert.IsTrue(config.AllowedMethods.Count == 0);

        // Try assigning null
        config.AllowedMethods = null;
        Assert.IsNotNull(config.AllowedMethods);
        Assert.IsTrue(config.AllowedMethods.Count == 0);

        // Assign new list
        config.AllowedMethods = new List<string>();
        Assert.IsNotNull(config.AllowedMethods);
        Assert.IsTrue(config.AllowedMethods.Count == 0);
    }

    [TestMethod]
    public void TestAllowCredentials()
    {
        CorsPolicyConfig config = new CorsPolicyConfig() { PolicyName = "A Name" };

        // Test default is false
        Assert.IsFalse(config.AllowCredentials);

        config.AllowCredentials = true;
        Assert.IsTrue(config.AllowCredentials);
    }
}