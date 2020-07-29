/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Net;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;

namespace OutSystems.HubEdition.Extensibility.Data.Platform.Configuration {

    public enum Source { Application, Services }
    public enum User { Admin, Runtime, Session, Log }

    public interface IPlatformDatabaseConfiguration : IEquatable<IPlatformDatabaseConfiguration> {

        /// <summary>
        /// Gets the database provider. It provides information about the database,
        /// and access to its services.
        /// </summary>
        /// <value>
        /// The database provider.
        /// </value>
        IPlatformDatabaseProvider PlatformDatabaseProvider { get; }

        /// <summary>
        /// This property indicates if the plugin implements the <code>IElevatedUserConfiguration</code> interface,
        /// meaning that it has operations that require elevated privileges user.
        /// When true, the caller (e.g. Configuration Tool) will ask the user for credentials, and use them in operations that require elevated user permissions.
        /// </summary>
        /// <value>
        /// True if it implements elevated privileges operations, False otherwise.
        /// </value>
        bool ImplementsElevatedPrivilegesOperations {
            get;
        }

        /// <summary>
        /// Indicates the current state of the Configuration, if the property is true, then IntegratedAuthenticationMode is set
        /// </summary>
        /// <value>
        /// The authentication type of the configuration.
        /// </value>
        AuthenticationType AuthenticationMode {
            get;
        }

        /// <summary>
        /// Gets the object that compacts all the needed configuration parameters to be used in runtime.
        /// </summary>
        /// <param name="source">Specifies the component that will use the configuration (e.g. service or application).</param>
        /// <param name="userType">Specifies to what user the configuration will refer to.</param>
        /// <returns>The configuration that allows services or applications to access the database as the specified user.</returns>
        IRuntimeDatabaseConfiguration RuntimeDatabaseConfiguration(Source source, User userType);

        /// <summary>
        /// Indicates the current state of the Configuration, if the property is true, then Advanced Configuration mode is set.
        /// </summary>
        /// <value>
        /// True if Advanced Configuration mode is set. False otherwise.
        /// </value>
        bool AdvancedConfigurationMode {
            get;set;
        }

        /// <summary>
        /// Contextual text to help the user understand what does the Advanced configuration consist of.
        /// </summary>
        /// <value>
        /// The contextual help for advanced mode.
        /// </value>
        string ContextualHelpForAdvancedMode {
            get;
        }

        /// <summary>
        /// Contextual text to help the user understand what does the Basic configuration consist of.
        /// </summary>
        /// <value>
        /// The contextual help for basic mode.
        /// </value>
        string ContextualHelpForBasicMode {
            get;
        }

        #region Admin

        /// <summary>
        /// This property returns the credentials for the admin user.
        /// </summary>
        /// <value>
        /// The admin authentication credentials.
        /// </value>
        NetworkCredential AdminAuthenticationCredential {
            get;
        }

        #endregion

        #region Runtime

        /// <summary>
        /// This property returns the credentials for the runtime user.
        /// </summary>
        /// <value>
        /// The runtime authentication credentials.
        /// </value>
        NetworkCredential RuntimeAuthenticationCredential {
            get;
        }

        #endregion

        #region Log

        /// <summary>
        /// This property returns the credentials for the log user.
        /// </summary>
        /// <value>
        /// The log authentication credentials.
        /// </value>
        NetworkCredential LogAuthenticationCredential {
            get;
        }

        #endregion
    }
}
