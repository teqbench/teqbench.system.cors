# System Cors

![Build Status Badge](.badges/build-status.svg) ![Build Number Badge](.badges/build-number.svg) ![Coverage](.badges/code-coverage.svg)

## Overview
The TeqBench System Cors (Cross Origin Request) policy management library (NuGet package) to enable CORS in ASP.NET Core service application(s). To be called as part of (application) startup.

Browser security prevents a web page from making requests to a different domain than the one that served the web page. This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app. For further background, see [references](#References).

# Contents
- [Developer Environment Setup](#Developer+Environment+Setup)
- [Usage](#Usage)
- [DevOps - Configurations, Builds and Deployments](#DevOps)
- [References](#References)
- [License](#License)

# Developer Environment Setup
> [!NOTE]
> In order to access the TeqBench's package registry on GitHub, a personal access token needs to be created with the appropriate scopes and Visual Studio configured to use it. See the [TeqBench Organization's README](https://github.com/teqbench) which outlines how to create a PAT and configure Visual Studio to use it.

## Tooling
- Visual Studio
- .NET 8.0.x
    - In the Visual Studio, navigate to Preferences > Other > Preview Features to enable using the .NET 8 SDK, i.e. check the checkbox for the option `Use the NET 8 SDK if installed (requires restart)`.

## Dependencies
> [!NOTE]
> Referenced/restored via the project file

- Microsoft.AspNetCore.Cors, 2.2.0
- Microsoft.Extensions.Configuration.Binder, 8.0.0

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

## DevOps

### Configurations

- Release
    - This configuration is used for compilation of releases to non-debug environments, i.e. production and preview environments.
- Debug
    - This configuration is used for compilation of releases to development/debug environments.

### Branching Strategy

- GitHub Flow
  - [Introduction From GitHub](https://docs.github.com/en/get-started/quickstart/github-flow)
  - [Indepth Overview](https://githubflow.github.io)

### Branches

- main (production)

### Local - Build, Pack(age) & Deploy

- To build/pack locally use the "Debug" configuration.

#### Build

- To create NuGet build locally, can be done either in Visual Studio or command line.
  - Visual Studio
    - Load the project
    - Right-mouse clicking the project file to bring up the context menu and selecting "Build {project's name}".
  - Command Line
    - Open terminal.
    - Navigate to the project's root folder and issue the command "dotnet build -c:Debug".
  - Build Output
    - Build output for Visual Studio or command line, i.e. assembly, will be found in the "{project}/bin/Debug/" folder.

#### Pack(age)

- To create NuGet package locally, can be done either in Visual Studio or command line.
  - Visual Studio
    - Load the project
    - Right-mouse clicking the project file to bring up the context menu and selecting "Pack {project's name}". 
  - Command Line
    - Open terminal.
    - Navigate to the project's root folder and issue the command "dotnet pack -c:Debug"
  - Pack Output
    - Pack command output for Visual Studio or command line, i.e. NuGet package file ".0.0.0-dev.nupkg", will be found in the "{solution}/publish" folder.
    - Because used the "Debug" configuration, the NuGet package version created is "0.0.0-dev" to communicate this is a NON-PRODUCTION build/package and should only be used for development/debug purposes; it should NEVER be uploaded to the TeqBench organization's package registry on GitHub.
   
#### Deployment

- A local deployment, in effect, is to a local "package source" folder and is configured in Visual Studio at "Visual Studio > Preferences > NuGet > Sources". This is useful when want to test changes to a package before pushing code changes to the project's repository.
- A locally built NuGet package can be deployed locally by copying the "{assembly-name}.0.0.0-dev.nupkg" to the local NuGet package source (i.e. a local folder) as configured in "Visual Studio > Preferences > NuGet > Sources".

### Cloud - Build, Pack(age) & Deploy

- Cloud based build, pack and deployment requires a pull request and successful merge into the main branch in order to start the release workflow.
- If opt to create a NuGet package, the resulting package will be uploaded to the [TeqBench Package Registry](https://github.com/orgs/teqbench/packages) on GitHub.
- As part of the pull request, the "Mergable" option must be set to "PR - Allow merge" in order for the pull request to be merged into the main branch, assuming all other validations pass.
- As part of the pull request, the "Release Type" option must be specified (e.g. "Major (Backwards-incompatible updates and/or bugfixes)", "Minor (Backwards-compatible updates and bugfixes)", or "Patch (Backwards-compatible bugfixes - ONLY)") to determine how the version number will be updated as part of the build. See [TeqBench Org's README](https://github.com/teqbench#version-numbers-in-teqbench) for more information on version numbers in TeqBench.

## References

- [Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0) - Microsoft
- [Cross-Origin Resource Sharing (CORS)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) - Mozilla
- [Cross-origin resource sharing](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) - Wikipedia

## License

&copy; 2021 TeqBench. All source code in this repository is only allowed for use by TeqBench; other usage by internal or external parties requires written consent from TeqBench.
