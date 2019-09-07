using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserClaim : BusinessBase<UserClaim>
    {
        #region Business Properties and Methods

        //declare members

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, RelationshipTypes.PrivateField);

        private int _id = IdProperty.DefaultValue;

        public int Id
        {
            get => GetProperty(IdProperty, _id);
            set => SetProperty(IdProperty, ref _id, value);
        }

        public static readonly PropertyInfo<string> UserIdProperty = RegisterProperty<string>(p => p.UserId, RelationshipTypes.PrivateField);

        private string _userId = UserIdProperty.DefaultValue;

        [Required]
        [StringLength(255)]
        public string UserId
        {
            get => GetProperty(UserIdProperty, _userId);
            set => SetProperty(UserIdProperty, ref _userId, value);
        }

        public static readonly PropertyInfo<string> ClaimTypeProperty = RegisterProperty<string>(p => p.ClaimType, RelationshipTypes.PrivateField);

        private string _claimType = ClaimTypeProperty.DefaultValue;

        public string ClaimType
        {
            get => GetProperty(ClaimTypeProperty, _claimType);
            set => SetProperty(ClaimTypeProperty, ref _claimType, value);
        }

        public static readonly PropertyInfo<string> ClaimValueProperty = RegisterProperty<string>(p => p.ClaimValue, RelationshipTypes.PrivateField);

        private string _claimValue = ClaimValueProperty.DefaultValue;

        public string ClaimValue
        {
            get => GetProperty(ClaimValueProperty, _claimValue);
            set => SetProperty(ClaimValueProperty, ref _claimValue, value);
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        #endregion //Business Properties and Methods

        #region Factory Methods

        public static UserClaim NewUserClaimEC()
        {
            return DataPortal.Create<UserClaim>();
        }

        public static UserClaim GetUserClaimEC(string userId, string claimValue, string claimType)
        {
            return DataPortal.Fetch<UserClaim>(new Criteria(userId, claimValue, claimType));
        }

        public static void DeleteUserClaimEC(string userId, string claimValue, string claimType)
        {
            DataPortal.Delete<UserClaim>(new Criteria(userId, claimValue, claimType));
        }

        #endregion //Factory Methods

        #region Child Factory Methods

        internal static UserClaim NewUserClaimECChild()
        {
            var child = new UserClaim();
            child.BusinessRules.CheckRules();
            child.MarkAsChild();
            return child;
        }

        internal static UserClaim GetUserClaimEC(SafeDataReader dr)
        {
            var child = new UserClaim();
            child.MarkAsChild();
            child.Fetch(dr);
            return child;
        }

        #endregion //Child Factory Methods

        #region Data Access

        #region Criteria

        [Serializable]
        private class Criteria
        {
            internal readonly string ClaimType;
            internal readonly string ClaimValue;
            internal readonly string UserId;

            public Criteria(string userId, string claimValue, string claimType)
            {
                UserId = userId;
                ClaimType = claimType;
                ClaimValue = claimValue;
            }
        }

        #endregion //Criteria

        #region Data Access - Create

        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        #endregion //Data Access - Create

        #region Data Access - Fetch

        private void DataPortal_Fetch(Criteria criteria)
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = ConnectionManager<SqlConnection>.GetManager(Helper.DatabaseConnectionString))
                {
                    ExecuteFetch(ctx.Connection, criteria);
                } //using
            }
        }

        private void ExecuteFetch(SqlConnection cn, Criteria criteria)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_SelectUserClaim";
                cm.Parameters.AddWithValue("@UserId", criteria.UserId);
                cm.Parameters.AddWithValue("@ClaimType", criteria.ClaimType);
                cm.Parameters.AddWithValue("@ClaimValue", criteria.ClaimValue);
                using (var dr = new SafeDataReader(cm.ExecuteReader()))
                {
                    dr.Read();
                    FetchObject(dr);
                    FetchChildren(dr);
                }
            } //using
        }

        #endregion //Data Access - Fetch

        #region Data Access - Insert

        protected override void DataPortal_Insert()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager(Helper.DatabaseConnectionString))
            {
                ExecuteInsert(ctx.Connection);
                //update child object(s)
                UpdateChildren(ctx.Connection);
            } //using
        }

        #endregion //Data Access - Insert

        #region Data Access - Update

        protected override void DataPortal_Update()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager(Helper.DatabaseConnectionString))
            {
                if (IsDirty)
                    ExecuteUpdate(ctx.Connection);
                //update child object(s)
                UpdateChildren(ctx.Connection);
            } //using
        }

        #endregion //Data Access - Update

        #region Data Access - Delete

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new Criteria(_userId, _claimValue, _claimType));
        }

        private void DataPortal_Delete(Criteria criteria)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager(Helper.DatabaseConnectionString))
            {
                ExecuteDelete(ctx.Connection, criteria);
            } //using
        }

        private void ExecuteDelete(SqlConnection cn, Criteria criteria)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_DeleteUserClaim";
                cm.Parameters.AddWithValue("@UserId", criteria.UserId);
                cm.Parameters.AddWithValue("@ClaimType", criteria.ClaimType);
                cm.Parameters.AddWithValue("@ClaimValue", criteria.ClaimValue);
                cm.ExecuteNonQuery();
            } //using
        }

        #endregion //Data Access - Delete

        #region Data Access - Fetch

        private void Fetch(SafeDataReader dr)
        {
            FetchObject(dr);
            MarkOld();
            FetchChildren(dr);
        }

        private void FetchObject(SafeDataReader dr)
        {
            _id = dr.GetInt32("ID");
            _userId = dr.GetString("UserId");
            _claimType = dr.GetString("ClaimType");
            _claimValue = dr.GetString("ClaimValue");
        }

        private void FetchChildren(SafeDataReader dr)
        {
        }

        #endregion //Data Access - Fetch

        #region Data Access - Insert

        internal void Insert(SqlConnection cn)
        {
            if (!IsDirty)
                return;
            ExecuteInsert(cn);
            MarkOld();
            //update child object(s)
            UpdateChildren(cn);
        }

        private void ExecuteInsert(SqlConnection cn)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_InsertUserClaim";
                AddInsertParameters(cm);
                cm.ExecuteNonQuery();
                _id = (int) cm.Parameters["@ID"].Value;
            } //using
        }

        private void AddInsertParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@UserId", _userId);
            cm.Parameters.AddWithValue("@ClaimType", _claimType);
            cm.Parameters.AddWithValue("@ClaimValue", _claimValue);
            cm.Parameters.AddWithValue("@ID", _id);
            cm.Parameters["@ID"].Direction = ParameterDirection.Output;
        }

        #endregion //Data Access - Insert

        #region Data Access - Update

        internal void Update(SqlConnection cn)
        {
            if (!IsDirty)
                return;

            if (IsDirty)
            {
                ExecuteUpdate(cn);
                MarkOld();
            }

            //update child object(s)
            UpdateChildren(cn);
        }

        private void ExecuteUpdate(SqlConnection cn)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_UpdateUserClaim";
                AddUpdateParameters(cm);
                cm.ExecuteNonQuery();
            } //using
        }

        private void AddUpdateParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@ID", _id);
            cm.Parameters.AddWithValue("@UserId", _userId);
            cm.Parameters.AddWithValue("@ClaimType", _claimType);
            cm.Parameters.AddWithValue("@ClaimValue", _claimValue);
        }

        private void UpdateChildren(SqlConnection cn)
        {
        }

        #endregion //Data Access - Update

        #region Data Access - Delete

        internal void DeleteSelf(SqlConnection cn)
        {
            if (!IsDirty)
                return;
            if (IsNew)
                return;

            ExecuteDelete(cn, new Criteria(_userId, _claimValue, _claimType));
            MarkNew();
        }

        #endregion //Data Access - Delete

        #endregion //Data Access
    }
}