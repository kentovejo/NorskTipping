using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserLoginERList : BusinessListBase<UserLoginERList, UserLoginEC>
    {
        #region BindingList Overrides

        protected new object AddNewCore()
        {
            var item = UserLoginEC.NewUserLoginEC();
            Add(item);
            return item;
        }

        #endregion //BindingList Overrides

        #region Factory Methods

        public static UserLoginERList NewUserLoginERList()
        {
            return new UserLoginERList();
        }

        public static UserLoginERList GetUserLoginERList(string userId)
        {
            return DataPortal.Fetch<UserLoginERList>(new FilterCriteria(userId));
        }

        public static UserLoginERList GetUserLoginERList(string loginProvider, string providerKey)
        {
            return DataPortal.Fetch<UserLoginERList>(new FilterCriteria(loginProvider, providerKey));
        }

        #endregion //Factory Methods

        #region Data Access

        #region Filter Criteria

        [Serializable]
        private class FilterCriteria
        {
            internal readonly string LoginProvider;
            internal readonly string ProviderKey;
            internal readonly string UserId;

            public FilterCriteria(string userId)
            {
                UserId = userId;
            }

            public FilterCriteria(string loginProvider, string providerKey)
            {
                LoginProvider = loginProvider;
                ProviderKey = providerKey;
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
                if (string.IsNullOrEmpty(criteria.UserId))
                {
                    cm.CommandText = "sp_SelectUserLogin";
                    cm.Parameters.AddWithValue("@LoginProvider", criteria.LoginProvider);
                    cm.Parameters.AddWithValue("@ProviderKey", criteria.ProviderKey);
                }
                else
                {
                    cm.CommandText = "sp_SelectUserLoginByfk_User";
                    cm.Parameters.AddWithValue("@UserId", criteria.UserId);
                }
                using (var dr = new SafeDataReader(cm.ExecuteReader()))
                {
                    while (dr.Read())
                        Add(UserLoginEC.GetUserLoginEC(dr));
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