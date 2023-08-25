using System.Collections.Generic;

namespace TradingToolbox.System.Cors.Config
{
    /// <summary>
    /// Interface defining CORS policy configuration.
    /// </summary>
    public interface ICorsPolicyConfig
    {
        /// <summary>
        /// Gets or sets the name of the policy.
        /// </summary>
        /// <value>
        /// The name of the policy.
        /// </value>
        string PolicyName { get; set; }

        /// <summary>
        /// Gets or sets the list of allowed origins.
        /// </summary>
        /// <value>
        /// The list of allowed allowed origins.
        /// </value>
        List<string> AllowedOrigins { get; set; }

        /// <summary>
        /// Gets or sets the list of allowed headers.
        /// </summary>
        /// <value>
        /// The list of allowed headers.
        /// </value>
        List<string> AllowedHeaders { get; set; }

        /// <summary>
        /// Gets or sets the list of allowed methods.
        /// </summary>
        /// <value>
        /// The list of allowed methods.
        /// </value>
        List<string> AllowedMethods { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether allow credentials or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> allowed credentials; otherwise, <c>false</c>.
        /// </value>
        bool AllowCredentials { get; set; }
    }
}
