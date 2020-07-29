/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

namespace OutSystems.HubEdition.Extensibility.Data {

    /// <summary>
    /// Specifies the data type to use in the database.
    /// </summary>
    public enum DBDataType {        
        TEXT,        
        BINARY_DATA,
        BOOLEAN,
        INTEGER,
        LONGINTEGER,
        DECIMAL,
        DATE_TIME,
        DATE,
        TIME,
        UNKNOWN
    }


    public static class DBDataTypeExtensions {
        public static bool IsDateOrTimeOrDateTime(this DBDataType type) {
            return type == DBDataType.DATE || type == DBDataType.DATE_TIME || type == DBDataType.TIME;
        }

        public static bool HasLength(this DBDataType type) {
            return type == DBDataType.TEXT || type == DBDataType.DECIMAL;
        }

        public static bool HasDecimals(this DBDataType type) {
            return type == DBDataType.DECIMAL;
        }

        public static string ToText(this DBDataType type) {
            switch (type) {
                case DBDataType.TEXT:
                    return "Text";
                case DBDataType.BINARY_DATA:
                    return "Binary Data";
                case DBDataType.BOOLEAN:
                    return "Boolean";
                case DBDataType.INTEGER:
                    return "Integer";
                case DBDataType.LONGINTEGER:
                    return "Long Integer";
                case DBDataType.DECIMAL:
                    return "Decimal";
                case DBDataType.DATE_TIME:
                    return "DateTime";
                case DBDataType.DATE:
                    return "Date";
                case DBDataType.TIME:
                    return "Time";
                case DBDataType.UNKNOWN:
                default:
                    return "Unknown";
            }
        }

    }
}
