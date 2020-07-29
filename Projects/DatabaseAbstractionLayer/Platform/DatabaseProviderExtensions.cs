/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using System.Linq;
using OutSystems.HubEdition.Extensibility.Data.Platform.QueryProvider;
using OutSystems.RuntimeCommon;
using OutSystems.RuntimeCommon.Log;

namespace OutSystems.HubEdition.Extensibility.Data.Platform {
    public static class DatabaseProviderExtensions {

        /// <summary>
        /// Creates an instance of a type that inherits from <typeparamref name="BaseType"/>, from a list of possible 
        /// Types (<paramref name="specificTypes"/>). Matching is performed using the attribute <see cref="Platform.QueryProvider.DatabaseProviderSpecificImplementationFor"/>.
        /// </summary>
        /// <typeparam name="BaseType">Base type to return</typeparam>
        /// <param name="provider">Database provider used to filter the <paramref name="specificTypes"/></param>
        /// <param name="specificTypes">Specific types inherit from <typeparamref name="BaseType"/></param>
        /// <returns>An object that inherits from <typeparamref name="BaseType"/></returns>
        public static BaseType GetProviderSpecificType<BaseType>(this IDatabaseProvider provider, params Type[] specificTypes) where BaseType : class {
            try {
                string providerKey = provider.Key.Serialize();

                foreach (Type specificType in specificTypes) {
                    var attribute = (DatabaseProviderSpecificImplementationFor) 
                        specificType.GetCustomAttributes(typeof(DatabaseProviderSpecificImplementationFor), false).FirstOrDefault();

                    if (attribute != null && attribute.Key.Equals(providerKey)) {
                        return (BaseType)Activator.CreateInstance(specificType);
                    }
                }                
            } catch (Exception e) {
                string message = String.Format("Error creating instance of '{0}' from types: {1}:\n{2}", typeof(BaseType).Name, 
                    specificTypes.Select(t => t.Name).StrCat(" | "), e);

                EventLogger.WriteError(message);
            }

            return null;
        }

        /// <summary>
        /// Creates an instance of a type that inherits from <typeparamref name="BaseType"/>, from a list of possible 
        /// Types (<paramref name="specificTypes"/>). If none is found, returns an instance of <typeparamref name="BaseType"/>.
        /// </summary>
        /// <typeparam name="BaseType">Base type to return</typeparam>
        /// <param name="provider">Database provider used to filter the <paramref name="specificTypes"/></param>
        /// <param name="specificTypes">Specific types that inherit from <typeparamref name="BaseType"/></param>
        /// <returns>An object that is or inherits from <typeparamref name="BaseType"/></returns>
        public static BaseType GetProviderSpecificOrBaseType<BaseType>(this IDatabaseProvider provider, params Type[] specificTypes) 
                where BaseType : class, new() {

            var type = GetProviderSpecificType<BaseType>(provider, specificTypes);
            return type ?? new BaseType();
        }
    }
}
