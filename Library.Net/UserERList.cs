using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserERList : BusinessListBase<UserERList, UserEC>
    {
        #region Enum criteria type

        public enum CriteriaType
        {
            ExistUserName,
            AllUsers
        }

        #endregion

        #region Factory Methods

        public static UserERList NewUserERList()
        {
            return new UserERList();
        }

        public static UserERList GetUserERList()
        {
            return DataPortal.Fetch<UserERList>(new FilterCriteria());
        }

        public static UserERList GetUserERList(string userName)
        {
            return DataPortal.Fetch<UserERList>(new FilterCriteria(userName));
        }

        public static UserERList GetUserERList(string userName, string password)
        {
            // Used to prelogin user
            return DataPortal.Fetch<UserERList>(new FilterCriteria(userName, password));
        }

        #endregion //Factory Methods

        #region Data Access

        #region Filter Criteria

        [Serializable]
        private class FilterCriteria
        {
            public readonly CriteriaType CriteriaType;
            public readonly string Password;
            public readonly string UserName;

            public FilterCriteria()
            {
                UserName = string.Empty;
                CriteriaType = CriteriaType.AllUsers;
            }

            public FilterCriteria(string userName)
            {
                UserName = userName;
                CriteriaType = CriteriaType.ExistUserName;
            }

            public FilterCriteria(string userName, string password)
            {
                UserName = userName;
                Password = password;
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
                if (string.IsNullOrEmpty(criteria.UserName) && criteria.CriteriaType == CriteriaType.AllUsers)
                {
                    cm.CommandText = "sp_SelectUserAll";
                }
                else if (!string.IsNullOrEmpty(criteria.UserName) && string.IsNullOrEmpty(criteria.Password))
                {
                    cm.CommandText = "sp_SelectUserByUserName";
                    cm.Parameters.AddWithValue("@UserName", criteria.UserName);
                }
                else if (criteria.UserName.Length > 0 && criteria.Password.Length > 0)
                {
                    cm.CommandText = "sp_PreLogin";
                    cm.Parameters.AddWithValue("@UserName", criteria.UserName);
                    cm.Parameters.AddWithValue("@Password", criteria.Password);
                }

                if (!string.IsNullOrEmpty(cm.CommandText))
                    using (var dr = new SafeDataReader(cm.ExecuteReader()))
                    {
                        while (dr.Read())
                            Add(UserEC.GetUserEC(dr));
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