/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.ConfigurationService;

namespace OutSystems.HubEdition.DatabaseProvider.DB2LUW.ConfigurationService {

    public class DB2LUWRuntimeDatabaseConfiguration : IRuntimeDatabaseConfiguration {

        public IDatabaseProvider DatabaseProvider {
            get { return DB2LUWIntegrationDatabaseProvider.Instance; }
        }

        [ConfigurationParameter]
        public string Username {
            get;
            set;
        }

        [ConfigurationParameter]
        public string ConnectionString {
            get;
            set;
        }

        public string DatabaseIdentifier {
            get
            {
                if (!string.IsNullOrEmpty(Schema))
                    return Schema;
                else
                    return ExtractShemaFromConnectionString();
            }
        }

        [ConfigurationParameter]
        public string Schema
        {
            get;
            set;
        }

        private string ExtractShemaFromConnectionString()
        {
            string result = "";
            if (!string.IsNullOrEmpty(ConnectionString) && ConnectionString.Contains(DB2LUWIDs.SCHEMA))
            {
                string searchStr = DB2LUWIDs.SCHEMA + "=";
                int startIndex = ConnectionString.IndexOf(searchStr) + searchStr.Length;
                int endIndex = ConnectionString.IndexOf(";", startIndex);
                if (endIndex == -1)
                    endIndex = ConnectionString.IndexOf("<", startIndex);
                if (endIndex == -1)
                    endIndex = ConnectionString.IndexOf(" ", startIndex);

                if (startIndex > 0 && endIndex > 0 && endIndex > startIndex)
                {
                    result = ConnectionString.Substring(startIndex, endIndex - startIndex);
                }
            }
            return result;
        }

        [ConfigurationParameter]
        public bool AutoCommit {
            get;
            set;
        }

        public string _onsConfig = "";
        [ConfigurationParameter]
        public string OnsConfig {
            get { return _onsConfig; }
            set { _onsConfig = value; }
        }

    }
}