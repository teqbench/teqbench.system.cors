using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TeqBench.System.Cors.Config;

namespace TeqBench.System.Cors
{
    /// <summary>
    /// CORS policy manager responsible for loading CORS configuration values from application configuration file to allow cross domain requests. To be
    /// used as part of application startup code. 
    /// </summary>
    public sealed class PolicyManager
    {
        #region Member Variables
        private List<string> _policyNames = new List<string>();
        private List<CorsPolicyConfig> _configuredPolicies = new List<CorsPolicyConfig>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyManager"/> class.
        /// </summary>
        /// <param name="configuration">A set of key/value configuration properties used to get configuration values for <see cref="CorsPolicyConfig"/>.</param>
        /// <param name="sectionKey">The configuration section key.</param>
        /// <exception cref="ArgumentException">Invalid 'sectionName' argument; cannot be null, empty or whitespace.</exception>
        public PolicyManager(IConfiguration configuration, string sectionKey)
        {
            if (string.IsNullOrWhiteSpace(sectionKey))
            {
                throw new ArgumentException("Invalid 'sectionName' argument; cannot be null, empty or whitespace.");
            }

            this.ConfiguredPolicies = configuration.GetSection(sectionKey).Get<List<CorsPolicyConfig>>() ?? new List<CorsPolicyConfig>();
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets the list of policy names used to configure CORS.
        /// </summary>
        /// <value>
        /// The list of policy names.
        /// </value>
        private List<string> PolicyNames { get => _policyNames; }

        /// <summary>
        /// Gets or sets the list of configured policies.
        /// </summary>
        /// <value>
        /// The list of configured policies.
        /// </value>
        private List<CorsPolicyConfig> ConfiguredPolicies { get => _configuredPolicies; set => _configuredPolicies = value; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Builds the CORS policies from the supplied CORS configuration values.
        /// </summary>
        /// <param name="services">A collection of service descriptors to ass CORS support to.</param>
        public void BuildPolicies(IServiceCollection services)
        {
            var policies = from p in this.ConfiguredPolicies
                           where !string.IsNullOrWhiteSpace(p.PolicyName)
                           select p;

            policies.ToList().ForEach(p =>
            {
                this.PolicyNames.Add(p.PolicyName);

                services.AddCors(options =>
                {
                    options.AddPolicy(p.PolicyName, builder =>
                    {
                        if (p.AllowedHeaders.Count.Equals(0))
                        {
                            builder.AllowAnyHeader();
                        }
                        else
                        {
                            builder.WithHeaders(p.AllowedHeaders.ToArray());
                        }

                        if (p.AllowedMethods.Count.Equals(0))
                        {
                            builder.AllowAnyMethod();
                        }
                        else
                        {
                            builder.WithMethods(p.AllowedMethods.ToArray());
                        }

                        if (p.AllowCredentials)
                        {
                            builder.AllowCredentials();
                        }

                        if (p.AllowedOrigins.Count.Equals(0))
                        {
                            builder.AllowAnyOrigin();
                        }
                        else
                        {
                            builder.SetIsOriginAllowed((host) => { return p.AllowedOrigins.Contains(host); });
                        }
                    });
                });
            });
        }

        /// <summary>
        /// Apply the configured and built CORS policies to the supplied, configured application pipeline to add CORS support to the application.
        /// </summary>
        /// <param name="app">The configured application pipeline to add CORS support to.</param>
        public void UsePolicies(IApplicationBuilder app)
        {
            this.PolicyNames.ForEach(name => app.UseCors(name));
        } 
        #endregion
    }
}
