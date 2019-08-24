using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserRORList : ReadOnlyListBase<UserRORList, UserROC>
    {
        #region Enum criteria type

        public enum CriteriaType
        {
            None,
            ByUserGroup,
            ByDepartment
        }

        #endregion

        #region Factory Methods

        public static UserRORList GetUserRORList(bool inactive = true)
        {
            return GetUserRORList(CriteriaType.None, 0, inactive);
        }

        public static UserRORList GetUserRORList(CriteriaType type, int id, bool inactive = true)
        {
            return DataPortal.Fetch<UserRORList>(new FilterCriteria(type, id, inactive));
        }

        #endregion //Factory Methods

        #region Data Access

        #region Filter Criteria

        [Serializable]
        private class FilterCriteria
        {
            public readonly int Id;
            public readonly bool Inactive;
            public readonly CriteriaType Type;

            public FilterCriteria(CriteriaType type, int id, bool inactive)
            {
                Type = type;
                Id = id;
                Inactive = inactive;
            }
        }

        #endregion //Filter Criteria

        #region Data Access - Fetch

        private void DataPortal_Fetch(FilterCriteria criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (var ctx = ConnectionManager<SqlConnection>.GetManager(Helper.DatabaseConnectionString))
            {
                ExecuteFetch(ctx.Connection, criteria);
            } //using

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        private void ExecuteFetch(SqlConnection cn, FilterCriteria criteria)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                switch (criteria.Type)
                {
                    case CriteriaType.ByUserGroup:
                        cm.CommandText = "sp_SelectUserPersonByfk_UserGroup";
                        cm.Parameters.AddWithValue("@fk_UserGroup", criteria.Id);
                        break;
                    case CriteriaType.ByDepartment:
                        cm.CommandText = "sp_SelectUserPersonByfk_Department";
                        cm.Parameters.AddWithValue("@fk_Department", criteria.Id);
                        break;
                    default:
                        cm.CommandText = "sp_SelectUserPersonAll";
                        break;
                }

                // Common parameter
                // - Used for sorting
                cm.Parameters.AddWithValue("@DisplayLastNameFirst", "0");
                // Common parameter
                // - Show all if inactive is true
                cm.Parameters.AddWithValue("@IncludeInactive", criteria.Inactive);
                using (var dr = new SafeDataReader(cm.ExecuteReader()))
                {
                    while (dr.Read())
                        Add(UserROC.GetUserROC(dr));
                }
            } //using
        }

        #endregion //Data Access - Fetch

        #endregion //Data Access
    }
}