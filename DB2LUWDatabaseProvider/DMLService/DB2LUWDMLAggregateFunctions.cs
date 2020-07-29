/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/


using OutSystems.HubEdition.Extensibility.Data.DMLService;
using OutSystems.RuntimeCommon;

namespace OutSystems.HubEdition.DatabaseProvider.DB2LUW.DMLService {
    internal class DB2LUWDMLAggregateFunctions : BaseDMLAggregateFunctions {

        internal DB2LUWDMLAggregateFunctions(IDMLService dmlService) : base(dmlService) { }

        public new DB2LUWDMLService DMLService {
            get { return (DB2LUWDMLService)base.DMLService; }
        }

        public override string Avg(string n) {
			//Unable to use Constants.DecimalTotalPrecision = 38, as this is not acceptable by DB2 LUW, maximum value is 31
			return string.Format("Avg(cast({0} as decimal(31, " + Constants.DecimalDecimals + ")))", n);
        }

    }
}
