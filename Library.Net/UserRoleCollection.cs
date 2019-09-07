using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserRoleCollection : BusinessListBase<UserRoleCollection, UserRoleEC>
    {
        public UserRoleEC GetItem(string fkUser, string fkRole)
        {
            return this.FirstOrDefault(item => item.FkUser == fkUser && item.FkRole == fkRole);
        }

        #region BindingList Overrides

        //protected new object AddNewCore()
        //{
        //    var item = UserRoleEC.NewUserRoleEC();
        //    Add(item);
        //    return item;
        //}

        #endregion //BindingList Overrides

        #region Factory Methods

        public static UserRoleCollection NewUserRoleERList()
        {
            return new UserRoleCollection();
        }

        public static UserRoleCollection GetUserRoleERList(string userName)
        {
            return GetUserRoleERList(userName, false);
        }

        public static UserRoleCollection GetUserRoleERList(string userName, bool showOnlySelected)
        {
            return DataPortal.Fetch<UserRoleCollection>(new FilterCriteria(userName, showOnlySelected));
        }

        #endregion //Factory Methods

        #region Data Access

        #region Filter Criteria

        [Serializable]
        private class FilterCriteria
        {
            public readonly string FkUser;
            public readonly bool ShowOnlySelected;

            public FilterCriteria(string fkUser, bool showOnlySelected)
            {
                FkUser = fkUser;
                ShowOnlySelected = showOnlySelected;
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
                cm.CommandText = "sp_SelectUserRoleByfk_User";
                cm.Parameters.AddWithValue("@UserName", criteria.FkUser);
                cm.Parameters.AddWithValue("@ShowOnlySelected", criteria.ShowOnlySelected);
                using (var dr = new SafeDataReader(cm.ExecuteReader()))
                {
                    while (dr.Read())
                        Add(UserRoleEC.GetUserRoleEC(dr));
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
                        child.Insert(ctx.Connection, this);
                    else
                        child.Update(ctx.Connection, this);
            } //using SqlConnection

            RaiseListChangedEvents = true;
        }

        #endregion //Data Access - Update

        #endregion //Data Access
    }
}