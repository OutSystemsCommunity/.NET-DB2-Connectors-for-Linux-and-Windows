/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

namespace OutSystems.HubEdition.Extensibility.Data.DatabaseObjects {
    public abstract class BaseTableSourceInfo : ITableSourceInfo {

        protected BaseTableSourceInfo(IDatabaseServices databaseServices, IDatabaseInfo database, string name, string qualifiedName) {
            DatabaseServices = databaseServices;
            Database = database;
            Name = name;
            QualifiedName = qualifiedName;
        }

        public IDatabaseServices DatabaseServices { get; private set; }

        /// <summary>
        /// Database where the table source is located.
        /// </summary>
        public virtual IDatabaseInfo Database { get; private set; }

        /// <summary>
        /// Name that identifies the table source inside the database
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Fully qualified identifier of the table source, including the database information
        /// </summary>
        public string QualifiedName { get; private set; }

        /// <summary>
        /// Human-readable name that unambiguously identifies the table source inside a database.
        /// This implementation returns the <see cref="Name"/>.
        /// </summary>
        public virtual string DisplayName {
            get { return Name; }
        }

        /// <summary>
        /// Returns true if both objects represent exactly the same table source, or false otherwise.
        /// This implementation returns true if both objects are the same instance or if the 
        /// <see cref="Database"/>, <see cref="Name"/> and <see cref="QualifiedName"/> are equal.
        /// </summary>
        /// <param name="other">Other table source object to compare with</param>
        /// <returns> true if the current object is equal to the other parameter; otherwise, false.</returns>
        public virtual bool Equals(ITableSourceInfo other) {
            return ReferenceEquals(this, other) || (Database.Equals(other.Database) && Name == other.Name && QualifiedName == other.QualifiedName);
        }

        public override bool Equals(object obj) {
            ITableSourceInfo ts = obj as ITableSourceInfo;
            return ts != null && Equals(ts);
        }

        public override int GetHashCode() {
            return Database.GetHashCode() ^ Name.GetHashCode() ^ QualifiedName.GetHashCode();
        }

        public string ValidationWarning {
            set;
            get;
        }
    }
}
