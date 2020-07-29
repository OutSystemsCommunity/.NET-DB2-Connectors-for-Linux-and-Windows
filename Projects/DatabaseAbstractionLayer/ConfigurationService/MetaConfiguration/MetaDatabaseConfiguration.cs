/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OutSystems.RuntimeCommon;
using System.Collections.Concurrent;

namespace OutSystems.HubEdition.Extensibility.Data.ConfigurationService.MetaConfiguration {

    internal class MetaParameterExtractor {

        internal enum MetaParamType {
            Parameter,
            UserDefinedParameter,
            UserChosenOptionParameter
        }

        internal class MetaParam {

            public MetaParamType Type { get; set; }
            public string PropName { get; set; }
            public MethodInfo Getter { get; set; }
            public MethodInfo Setter { get; set; }
            public bool Encrypt { get; set; }
            public bool Persist { get; set; }
            public MethodInfo visibilityChecker { get; set; }
            public UserDefinedConfigurationParameter UserConfigurationParameter { get; set; }
            public Dictionary<string, KeyValuePair<string, string>> HelpInfo { get; set; }

            public IParameter ToParameter(object configuraiton) {
                switch (Type) {
                    case MetaParamType.Parameter:
                        return new Parameter(PropName, Getter, Setter, Encrypt, Persist, configuraiton);
                    case MetaParamType.UserDefinedParameter:
                        return new UserDefinedParameter(PropName, Getter, Setter, configuraiton, Encrypt, Persist, visibilityChecker, UserConfigurationParameter);
#if !JAVA
                    case MetaParamType.UserChosenOptionParameter:
                        return new UserChosenOptionParameter(PropName, Getter, Setter, configuraiton, Encrypt, Persist, visibilityChecker, UserConfigurationParameter, HelpInfo);
#endif
                    default:
                        throw new InvalidOperationException("Unexpected parameter type" + Type);
                }
            }
        }


        private readonly IList<MetaParam> parameters;

        public MetaParameterExtractor(Type configurationType) {
            this.parameters = FindParameters(configurationType);
        }

        private IList<MetaParam> FindParameters(Type type) {
            IDictionary<string, MetaParam> results = new Dictionary<string, MetaParam>();
            for (Type t = type; t != null; t = t.BaseType) {
                foreach (PropertyInfo prop in t.GetProperties()) {
                    Attribute param = GetConfigurationParameter(prop);
                    if (param != null && !results.ContainsKey(prop.Name)) {
                        results.Add(prop.Name, GetParameterToAdd(type, param, prop));
                    }
                }
            }
            return results.Values.ToList();
        }

        private static Attribute GetConfigurationParameter(PropertyInfo prop) {
            var attrs = (ConfigurationParameter[]) prop.GetCustomAttributes(typeof(ConfigurationParameter), false);
            return attrs.Length == 0 ? null : attrs[0];
        }

        private static Attribute[] GetHelpLinksForEnum(PropertyInfo prop) {
            return (HelpLinkForEnumConfigurationParameter[]) prop.GetCustomAttributes(typeof(HelpLinkForEnumConfigurationParameter), false);
        }

