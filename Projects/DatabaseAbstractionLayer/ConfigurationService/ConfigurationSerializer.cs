/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService.MetaConfiguration;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.Extensibility.Data.ConfigurationService {

    public static class Serializers {
    
        public static IntegrationSerializer ForIntegration {
            get {
                return new IntegrationSerializer();
            }
        }
    }

    sealed public class Serializer<TConfiguration> {

        public Func<string, string> ValueEncrypter { get; set; }

        private readonly string tag;
        private readonly Func<TConfiguration, IEnumerable<IParameter>> parameterExtractor;

        internal Serializer(string tag, Func<TConfiguration, IEnumerable<IParameter>> parameterExtractor) {
            this.tag = tag;
            this.parameterExtractor = parameterExtractor;
            this.ValueEncrypter = null;
        }

        public XElement Serialize(TConfiguration conf) {
            var res = new XElement(tag);
            parameterExtractor(conf)
                .Select(p => {
                    bool encrypt = ValueEncrypter != null && p.Encrypt;
                    if (encrypt) {
                        return new XElement(p.Name,
                                    ValueEncrypter(p.Get()),
                                    new XAttribute("encrypted", "true"));
                    } else {
                        return new XElement(p.Name,
                                    p.Get(),
                                    new XAttribute("encrypted", "false"));
                    }
                })
                .Apply(res.Add);
            return res;
        }
    }

    public static class Deserializers {
    
        public static IntegrationDeserializer ForIntegration {
            get {
                return new IntegrationDeserializer();
            }
        }

        public static Deserializer<IRuntimeDatabaseConfiguration> ForRuntime {
            get {
                return new Deserializer<IRuntimeDatabaseConfiguration>("RuntimeDatabaseConfiguration");
            }
        }
    }

    sealed public class Deserializer<TConfiguration> {

        public string Tag { get; private set; }
        public Func<string, string> ValueDecrypter { get; set; }

        internal Deserializer(string tag) {
            Tag = tag;
            ValueDecrypter = null;
        }

        public void Deserialize(XElement xmlConf, TConfiguration conf) {

            if (xmlConf == null) {
                throw new ConfigurationSerializationException("No root");
            }

            if (xmlConf.Name != Tag) {
                throw new ConfigurationSerializationException("Expected element tagged " + Tag + " got " + xmlConf.Name);
            }

            var meta = new MetaDatabaseConfiguration(conf);

            foreach (var elem in xmlConf.Elements()) {
                var param = meta.GetParameter(elem.Name.LocalName);
                if (param != null) {

                    bool decryptValue = ValueDecrypter != null
                        && elem.Attribute("encrypted") != null
                        && elem.Attribute("encrypted").Value.Equals("true");

                    param.Set(decryptValue ? ValueDecrypter(elem.Value) : elem.Value);
                }
            }
        }

    }


    public sealed class IntegrationDeserializer {

        internal IntegrationDeserializer() { }

        public static IDictionary<string, string> ReadPropertiesFromSerialization(string serializedForm) {
            try {
                var props = new Dictionary<string, string>();
                var configurationXML = XDocument.Load(new StringReader(serializedForm));

                if (configurationXML.Root == null) return props;
                foreach (var elem in configurationXML.Root.Elements()) {
                    props.Add(elem.Name.LocalName, elem.Value);
                }
                return props;
            } catch (XmlException) {
                throw new Exception("Error deserializing database connection: Invalid Database Configuration Format");
            }
        }

        public void Deserialize(string serializedForm, IIntegrationDatabaseConfiguration config) {
            try {
                var wrapper = new MetaDatabaseConfiguration(config);
                var properties = ReadPropertiesFromSerialization(serializedForm);
                foreach (var nameValue in properties) {
                    var param = wrapper.GetParameter(nameValue.Key);

                    if (param != null) {
                        param.Set(nameValue.Value);
                    }
                }
            } catch (Exception e) {
                throw new ConfigurationSerializationException(e);
            }
        }
    
    }

    public sealed class IntegrationSerializer {

        internal IntegrationSerializer() { }

        public string Serialize(IIntegrationDatabaseConfiguration config) {
            try {
                var wrapper = new MetaDatabaseConfiguration(config);

                var parametersToSerialize = wrapper.VisibleParameters.Cast<IParameter>().Where(p => !p.ReadOnly)
                                            .Union(wrapper.Parameters.Where(p => !(p is IUserDefinedParameter)));

                var configurationXML = new XDocument(new XElement("DBConfiguration"));

                foreach (var paramToSerialize in parametersToSerialize) {
                    configurationXML.Root.Add(new XElement(paramToSerialize.Name, paramToSerialize.Get()));
                }

                return configurationXML.ToString();
            } catch (Exception e) {
                throw new ConfigurationSerializationException(e);
            }
        }


    }

    /// <summary>
    /// Exception for signalling configuration serialization errors.
    /// </summary>
    public class ConfigurationSerializationException : Exception {

        public ConfigurationSerializationException(string message) : base(message) { }

        public ConfigurationSerializationException(Exception e) : base(e.Message, e) { }
    }
}
