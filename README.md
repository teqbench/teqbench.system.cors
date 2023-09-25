# Trading Toolbox - System Cors

## Overview
The Trading Toobox System Cors (Cross Origin Request) policy management library (NuGet package) to enable CORS in ASP.NET Core service application(s). To be called as part of (application) startup.

Browser security prevents a web page from making requests to a different domain than the one that served the web page. This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app. For further background, see [references](#References).

# Contents
- [Developer Environment Setup](#Developer+Environment+Setup)
- [How-to Use](#How-to+Use)
- [DevOps - Configurations, Builds and Deployments](#DevOps)
- [References](#References)

# Developer Environment Setup

> [!NOTE]
> In order to access the Trading Toolbox's package registry on GitHub, a personal access token needs to be created with the appropriate scopes and Visual Studio configured to use it. See the [Trading Toolbox's Organization README](https://github.com/trading-toolbox) which outlines how to create a PAT and configure Visual Studio to use it.

## Tooling

- .NET 7.0.x
- Visual Studio

## Dependencies

> [!NOTE]
> Referenced/restored via the project file

- Microsoft.AspNetCore.Cors, 2.2.0
- Microsoft.Extensions.Configuration.Binder, 7.0.4

# How-to Use

In the .NET service application's appsettings.json, add the following configuration:

```csharp
// This an example section for a developer environment. 
"corsPolicies": [
  {
    "policyName": "ApiServiceCorsPolicy",
    "allowedOrigins": [ "http://localhost:4200" ],
    "allowCredentials": true
  }
]
```

In the .NET service applications's startup class:
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
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, nsPositionModeling.IMongoDbService mongoDbService)
    {
        // Add other startup configuration before/after call to CorsPolicyManager.UsePolicies appropriate to this method.

        this.CorsPolicyManager.UsePolicies(app);
    }
    #endregion
}
```

# DevOps
## Configurations
- Release
    - This configuration is used for compilation of releases to non-debug environments, i.e. production and preview environments.
- Debug
    - This configuration is used for compilation of releases to development/debug environments.

## Branches
- main (production)
- staging (production preview)
- dev (developer integration)

## Builds
### Local
### Cloud

## Deployments
### Local
A local deployment is in effect to a local "package repository" folder. This is useful when want to test changes to a package before pushing to repo.

#### Pre-deployment
* Create a local artifact folder for NuGet packages deployments/references and add to Visual Studio NuGet Preferences (Tools > Preferences > NuGet > Sources). This is the local NuGet artifact repository where local NuGet packages will be deployed to and referenced locally for local/debug development.

#### Deployment
1. Open Terminal prompt from solution folder.
2. Update package version in solution and project files.
3. Create local NuGet package (i.e. pack) via the Project's context menu or from a terminal project at the project's root.

### Cloud
#### Pre-deployment
#### Deployment

# References
- [Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0) - Microsoft
- [Cross-Origin Resource Sharing (CORS)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) - Mozilla
- [Cross-origin resource sharing](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) - Wikipedia
