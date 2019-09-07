using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserRoleEC : BusinessBase<UserRoleEC>
    {
        #region Business Properties and Methods

        public static readonly PropertyInfo<string> FkUserProperty = RegisterProperty<string>(p => p.FkUser, RelationshipTypes.PrivateField);

        private string _fkUser = FkUserProperty.DefaultValue;

        public string FkUser
        {
            get => GetProperty(FkUserProperty, _fkUser);
            set => SetProperty(FkUserProperty, ref _fkUser, value);
        }

        public static readonly PropertyInfo<string> FkRoleProperty = RegisterProperty<string>(p => p.FkRole, RelationshipTypes.PrivateField);

        private string _fkRole = FkRoleProperty.DefaultValue;

        public string FkRole
        {
            get => GetProperty(FkRoleProperty, _fkRole);
            set => SetProperty(FkRoleProperty, ref _fkRole, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, RelationshipTypes.PrivateField);

        private string _name = NameProperty.DefaultValue;

        public string Name
        {
            get => GetProperty(NameProperty, _name);
            set => SetProperty(NameProperty, ref _name, value);
        }

        public static readonly PropertyInfo<bool> SelectedProperty = RegisterProperty<bool>(p => p.Selected, RelationshipTypes.PrivateField);

        private bool _selected = SelectedProperty.DefaultValue;

        public bool Selected
        {
            get => GetProperty(SelectedProperty, _selected);
            set => SetProperty(SelectedProperty, ref _selected, value);
        }

        protected override object GetIdValue()
        {
            return $"{_fkUser}:{_fkRole}";
        }

        #endregion //Business Properties and Methods

        #region Factory Methods

        internal static UserRoleEC NewUserRoleEC()
        {
            return new UserRoleEC();
        }

        internal static UserRoleEC GetUserRoleEC(SafeDataReader dr)
        {
            return new UserRoleEC(dr);
        }

        public UserRoleEC()
        {
            BusinessRules.CheckRules();
            MarkAsChild();
        }

        private UserRoleEC(SafeDataReader dr)
        {
            MarkAsChild();
            Fetch(dr);
        }

        #endregion //Factory Methods

        #region Data Access

        #region Data Access - Fetch

        private void Fetch(SafeDataReader dr)
        {
            FetchObject(dr);
            MarkOld();
        }

        private void FetchObject(SafeDataReader dr)
        {
            _fkUser = dr.GetString("fk_User");
            _fkRole = dr.GetString("fk_Role");
            _name = dr.GetString("Name");
            _selected = dr.GetBoolean("Selected");
        }

        #endregion //Data Access - Fetch

        #region Data Access - Insert

        internal void Insert(SqlConnection cn, UserRoleCollection parent)
        {
            throw new NotImplementedException("Insert not allowed");
            //if (!IsDirty) return;

            //ExecuteInsert(cn, parent);
            //MarkOld();

            ////update child object(s)
            //UpdateChildren(ctx.Connection);
        }

        //private void ExecuteInsert(SqlConnection cn, UserRoleERList parent)
        //{
        //    using (SqlCommand cm = cn.CreateCommand())
        //    {
        //        cm.CommandType = CommandType.StoredProcedure;
        //        cm.CommandText = "sp_InsertUserRole";

        //        AddInsertParameters(cm, parent);

        //        if (log.IsDebugEnabled) 
        //{
        //log.Debug(cm.CommandText + ":" + CslaLibrary.Helper.ParametersToString(cm.Parameters));
        //cm.ExecuteNonQuery();
        //}
        //        _id = (int)cm.Parameters["@NewID"].Value;
        //    }//using
        //}

        //private void AddInsertParameters(SqlCommand cm, UserRoleERList parent)
        //{
        //    //TODO: if parent use identity key, fix fk member with value from parent
        //    cm.Parameters.AddWithValue("@fk_User", _fkUser);
        //    cm.Parameters.AddWithValue("@fk_Role", _fkRole);
        //    //cm.Parameters.AddWithValue("@CreatedDate", _createdDate.DBValue);
        //    cm.Parameters.AddWithValue("@NewID", _id);
        //    cm.Parameters["@NewID"].Direction = ParameterDirection.Output;
        //}

        #endregion //Data Access - Insert

        #region Data Access - Update

        internal void Update(SqlConnection cn, UserRoleCollection parent)
        {
            if (!IsDirty)
                return;

            if (IsDirty)
            {
                ExecuteUpdate(cn, parent);
                MarkOld();
            }

            //update child object(s)
            UpdateChildren(cn);
        }

        private void ExecuteUpdate(SqlConnection cn, UserRoleCollection parent)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_UpdateUserRole";
                AddUpdateParameters(cm, parent);
                cm.ExecuteNonQuery();
            } //using
        }

        private void AddUpdateParameters(SqlCommand cm, UserRoleCollection parent)
        {
            cm.Parameters.AddWithValue("@fk_User", _fkUser);
            cm.Parameters.AddWithValue("@fk_Role", _fkRole);
            cm.Parameters.AddWithValue("@Selected", _selected);
            //cm.Parameters.AddWithValue("@CreatedDate", _createdDate.DBValue);
        }

        private void UpdateChildren(SqlConnection cn)
        {
        }

        #endregion //Data Access - Update

        #region Data Access - Delete

        internal void DeleteSelf(SqlConnection cn)
        {
            throw new NotImplementedException("Delete not allowed");

            //if (!IsDirty) return;
            //if (IsNew) return;

            //ExecuteDelete(cn);
            //MarkNew();
        }

        //private void ExecuteDelete(SqlConnection cn)
        //{
        //    using (SqlCommand cm = cn.CreateCommand())
        //    {
        //        cm.CommandType = CommandType.StoredProcedure;
        //        cm.CommandText = "sp_DeleteUserRole";

        //        cm.Parameters.AddWithValue("@fk_User", _fkUser);
        //        cm.Parameters.AddWithValue("@fk_Role", _fkRole);

        //        if (log.IsDebugEnabled) log.Debug(cm.CommandText + ":" + CslaLibrary.Helper.ParametersToString(cm.Parameters));
        //{
        //log.Debug(cm.CommandText + ":" + CslaLibrary.Helper.ParametersToString(cm.Parameters));
        //cm.ExecuteNonQuery();
        //}        //    }//using
        //}

        #endregion //Data Access - Delete

        #endregion //Data Access
    }
}