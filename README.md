# Overview
Cross origin request policy management to enable CORS in ASP.NET Core service application(s). To be called as part of (application) startup.

> Browser security prevents a web page from making requests to a different domain than the one that served the web page. This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app.

## References
- [Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0)
- [Cross-Origin Resource Sharing (CORS)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)

# Example Usage
```json
// appsettings.json in a service's API application for CORS configuration values (dev environment setting example)

{
  "corsPolicies": [
    {
      "policyName": "ApiLogServiceCorsPolicy",
      "allowedOrigins": [ "https://localhost:44336", "http://localhost:4200", "http://localhost:8083" ],
      "allowCredentials": true
    }
  ]
}
```

```c#
// Startup.cs in a service's API application.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    private PolicyManager CorsPolicyManager { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        this.CorsPolicyManager = new PolicyManager(configuration, "corsPolicies");
    }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">The service descriptors.</param>
    /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
    public void ConfigureServices(IServiceCollection services)
    {
        this.CorsPolicyManager.BuildPolicies(services);
    }

    /// <summary>
    /// Configures the specified application.
    /// </summary>
    /// <param name="app">The application mechanisms to configure an application's pipeline.</param>
    /// <param name="env">The web hosting environmwent information the application will run in.</param>
    /// <param name="lifetime">The application lifetime event provider.</param>
    /// <param name="documentDbService">The document database service instance to dispose when app is shutting down.</param>
    /// <remarks>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </remarks>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IDocumentDbService documentDbService)
    {
        lifetime.ApplicationStopping.Register(state => {
            ((IDisposable) state).Dispose();
        }, documentDbService);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        this.CorsPolicyManager.UsePolicies(app);

        // There is likely more configuiration steps after call to "UsePolicies"; kept short for example only.
    }
}
```

# Builds
## Local/Debug Builds
### Command Line Build
### IDE Build

## Cloud Builds
* TODO

# Deployments
## Local Deployments
### Pre-deployment
* Create a local artifact folder for NuGet packages deployments/references and add to Visual Studio NuGet Preferences (Tools > Preferences > NuGet > Sources). This is the local NuGet artifact repository where local NuGet packages will be deployed to and referenced locally for local/debug development.

### Deployment
1. Open Terminal prompt from solution folder.
2. Update package version
    > `$ command line version update information`
3. Create local NuGet package (i.e. pack).
    > `$ dotnet pack`

## Cloud Deployments - Staging
### Pre-deployment
* TODO

### Deployment
* TODO

## Cloud Deployments - Production
### Pre-deployment
* TODO

### Deployment
* TODO