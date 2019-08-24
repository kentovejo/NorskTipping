using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserClaimERList : BusinessListBase<UserClaimERList, UserClaimEC>
    {
        #region BindingList Overrides

        protected new object AddNewCore()
        {
            var item = UserClaimEC.NewUserClaimEC();
            Add(item);
            return item;
        }

        #endregion //BindingList Overrides

        #region Factory Methods

        public static UserClaimERList NewUserClaimERList()
        {
            return new UserClaimERList();
        }

        public static UserClaimERList GetUserClaimERList(string userId)
        {
            return DataPortal.Fetch<UserClaimERList>(new FilterCriteria(userId));
        }

        #endregion //Factory Methods

        #region Data Access

        #region Filter Criteria

        [Serializable]
        private class FilterCriteria
        {
            internal readonly object UserId;

            public FilterCriteria(string userId)
            {
                UserId = userId;
            }
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
                cm.CommandText = "sp_SelectUserClaimByfk_User";
                cm.Parameters.AddWithValue("@UserId", criteria.UserId);
                using (var dr = new SafeDataReader(cm.ExecuteReader()))
                {
                    while (dr.Read())
                        Add(UserClaimEC.GetUserClaimEC(dr));
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