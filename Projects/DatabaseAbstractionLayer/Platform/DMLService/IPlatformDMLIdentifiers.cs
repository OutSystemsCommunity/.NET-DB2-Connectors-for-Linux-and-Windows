/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;
using OutSystems.HubEdition.Extensibility.Data.DMLService;

namespace OutSystems.HubEdition.Extensibility.Data.Platform.DMLService {
    /// <summary>
    /// This interface defines methods that help build DML Identifiers for columns, tables, and others.
    /// </summary>
    public interface IPlatformDMLIdentifiers: IDMLIdentifiers {
        /// <summary>
        /// Returns an escaped identifier representing an object (e.g. table or view) that is qualified using 
        /// the information provided by the <see cref="IDatabaseServices.DatabaseConfiguration"/> associated with the <see cref="DMLService"/>.        
        /// </summary>
        /// <param name="database">DatabaseInfo used for qualification</param>
        /// <param name="objectName">Name of the database object (e.g. table, view, stored procedure).</param>
        /// <returns>The escaped and qualified identifier</returns>
        string EscapeAndQualifyIdentifier(IDatabaseInfo database, string objectName);

        /// <summary>
        /// Checks if the specified column name is valid.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>True if the column name is valid, false otherwise</returns>
        bool IsValidColumnName(string columnName);
    }
}
