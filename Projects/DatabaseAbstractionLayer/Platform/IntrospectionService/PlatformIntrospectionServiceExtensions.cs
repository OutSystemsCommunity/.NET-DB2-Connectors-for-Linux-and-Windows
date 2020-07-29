/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System.Collections.Generic;
using System.Linq;
using OutSystems.HubEdition.Extensibility.Data.DatabaseObjects;
using OutSystems.HubEdition.Extensibility.Data.Platform.DatabaseObjects;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.Extensibility.Data.Platform.IntrospectionService {
    public static class PlatformIntrospectionServiceExtensions {
        public static IEnumerable<IPlatformTableSourceColumnInfo> GetPlatformTableSourceColumns(
                this IPlatformIntrospectionService platformIntrospectionService, ITableSourceInfo tableSource) {

            IDictionary<ITableSourceInfo, IPlatformTableSourceInfo> result = platformIntrospectionService.GetTableSourcesDetails(tableSource);
            return result.IsEmpty()? null: result.First().Value.Columns;
        }
    }
}
