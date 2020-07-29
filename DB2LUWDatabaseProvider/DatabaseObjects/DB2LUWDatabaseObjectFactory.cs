/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System.Text.RegularExpressions;
using OutSystems.HubEdition.DatabaseProvider.DB2LUW.ConfigurationService;
using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;
using OutSystems.HubEdition.Extensibility.Data.IntrospectionService;

namespace OutSystems.HubEdition.DatabaseProvider.DB2LUW.DatabaseObjects {
    public class DB2LUWDatabaseObjectFactory : IDatabaseObjectFactory {
        
        // A DB2LUW piece is any character excluding " and . if not delimited by the literal char, "
        private static readonly Regex pieceRegex = new Regex("(\"[^\"]+\")|([^\\.\"]+)", RegexOptions.Compiled);
        
        private static readonly char[] trimChars = " \"".ToCharArray();

        private readonly IDatabaseServices databaseServices;

        public DB2LUWDatabaseObjectFactory(IDatabaseServices databaseServices) {
            this.databaseServices = databaseServices;
        }

        

        public IDatabaseInfo CreateDatabaseInfo(string databaseIdentifier) {
            string schema = databaseIdentifier.Trim(trimChars);
            return new DB2LUWDatabaseInfo(databaseServices, schema);
        }

        public ITableSourceInfo CreateTableSourceInfo(string qualifiedName) {
            string table;
            string schema;
            if (ParseQualifiedTableName(qualifiedName, out schema, out table)) {
                return new DB2LUWTableSourceInfo(databaseServices, new DB2LUWDatabaseInfo(databaseServices, schema), table, qualifiedName);
            }
            throw new IntrospectionServiceException("'" + qualifiedName + "' is not a valid qualified table name.");
        }

        private bool ParseQualifiedTableName(string qualifiedName, out string schema, out string table) {
            schema = string.Empty;
            table = string.Empty;

            MatchCollection matches = pieceRegex.Matches(qualifiedName);
            switch (matches.Count) {
                case 2:
                    schema = matches[0].Value.Trim(trimChars);
                    table = matches[1].Value.Trim(trimChars);
                    break;
                case 1:
                    table = matches[0].Value.Trim(trimChars);
                    break;
                default:
                    return false;
            }
            return true;
        }

        public IDatabaseInfo CreateLocalDatabaseInfo() {
            return new DB2LUWDatabaseInfo(databaseServices, ((DB2LUWDatabaseConfiguration)databaseServices.DatabaseConfiguration).Schema);
        }

    }
}
