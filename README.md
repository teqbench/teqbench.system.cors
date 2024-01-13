# System Cors

![Build Status Badge](.badges/build-status.svg) ![Build Number Badge](.badges/build-number.svg) ![Coverage](.badges/code-coverage.svg)

## Overview
The TeqBench System Cors (Cross Origin Request) policy management library (NuGet package) to enable CORS in ASP.NET Core service application(s). To be called as part of (application) startup.

Browser security prevents a web page from making requests to a different domain than the one that served the web page. This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app. For further background, see [references](#References).

## Contents
- [Developer Environment Setup](#Developer+Environment+Setup)
- [Usage](#Usage)
- [References](#References)
- [License](#License)

## Developer Environment Setup

### General
- [Branching Strategy & Practices](https://github.com/teqbench/teqbench.docs/wiki/Branching-Strategy)

### .NET
- [General Tooling](https://github.com/teqbench/teqbench.docs/wiki/.NET-General-Tooling)
- [Configurations](https://github.com/teqbench/teqbench.docs/wiki/.NET-Configuration-Standards)
- [Coding Standards](https://github.com/teqbench/teqbench.docs/wiki/.NET-Coding-Standards)
- [Solutions](https://github.com/teqbench/teqbench.docs/wiki/.NET-Solutions)
- [Projects](https://github.com/teqbench/teqbench.docs/wiki/.NET-Projects)
- [Building](https://github.com/teqbench/teqbench.docs/wiki/.NET-Build-Process)
- [Package & Deployment](https://github.com/teqbench/teqbench.docs/wiki/.NET-Package-Deploy)
- [Versioning](https://github.com/teqbench/teqbench.docs/wiki/.NET-Versioning-Standards)

## Usage

### Add NuGet Package To Project

```
dotnet add package TeqBench.System.Cors
```

### Update Source Code

> [!NOTE]
> For complete usage, see [TradingToolbox.Applications.Trading.Modeler.ServiceApp](https://github.com/teqbench/tradingtoolbox.applications.trading.modeler.serviceapp)

In the .NET service application's appsettings.json, add the following configuration:

```json
// This an example section for a developer environment. 
"corsPolicies": [
  {
    "policyName": "ApiServiceCorsPolicy",
    "allowedOrigins": [ "http://localhost:4200" ],
    "allowCredentials": true
  }
]
```

In the .NET service applications's startup class, add the following CORS support:
```csharp
/// <summary>
/// Application's startup routine to be used by the web host when starting this service application.
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        this.CorsPolicyManager = new PolicyManager(configuration, "corsPolicies");
    }

    /// <summary>
    /// Gets the CORS policy manager.
    /// </summary>
    /// <value>
    /// The CORS policy manager.
    /// </value>
    private PolicyManager CorsPolicyManager { get; }

    #region Public Methods
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">The services collection to add service configurations to.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        this.CorsPolicyManager.BuildPolicies(services);

        // Add other startup configuration below appropriate to this method.
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application request pipeline to configure.</param>
    /// <param name="env">Web hosting environment information.</param>
    /// <param name="lifetime">The application's lifetime event notifier.</param>
    /// <param name="mongoDbService">The mongo database service.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
      IHostApplicationLifetime lifetime, nsPositionModeling.IMongoDbService mongoDbService)
    {
        // Add other startup configuration before/after call to CorsPolicyManager.UsePolicies
        // appropriate to this method.

        this.CorsPolicyManager.UsePolicies(app);
    }
    #endregion
}
```

## References

- [Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0) - Microsoft
- [Cross-Origin Resource Sharing (CORS)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) - Mozilla
- [Cross-origin resource sharing](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) - Wikipedia

## Licensing

[License](https://github.com/teqbench/teqbench.docs/wiki/License)
