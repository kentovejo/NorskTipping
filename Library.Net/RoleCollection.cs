using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class RoleCollection : BusinessListBase<RoleCollection, Role>
    {
        #region Factory Methods

        public static RoleCollection NewRoleERList()
        {
            return new RoleCollection();
        }

        public static RoleCollection GetRoleERList()
        {
            return DataPortal.Fetch<RoleCollection>(new FilterCriteria());
        }

        #endregion //Factory Methods

        #region Data Access

        #region Filter Criteria

        [Serializable]
        private class FilterCriteria
        {
        }

        #endregion //Filter Criteria

        #region Data Access - Fetch

        private void DataPortal_Fetch(FilterCriteria criteria)
        {
            RaiseListChangedEvents = false;

            using (var ctx = ConnectionManager<SqlConnection>.GetManager(Helper.DatabaseConnectionString))
            {
                ExecuteFetch(ctx.Connection, criteria);
            } //using

            RaiseListChangedEvents = true;
        }

        private void ExecuteFetch(SqlConnection cn, FilterCriteria criteria)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_SelectRoleAll";
                using (var dr = new SafeDataReader(cm.ExecuteReader()))
                {
                    while (dr.Read())
                        Add(Role.GetRoleEC(dr));
                }
            } //using
        }

        #endregion //Data Access - Fetch

        #region Data Access - Update

        protected override void DataPortal_Update()
        {
            RaiseListChangedEvents = false;

            using (var ctx = ConnectionManager<SqlConnection>.GetManager(Helper.DatabaseConnectionString))
            {
                // loop through each deleted child object
                foreach (var deletedChild in DeletedList)
                    deletedChild.DeleteSelf(ctx.Connection);
                DeletedList.Clear();

                // loop through each non-deleted child object
                foreach (var child in this)
                    if (child.IsNew)
                        child.Insert(ctx.Connection);
                    else
                        child.Update(ctx.Connection);
            } //using SqlConnection

            RaiseListChangedEvents = true;
        }

        #endregion //Data Access - Update

        #endregion //Data Access
    }
}