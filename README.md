# Trading Toolbox - System Cors

## Overview
The Trading Toobox System Cors (Cross Origin Request) policy management library (NuGet package) to enable CORS in ASP.NET Core service application(s). To be called as part of (application) startup.

Browser security prevents a web page from making requests to a different domain than the one that served the web page. This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app. For further background, see [references](#References).

# Contents
- [Developer Environment Setup](#Developer+Environment+Setup)
- [Usage](#Usage)
- [DevOps - Configurations, Builds and Deployments](#DevOps)
- [References](#References)
- [License](#License)

# Developer Environment Setup
> [!NOTE]
> In order to access the Trading Toolbox's package registry on GitHub, a personal access token needs to be created with the appropriate scopes and Visual Studio configured to use it. See the [Trading Toolbox Organization's README](https://github.com/trading-toolbox) which outlines how to create a PAT and configure Visual Studio to use it.

## Tooling
- .NET 7.0.x
- Visual Studio

## Dependencies
> [!NOTE]
> Referenced/restored via the project file

- Microsoft.AspNetCore.Cors, 2.2.0
- Microsoft.Extensions.Configuration.Binder, 7.0.4

# Usage
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

## Local - Build, Pack(age) & Deploy
- To build/pack locally use the "Debug" configuration.

### Build
- To create NuGet build locally, can be done either in Visual Studio or command line.
  - Visual Studio
    - Load the project
    - Right-mouse clicking the project file to bring up the context menu and selecting "Build {project's name}".
  - Command Line
    - Open terminal.
    - Navigate to the project's root folder and issue the command "dotnet build -c:Debug".
  - Build Output
    - Build output for Visual Studio or command line, i.e. assembly, will be found in the "{project}/bin/Debug/" folder.

### Pack(age)
- To create NuGet package locally, can be done either in Visual Studio or command line.
  - Visual Studio
    - Load the project
    - Right-mouse clicking the project file to bring up the context menu and selecting "Pack {project's name}". 
  - Command Line
    - Open terminal.
    - Navigate to the project's root folder and issue the command "dotnet pack -c:Debug"
  - Pack Output
    - Pack command output for Visual Studio or command line, i.e. NuGet package file ".0.0.0-dev.nupkg", will be found in the "{project}/bin/Debug/" folder.
    - Because used the "Debug" configuration, the NuGet package version created is "0.0.0-dev" to communicate this is a NON-PRODUCTION build/package and should only be used for development/debug purposes; it should NEVER be uploaded to the Trading Toolbox organization's package registry on GitHub.
   
### Deployment
- A local deployment, in effect, is to a local "package source" folder and is configured in Visual Studio at "Visual Studio > Preferences > NuGet > Sources". This is useful when want to test changes to a package before pushing code changes to the project's repository.
- A locally built NuGet package can be deployed locally by copying the "{assembly-name}.0.0.0-dev.nupkg" to the local NuGet package source (i.e. a local folder) as configured in "Visual Studio > Preferences > NuGet > Sources".

## Cloud - Build, Pack(age) & Deploy
- Cloud based build/pack use the "Release" configuration, and currently, ONLY build from the "main" branch.
- Cloud based build/pack/deploy use the [Production Release Workflow](https://github.com/trading-toolbox/production-release-workflow/actions/workflows/production-release-workflow.yml) Trading Toolbox Action to build and optionally pack/deploy a NuGet package for a selected project (i.e. repository).
- If opt to create a NuGet package, the resulting package will be uploaded to the [Trading Toolbox Package Registry](https://github.com/orgs/trading-toolbox/packages) on GitHub.
- As part of the [Production Release Workflow](https://github.com/trading-toolbox/production-release-workflow/actions/workflows/production-release-workflow.yml) build options, select what type of update the release is, e.g. "Major (Backwards-incompatible functionality added)", "Minor (Backwards-compatible functionality added)", or "Patch/Revision (Bugfixes/updates for a specific release)". See [Trading Toolbox Org's README](https://github.com/trading-toolbox#version-numbers-in-trading-toolbox) for more information on version numbers in Trading Toolbox.

# References
- [Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0) - Microsoft
- [Cross-Origin Resource Sharing (CORS)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) - Mozilla
- [Cross-origin resource sharing](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) - Wikipedia

# License
&copy; 2021 Trading Toolbox. All source code in this repository is only allowed for use by Trading Toolbox; other usage by internal or external parties requires written consent from Trading Toolobx.