        private MetaParam GetParameterToAdd(Type type, Attribute param, PropertyInfo prop) {
            
            string propName = prop.Name;

            
            MethodInfo getter = prop.GetGetMethod();

            
            MethodInfo setter = prop.GetSetMethod();


            var userDefinedConfigParam = param as UserDefinedConfigurationParameter;
            if (userDefinedConfigParam != null) {
                MethodInfo visibilityChecker = null;
                if (!string.IsNullOrEmpty(userDefinedConfigParam.VisibilityChecker)) {
                    visibilityChecker = type.GetMethod(userDefinedConfigParam.VisibilityChecker);
                }
#if !JAVA
                if (prop.PropertyType.IsEnum) {

                    Dictionary<string, KeyValuePair<string, string>> helpInfo = null;

                    foreach (HelpLinkForEnumConfigurationParameter help in GetHelpLinksForEnum(prop)) {
                        if (helpInfo == null) {
                            helpInfo = new Dictionary<string, KeyValuePair<string, string>>();
                        }
                        helpInfo.Add(help.EnumValue, new KeyValuePair<string, string>(help.Text, help.Url));
                    }
                    return new MetaParam {
                        Type = MetaParamType.UserChosenOptionParameter,
                        PropName = propName,
                        Getter = getter,
                        Setter = setter,
                        Encrypt = userDefinedConfigParam.Encrypt,
                        Persist = userDefinedConfigParam.Persist,
                        visibilityChecker = visibilityChecker,
                        UserConfigurationParameter = userDefinedConfigParam,
                        HelpInfo = helpInfo
                    };
                }
#endif

                return new MetaParam {
                    Type = MetaParamType.UserDefinedParameter,
                    PropName = propName,
                    Getter = getter,
                    Setter = setter,
                    Encrypt = userDefinedConfigParam.Encrypt,
                    Persist = userDefinedConfigParam.Persist,
                    visibilityChecker = visibilityChecker,
                    UserConfigurationParameter = userDefinedConfigParam
                };
            }

            var configParam = (ConfigurationParameter) param;
            return new MetaParam {
                Type = MetaParamType.Parameter,
                PropName = propName,
                Getter = getter,
                Setter = setter,
                Encrypt = configParam.Encrypt,
                Persist = configParam.Persist
            };
        }

        public IList<IParameter> ToParameters(object configuration) {
            return parameters.Select(p => p.ToParameter(configuration)).ToList();
        }

    }

    /// <summary>
    /// Represents the meta-information about a database configuration.
    /// </summary>

    public class MetaDatabaseConfiguration {

        private static readonly ConcurrentDictionary<Type, MetaParameterExtractor> parameterExtractorCache = new ConcurrentDictionary<Type, MetaParameterExtractor>();

        private readonly object configuration;
        private readonly IList<IParameter> parameters;
        
        public MetaDatabaseConfiguration(object configuration) {
            this.configuration = configuration;
            this.parameters = parameterExtractorCache
                                .GetOrAdd(
                                    configuration.GetType(),
                                    t => new MetaParameterExtractor(t))
                                .ToParameters(configuration);
        }

        /// <summary>
        /// Returns a parameter with the given name.
        /// </summary>
        ///
        /// <param name="name">The parameter's name.</param>
        ///
        /// <returns>A parameter with the given name.</returns>
        public IParameter GetParameter(string name) {

            Func<IParameter, bool> matchesName = p => p.Name.Equals(name);

            return Parameters.FirstOrDefault(matchesName) ??
                   AdvancedModeParameters().Cast<IParameter>().FirstOrDefault(matchesName);
        }

        private IEnumerable<IUserDefinedParameter> AdvancedModeParameters() {

            var integrationConf = configuration as IIntegrationDatabaseConfiguration;
            if (integrationConf == null) yield break;
            yield return new AdvancedConnectionStringParam(integrationConf);
            yield return new ConnStringTemplateParam(integrationConf);
        }

        public string this[string pname] {
            get { return GetParameter(pname).Get(); }
            set { GetParameter(pname).Set(value); }
        }

        /// <summary>
        /// Gets a list of visible parameters.
        /// </summary>
        ///
        /// <value>The list of visible parameters.</value>
        public IList<IUserDefinedParameter> VisibleParameters {
            get {
                if (configuration is IIntegrationDatabaseConfiguration &&  ((IIntegrationDatabaseConfiguration) configuration).AdvancedConfiguration.IsSet()) {
                    return parameters
                        .OfType<IUserDefinedParameter>()
                        .Where(p => p.Visible && p.Region == ParameterRegion.UserSpecific)
                        .Union(AdvancedModeParameters().Where(p => p.Visible))
                        .ToList();
                } 
                return parameters.OfType<IUserDefinedParameter>().Where(p => p.Visible).ToList();
            }
        }

        public IList<IParameter> PersistableParameters {
            get {
                return parameters.Where(p => p.Persist).ToList();
            }
        }

        /// <summary>
        /// Gets a list of all parameters.
        /// </summary>
        ///
        /// <value>The list of parameters.</value>
        public IList<IParameter> Parameters {
            get {
                return parameters;
            }
        }
    }
}
