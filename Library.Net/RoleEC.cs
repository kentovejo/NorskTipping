using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class RoleEC : BusinessBase<RoleEC>
    {
        #region Business Properties and Methods

        //declare members

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, RelationshipTypes.PrivateField);

        private string _name = NameProperty.DefaultValue;

        [Required]
        [StringLength(255)]
        public string Name
        {
            get => GetProperty(NameProperty, _name);
            set => SetProperty(NameProperty, ref _name, value);
        }

        public static readonly PropertyInfo<bool> IsActiveProperty = RegisterProperty<bool>(p => p.IsActive, RelationshipTypes.PrivateField);

        private bool _isActive = IsActiveProperty.DefaultValue;

        public bool IsActive
        {
            get => GetProperty(IsActiveProperty, _isActive);
            set => SetProperty(IsActiveProperty, ref _isActive, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description, RelationshipTypes.PrivateField);

        private string _description = DescriptionProperty.DefaultValue;

        [StringLength(255)]
        public string Description
        {
            get => GetProperty(DescriptionProperty, _description);
            set => SetProperty(DescriptionProperty, ref _description, value);
        }

        protected override object GetIdValue()
        {
            return _name;
        }

        #endregion //Business Properties and Methods

        #region Factory Methods

        public RoleEC()
        {
            /* require use of factory method */
        }

        public static RoleEC NewRoleEC(string name)
        {
            return DataPortal.Create<RoleEC>(new Criteria(name));
        }

        public static RoleEC GetRoleEC(string name)
        {
            return DataPortal.Fetch<RoleEC>(new Criteria(name));
        }

        public static void DeleteRoleEC(string name)
        {
            DataPortal.Delete<RoleEC>(new Criteria(name));
        }

        #endregion //Factory Methods

        #region Child Factory Methods

        private RoleEC(string name)
        {
            _name = name;
        }

        internal static RoleEC NewRoleECChild(string name)
        {
            var child = new RoleEC(name);
            child.BusinessRules.CheckRules();
            child.MarkAsChild();
            return child;
        }

        internal static RoleEC GetRoleEC(SafeDataReader dr)
        {
            var child = new RoleEC();
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
            public readonly string Name;

            public Criteria(string name)
            {
                Name = name;
            }
        }

        #endregion //Criteria

        #region Data Access - Create

        [RunLocal]
        private void DataPortal_Create(Criteria criteria)
        {
            _name = criteria.Name;
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
                }
            }
        }

        private void ExecuteFetch(SqlConnection cn, Criteria criteria)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_SelectRole";
                cm.Parameters.AddWithValue("@Name", criteria.Name);
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
            DataPortal_Delete(new Criteria(_name));
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
                cm.CommandText = "sp_DeleteRole";
                cm.Parameters.AddWithValue("@Name", criteria.Name);
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
            _name = dr.GetString("Name");
            _isActive = dr.GetBoolean("IsActive");
            _description = dr.GetString("Description");
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
                cm.CommandText = "sp_InsertRole";
                AddInsertParameters(cm);
                cm.ExecuteNonQuery();
            } //using
        }

        private void AddInsertParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@Name", _name);
            if (_isActive)
                cm.Parameters.AddWithValue("@IsActive", _isActive);
            else
                cm.Parameters.AddWithValue("@IsActive", DBNull.Value);
            if (_description.Length > 0)
                cm.Parameters.AddWithValue("@Description", _description);
            else
                cm.Parameters.AddWithValue("@Description", DBNull.Value);
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
                cm.CommandText = "sp_UpdateRole";
                AddUpdateParameters(cm);
                cm.ExecuteNonQuery();
            } //using
        }

        private void AddUpdateParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@Name", _name);
            if (_isActive)
                cm.Parameters.AddWithValue("@IsActive", _isActive);
            else
                cm.Parameters.AddWithValue("@IsActive", DBNull.Value);
            if (_description.Length > 0)
                cm.Parameters.AddWithValue("@Description", _description);
            else
                cm.Parameters.AddWithValue("@Description", DBNull.Value);
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

            ExecuteDelete(cn, new Criteria(_name));
            MarkNew();
        }

        #endregion //Data Access - Delete

        #endregion //Data Access
    }
}