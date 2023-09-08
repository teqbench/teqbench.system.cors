# Trading Toolbox - System Cors

## Overview
The Trading Toobox System Cors (Cross Origin Request) policy management library to enable CORS in ASP.NET Core service application(s). To be called as part of (application) startup.

Browser security prevents a web page from making requests to a different domain than the one that served the web page. This restriction is called the same-origin policy. The same-origin policy prevents a malicious site from reading sensitive data from another site. Sometimes, you might want to allow other sites to make cross-origin requests to your app. For further background, see [references](References).

# Contents
- [Developer Environment Setup](Developer+Environment+Setup)
- [DevOps - Configurations, Builds and Deployments](DevOps)
- [References](References)

# Developer Environment Setup
If have not already done so, in Visual Studio, will need to add a package source to reference the Trading Toolbox's GitHub package repository. A personal access token from GitHub is required in the configuration of a new package source in Visual Studio. This section details how to create a GitHub personal access token and how to configure a new package resource using it in Visual Studio.

## Create GitHub Personal Access Token
A GitHub personal access token is necessary to to gain access to the Trading Toolbox's GitHub package repository sinces it's private.
- To create a GitHub personal access token, sign in to GitHub, naviagate to "Develeper Settings" page accessible via your profile settings page.
- Once on the ""Developer Settings"" page, under the "Personal access tokens" menu option, select "Tokens (classic)" which will navigate to the "Personal access tokens (classic)" page.
- On the "Personal access tokens (classic)" page, select the option to "Generate new token (classic)" then you should be prompted to enter MFA authentication code in order to access the "New personal access token (classic)" page where a new token can be created.
- On the "New personal access token (classic)" page, enter use the following settings to create your new GitHub personal access token:
    - Note: trading-toolbox-packages-token (just a suggested name/description...use whatever you like :))
    - Expiration: select "no expiration"
    - Selected scopes: repo, write:packages, read:packages
- At this point, click update/save and when the token is created, you will temporarily see the new token, copy it and set aside in a text editor as will use as part of setting up NuGet package source in Visual Studio.

## Visual Studio Configuration
In Visual Studio, open Preferences and navigate to the NuGet Sources pane to add a new package source. Use the following settings for the new source:
- Name: github
- Location: https://nuget.pkg.github.com/trading-toolbox/index.json
- Username: {GitHub username}
- Passwork: {GitHub personal access token}

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
