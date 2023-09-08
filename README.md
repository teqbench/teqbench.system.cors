# Overview
Cross origin request policy management to enable CORS in ASP.NET Core service application(s). To be called as part of (application) startup.

> Browser security prevents a web page from making requests to a different domain than the one that served the web page. This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app.

## References
- [Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0)
- [Cross-Origin Resource Sharing (CORS)](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)

# Developer Environment Setup
If have not already done so, in Visual Studio, add a package source to reference the Trading Toolbox's GitHub package repository.

1. A GitHub personal access token is necessary to to gain access to the Trading Toolbox's GitHub package repository sinces it's private.
    - To create a GitHub personal access token, sign in to GitHub, naviagate to "Develeper Settings" page accessible via your profile settings page.
    - Once on the ""Developer Settings"" page, under the "Personal access tokens" menu option, select "Tokens (classic)" which will navigate to the "Personal access tokens (classic)" page.
    - On the "Personal access tokens (classic)" page, select the option to "Generate new token (classic)" then you should be prompted to enter MFA authentication code in order to access the "New personal access token (classic)" page where a new token can be created.
    - On the "New personal access token (classic)" page, enter use the following settings to create your new GitHub personal access token:
        - Note: trading-toolbox-packages-token (just a suggested name/description...use whatever you like :))
        - Expiration: select "no expiration"
        - Selected scopes: repo, write:packages, read:packages
    - At this point, click update/save and when the token is created, you will temporarily see the new token, copy it and set aside in a text editor as will use as part of setting up NuGet package source in Visual Studio in step 2.
2. In Visual Studio, open Preferences and navigate to the NuGet Sources pane to add a new package source.
    - Name: github
    - Location: https://nuget.pkg.github.com/trading-toolbox/index.json
    - Username: (your GitHub username)
    - Passwork: (paste the GitHub personal access token in step 1)

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