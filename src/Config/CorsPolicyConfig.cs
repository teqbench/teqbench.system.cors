using System.Collections.Generic;

namespace TeqBench.System.Cors.Config
{
    /// <summary>
    /// Implementation of CORS policy configuration policy.
    /// </summary>
    /// <seealso cref="TeqBench.System.Cors.Contracts.Config.ICorsPolicyConfig" />
    internal class CorsPolicyConfig : ICorsPolicyConfig
    {
        #region Member Variables
        private List<string> _allowedOrigins = new List<string>();
        private List<string> _allowedHeaders = new List<string>();
        private List<string> _allowedMethods = new List<string>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the policy.
        /// </summary>
        /// <value>
        /// The name of the policy.
        /// </value>
        public required string PolicyName { get; set; }

        /// <summary>
        /// Gets or sets the list of allowed origins.
        /// </summary>
        /// <value>
        /// The list of allowed allowed origins.
        /// </value>
        public required List<string> AllowedOrigins
        {
            get
            {
                return this._allowedOrigins;
            }
            set
            {
                if (value != null)
                {
                    this._allowedOrigins = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the list of allowed headers.
        /// </summary>
        /// <value>
        /// The list of allowed headers.
        /// </value>
        public List<string> AllowedHeaders
        {
            get
            {
                return this._allowedHeaders;
            }
            set
            {
                if (value != null)
                {
                    this._allowedHeaders = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the list of allowed methods.
        /// </summary>
        /// <value>
        /// The list of allowed methods.
        /// </value>
        public List<string> AllowedMethods
        {
            get
            {
                return this._allowedMethods;
            }
            set
            {
                if (value != null)
                {
                    this._allowedMethods = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether allow credentials or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> allowed credentials; otherwise, <c>false</c>.
        /// </value>
        public bool AllowCredentials { get; set; } 
        #endregion
    }
}
