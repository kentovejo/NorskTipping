using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserLoginEC : BusinessBase<UserLoginEC>
    {
        #region Business Properties and Methods

        //declare members
        public static readonly PropertyInfo<string> UserIdProperty = RegisterProperty<string>(p => p.UserId, RelationshipTypes.PrivateField);

        private string _userId = UserIdProperty.DefaultValue;

        [Required]
        [StringLength(255)]
        public string UserId
        {
            get => GetProperty(UserIdProperty, _userId);
            set => SetProperty(UserIdProperty, ref _userId, value);
        }

        public static readonly PropertyInfo<string> LoginProviderProperty = RegisterProperty<string>(p => p.LoginProvider, RelationshipTypes.PrivateField);

        private string _loginProvider = LoginProviderProperty.DefaultValue;

        public string LoginProvider
        {
            get => GetProperty(LoginProviderProperty, _loginProvider);
            set => SetProperty(LoginProviderProperty, ref _loginProvider, value);
        }

        public static readonly PropertyInfo<string> ProviderKeyProperty = RegisterProperty<string>(p => p.ProviderKey, RelationshipTypes.PrivateField);

        private string _providerKey = ProviderKeyProperty.DefaultValue;

        public string ProviderKey
        {
            get => GetProperty(ProviderKeyProperty, _providerKey);
            set => SetProperty(ProviderKeyProperty, ref _providerKey, value);
        }

        protected override object GetIdValue()
        {
            return $"{_userId}:{_loginProvider}:{_providerKey}";
        }

        #endregion //Business Properties and Methods

        #region Factory Methods

        public static UserLoginEC NewUserLoginEC()
        {
            return DataPortal.Create<UserLoginEC>();
        }

        public static UserLoginEC GetUserLoginEC(string userId, string providerKey, string loginProvider)
        {
            return DataPortal.Fetch<UserLoginEC>(new Criteria(userId, providerKey, loginProvider));
        }

        public static void DeleteUserLoginEC(string userId, string providerKey, string loginProvider)
        {
            DataPortal.Delete<UserLoginEC>(new Criteria(userId, providerKey, loginProvider));
        }

        #endregion //Factory Methods

        #region Child Factory Methods

        internal static UserLoginEC NewUserLoginECChild()
        {
            var child = new UserLoginEC();
            child.BusinessRules.CheckRules();
            child.MarkAsChild();
            return child;
        }

        internal static UserLoginEC GetUserLoginEC(SafeDataReader dr)
        {
            var child = new UserLoginEC();
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
            internal readonly string LoginProvider;
            internal readonly string ProviderKey;
            internal readonly string UserId;

            public Criteria(string userId, string providerKey, string loginProvider)
            {
                UserId = userId;
                LoginProvider = loginProvider;
                ProviderKey = providerKey;
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
                cm.CommandText = "sp_SelectUserLogin";
                cm.Parameters.AddWithValue("@LoginProvider", criteria.LoginProvider);
                cm.Parameters.AddWithValue("@ProviderKey", criteria.ProviderKey);
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
            DataPortal_Delete(new Criteria(_userId, _providerKey, _loginProvider));
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
                cm.CommandText = "sp_DeleteUserLogin";
                cm.Parameters.AddWithValue("@UserId", criteria.UserId);
                cm.Parameters.AddWithValue("@LoginProvider", criteria.LoginProvider);
                cm.Parameters.AddWithValue("@ProviderKey", criteria.ProviderKey);
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
            _userId = dr.GetString("UserId");
            _loginProvider = dr.GetString("LoginProvider");
            _providerKey = dr.GetString("ProviderKey");
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
                cm.CommandText = "sp_InsertUserLogin";
                AddInsertParameters(cm);
                cm.ExecuteNonQuery();
            } //using
        }

        private void AddInsertParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@UserId", _userId);
            cm.Parameters.AddWithValue("@ProviderKey", _providerKey);
            cm.Parameters.AddWithValue("@LoginProvider", _loginProvider);
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
                cm.CommandText = "sp_UpdateUserLogin";

                AddUpdateParameters(cm);
                cm.ExecuteNonQuery();
            } //using
        }

        private void AddUpdateParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@UserId", _userId);
            cm.Parameters.AddWithValue("@LoginProvider", _loginProvider);
            cm.Parameters.AddWithValue("@ProviderKey", _providerKey);
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

            ExecuteDelete(cn, new Criteria(_userId, _providerKey, _loginProvider));
            MarkNew();
        }

        #endregion //Data Access - Delete

        #endregion //Data Access
    }
}