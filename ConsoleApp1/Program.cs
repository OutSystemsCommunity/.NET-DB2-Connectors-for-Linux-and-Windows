using System;

using OutSystems.HubEdition.DatabaseProvider.DB2LUW;
using OutSystems.HubEdition.DatabaseProvider.DB2LUW.ConfigurationService;
using OutSystems.HubEdition.Extensibility.Data;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;
using System.Data;
using System.Linq;
using OutSystems.HubEdition.Extensibility.Data.IntrospectionService;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var config = new DB2LUWRuntimeDatabaseConfiguration
				{
					ConnectionString = "Database=[dbname]; Server=[servername]:[port]; Password=[password];User ID=[username];",
					Username = "[username]",					
					Schema = "[schemaname]"
					
					
				};
				var provider = new DB2LUWIntegrationDatabaseProvider();
				var databaseServices = provider.GetIntegrationDatabaseServices(config);
				var introService = databaseServices.IntrospectionService;
				var dbs = introService.ListDatabases();
                var tables = introService.ListTableSources(dbs.FirstOrDefault(), null);
				IDatabaseInfo currentDBInfo = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));

				Console.WriteLine($"Services: {databaseServices.ToString()}");

                
				var executionService = databaseServices.ExecutionService;
				var transactionService = databaseServices.TransactionService;

				using (var connection = transactionService.CreateConnection())
				{
                    /*var sql = "select TABNAME from SYSCAT.TABDEP where TABNAME like '%' || @SEARCH_STR || '%' or BNAME like '%' || @SEARCH_STR || '%' FETCH FIRST 1 ROWS ONLY";
                    using (var cmd = executionService.CreateCommand(connection, sql))
					{

						var param = executionService.CreateParameter(cmd, "@SEARCH_STR", DbType.String, "SYSTABLES");
						var obtained = Convert.ToString(executionService.ExecuteScalar(cmd));
						Console.WriteLine($"Obtained: {obtained}");
					}*/

                    var sql = "CALL SPSEARCHPERSON1();";
                    using (var cmd = executionService.CreateCommand(connection, sql))
                    {
                        using (IDataReader reader = executionService.ExecuteReader(cmd))
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Obtained: {(string)reader["NAME"]}");
                            }
                        }                                                
                    }
                }
			}
			catch(Exception exc)
			{
				Console.WriteLine($"Error: {exc.Message}");
			}



			//IDatabaseInfo db = databaseServices.ObjectFactory.CreateDatabaseInfo(GetDatabaseIdentifier(databaseServices));
			//ITableSourceInfo tbSourceInfo =
			//	databaseServices.IntrospectionService.ListTableSourcesWithoutFilter(db).FirstOrDefault(ts => ts.Name.Equals("INTROSPECVIEW126", StringComparison.InvariantCultureIgnoreCase));
			//var columns = introService.GetTableSourceColumns(tbSourceInfo);

			//ITableSourceInfo tbSourceInfoFKs =
			//	databaseServices.IntrospectionService.ListTableSourcesWithoutFilter(db).FirstOrDefault(ts => ts.Name.Equals("FOREIGNKEYSTABLE126", StringComparison.InvariantCultureIgnoreCase));
			//var columnsFks = introService.GetTableSourceForeignKeys(tbSourceInfoFKs);



			
			Console.ReadLine();
		}

		public static string GetDatabaseIdentifier(IDatabaseServices services)
		{
			return services.DMLService.Identifiers.EscapeIdentifier(services.DatabaseConfiguration.DatabaseIdentifier);
		}
	}	
}
