using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserEC : BusinessBase<UserEC>
    {
        #region Business Properties and Methods

        //declare members
        public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(o => o.UserName, RelationshipTypes.PrivateField);

        private string _userName = UserNameProperty.DefaultValue;

        [Required]
        [StringLength(255)]
        public string UserName
        {
            get => GetProperty(UserNameProperty, _userName);
            set => SetProperty(UserNameProperty, ref _userName, value);
        }

        public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(o => o.Password);

        [Required]
        [StringLength(128)]
        public string Password
        {
            get => GetProperty(PasswordProperty);
            set => SetProperty(PasswordProperty, value);
        }

        public static readonly PropertyInfo<bool> IsActiveProperty = RegisterProperty(p => p.IsActive, null, true, RelationshipTypes.PrivateField);

        private bool _isActive = IsActiveProperty.DefaultValue;

        public bool IsActive
        {
            get => GetProperty(IsActiveProperty, _isActive);
            set => SetProperty(IsActiveProperty, ref _isActive, value);
        }

        public static readonly PropertyInfo<bool> IsAutoUserProperty = RegisterProperty<bool>(p => p.IsAutoUser, RelationshipTypes.PrivateField);

        private bool _isAutoUser = IsAutoUserProperty.DefaultValue;

        public bool IsAutoUser
        {
            get => GetProperty(IsAutoUserProperty, _isAutoUser);
            set => SetProperty(IsAutoUserProperty, ref _isAutoUser, value);
        }

        public static readonly PropertyInfo<int> FkUserprofileProperty = RegisterProperty<int>(p => p.FkUserprofile, RelationshipTypes.PrivateField);

        private int _fkUserprofile = FkUserprofileProperty.DefaultValue;

        public int FkUserprofile
        {
            get => GetProperty(FkUserprofileProperty, _fkUserprofile);
            set => SetProperty(FkUserprofileProperty, ref _fkUserprofile, value);
        }

        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(o => o.Email);

        [EmailAddress]
        [StringLength(255)]
        public string Email
        {
            get => GetProperty(EmailProperty);
            set => SetProperty(EmailProperty, value);
        }

        public static readonly PropertyInfo<bool> EmailConfirmedProperty = RegisterProperty<bool>(p => p.EmailConfirmed, RelationshipTypes.PrivateField);

        private bool _emailConfirmed = EmailConfirmedProperty.DefaultValue;

        public bool EmailConfirmed
        {
            get => GetProperty(EmailConfirmedProperty, _emailConfirmed);
            set => SetProperty(EmailConfirmedProperty, ref _emailConfirmed, value);
        }

        public static readonly PropertyInfo<string> PhoneNumberProperty = RegisterProperty<string>(o => o.PhoneNumber);

        public string PhoneNumber
        {
            get => GetProperty(PhoneNumberProperty);
            set => SetProperty(PhoneNumberProperty, value);
        }

        public static readonly PropertyInfo<bool> PhoneNumberConfirmedProperty = RegisterProperty<bool>(p => p.PhoneNumberConfirmed, RelationshipTypes.PrivateField);

        private bool _phoneNumberConfirmed = PhoneNumberConfirmedProperty.DefaultValue;

        public bool PhoneNumberConfirmed
        {
            get => GetProperty(PhoneNumberConfirmedProperty, _phoneNumberConfirmed);
            set => SetProperty(PhoneNumberConfirmedProperty, ref _phoneNumberConfirmed, value);
        }

        public static readonly PropertyInfo<SmartDate> createdDateProperty = RegisterProperty(c => c.CreatedDate, null, new SmartDate(true), RelationshipTypes.PrivateField);

        protected SmartDate _createdDate = createdDateProperty.DefaultValue;

        [Required(ErrorMessage = "'CreatedDate' is required")]
        public DateTime CreatedDate => GetProperty(createdDateProperty, _createdDate.Date);

        public string CreatedDateString
        {
            get => GetPropertyConvert<SmartDate, string>(createdDateProperty, _createdDate);
            set => SetPropertyConvert(createdDateProperty, ref _createdDate, value);
        }

        public static readonly PropertyInfo<SmartDate> LastLoginDateProperty = RegisterProperty(c => c.LastLoginDate, null, new SmartDate(true), RelationshipTypes.PrivateField);

        protected SmartDate _LastLoginDate = LastLoginDateProperty.DefaultValue;

        [Required(ErrorMessage = "'LastLoginDate' is required")]
        public DateTime LastLoginDate => GetProperty(LastLoginDateProperty, _LastLoginDate.Date);

        public string LastLoginDateString
        {
            get => GetPropertyConvert<SmartDate, string>(LastLoginDateProperty, _LastLoginDate);
            set => SetPropertyConvert(LastLoginDateProperty, ref _LastLoginDate, value);
        }

        public static readonly PropertyInfo<SmartDate> LastPasswordChangedDateProperty = RegisterProperty(c => c.LastPasswordChangedDate, null, new SmartDate(true), RelationshipTypes.PrivateField);

        protected SmartDate _LastPasswordChangedDate = LastPasswordChangedDateProperty.DefaultValue;

        [Required(ErrorMessage = "'LastPasswordChangedDate' is required")]
        public DateTime LastPasswordChangedDate => GetProperty(LastPasswordChangedDateProperty, _LastPasswordChangedDate.Date);

        public string LastPasswordChangedDateString
        {
            get => GetPropertyConvert<SmartDate, string>(LastPasswordChangedDateProperty, _LastPasswordChangedDate);
            set => SetPropertyConvert(LastPasswordChangedDateProperty, ref _LastPasswordChangedDate, value);
        }

        public static readonly PropertyInfo<SmartDate> LastLockoutDateProperty = RegisterProperty(c => c.LastLockoutDate, null, new SmartDate(true), RelationshipTypes.PrivateField);

        protected SmartDate _LastLockoutDate = LastLockoutDateProperty.DefaultValue;

        [Required(ErrorMessage = "'LastLockoutDate' is required")]
        public DateTime LastLockoutDate => GetProperty(LastLockoutDateProperty, _LastLockoutDate.Date);

        public string LastLockoutDateString
        {
            get => GetPropertyConvert<SmartDate, string>(LastLockoutDateProperty, _LastLockoutDate);
            set => SetPropertyConvert(LastLockoutDateProperty, ref _LastLockoutDate, value);
        }

        public static readonly PropertyInfo<int> FailedPasswordAttemptCountProperty = RegisterProperty<int>(p => p.FailedPasswordAttemptCount, RelationshipTypes.PrivateField);

        private int _failedPasswordAttemptCount = FailedPasswordAttemptCountProperty.DefaultValue;

        public int FailedPasswordAttemptCount
        {
            get => GetProperty(FailedPasswordAttemptCountProperty, _failedPasswordAttemptCount);
            set => SetProperty(FailedPasswordAttemptCountProperty, ref _failedPasswordAttemptCount, value);
        }

        public static readonly PropertyInfo<string> CommentProperty = RegisterProperty<string>(p => p.Comment, RelationshipTypes.PrivateField);

        private string _comment = CommentProperty.DefaultValue;

        public string Comment
        {
            get => GetProperty(CommentProperty, _comment);
            set => SetProperty(CommentProperty, ref _comment, value);
        }

        public static readonly PropertyInfo<bool> MustChangePasswordProperty = RegisterProperty(p => p.MustChangePassword, null, true, RelationshipTypes.PrivateField);

        private bool _mustChangePassword = MustChangePasswordProperty.DefaultValue;

        public bool MustChangePassword
        {
            get => GetProperty(MustChangePasswordProperty, _mustChangePassword);
            set => SetProperty(MustChangePasswordProperty, ref _mustChangePassword, value);
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(p => p.FirstName, RelationshipTypes.PrivateField);

        private string _firstName = FirstNameProperty.DefaultValue;

        public string FirstName
        {
            get => GetProperty(FirstNameProperty, _firstName);
            set => SetProperty(FirstNameProperty, ref _firstName, value);
        }

        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(p => p.MiddleName, RelationshipTypes.PrivateField);

        private string _middleName = MiddleNameProperty.DefaultValue;

        public string MiddleName
        {
            get => GetProperty(MiddleNameProperty, _middleName);
            set => SetProperty(MiddleNameProperty, ref _middleName, value);
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(p => p.LastName, RelationshipTypes.PrivateField);

        private string _lastName = LastNameProperty.DefaultValue;

        public string LastName
        {
            get => GetProperty(LastNameProperty, _lastName);
            set => SetProperty(LastNameProperty, ref _lastName, value);
        }

        public static readonly PropertyInfo<int> PersonIdProperty = RegisterProperty<int>(p => p.PersonId, RelationshipTypes.PrivateField);

        private int _personId = PersonIdProperty.DefaultValue;

        public int PersonId
        {
            get => GetProperty(PersonIdProperty, _personId);
            set => SetProperty(PersonIdProperty, ref _personId, value);
        }

        public static readonly PropertyInfo<string> PositionProperty = RegisterProperty<string>(p => p.Position, RelationshipTypes.PrivateField);

        private string _position = PositionProperty.DefaultValue;

        public string Position
        {
            get => GetProperty(PositionProperty, _position);
            set => SetProperty(PositionProperty, ref _position, value);
        }

        public static readonly PropertyInfo<string> CodeSubjectsProperty = RegisterProperty<string>(p => p.CodeSubjects, RelationshipTypes.PrivateField);

        private string _codeSubjects = CodeSubjectsProperty.DefaultValue;

        public string CodeSubjects
        {
            get => GetProperty(CodeSubjectsProperty, _codeSubjects);
            set => SetProperty(CodeSubjectsProperty, ref _codeSubjects, value);
        }

        public static readonly PropertyInfo<bool> LockoutEnabledProperty = RegisterProperty(p => p.LockoutEnabled, null, true, RelationshipTypes.PrivateField);

        private bool _lockoutEnabled = LockoutEnabledProperty.DefaultValue;

        public bool LockoutEnabled
        {
            get => GetProperty(LockoutEnabledProperty, _lockoutEnabled);
            set => SetProperty(LockoutEnabledProperty, ref _lockoutEnabled, value);
        }

        public static readonly PropertyInfo<bool> TwoFactorEnabledProperty = RegisterProperty<bool>(p => p.TwoFactorEnabled, RelationshipTypes.PrivateField);

        private bool _twoFactorEnabled = TwoFactorEnabledProperty.DefaultValue;

        public bool TwoFactorEnabled
        {
            get => GetProperty(TwoFactorEnabledProperty, _twoFactorEnabled);
            set => SetProperty(TwoFactorEnabledProperty, ref _twoFactorEnabled, value);
        }

        public static readonly PropertyInfo<SmartDate> LockoutEndDateUtcProperty = RegisterProperty(c => c.LockoutEndDateUtc, null, new SmartDate(true), RelationshipTypes.PrivateField);

        protected SmartDate _lockoutEndDateUtc = LockoutEndDateUtcProperty.DefaultValue;

        [Required(ErrorMessage = "'LockoutEndDateUtc' is required")]
        public DateTime LockoutEndDateUtc
        {
            get => GetProperty(LockoutEndDateUtcProperty, _lockoutEndDateUtc.Date);
            set => SetPropertyConvert(LockoutEndDateUtcProperty, ref _lockoutEndDateUtc, value);
        }

        public string LockoutEndDateUtcString
        {
            get => GetPropertyConvert<SmartDate, string>(LockoutEndDateUtcProperty, _lockoutEndDateUtc);
            set => SetPropertyConvert(LockoutEndDateUtcProperty, ref _lockoutEndDateUtc, value);
        }

        public string LockoutEndDateLocalString => !string.IsNullOrEmpty(LockoutEndDateUtcString) ? _lockoutEndDateUtc.Date.ToLocalTime().ToString("g") : string.Empty;

        public static readonly PropertyInfo<string> SecurityStampProperty = RegisterProperty(o => o.SecurityStamp, null, Guid.NewGuid().ToString(), RelationshipTypes.PrivateField);

        private string _securityStamp = SecurityStampProperty.DefaultValue;

        public string SecurityStamp
        {
            get => GetProperty(SecurityStampProperty, _securityStamp);
            set => SetProperty(SecurityStampProperty, ref _securityStamp, value);
        }

        public string UserNameWithServiceProviderId => $"{UserName}|";

        protected override object GetIdValue()
        {
            return UserName;
        }

        #endregion //Business Properties and Methods

        #region Factory Methods

        public UserEC()
        {
            /* require use of factory method */
        }

        public static UserEC NewUserEC(string userName)
        {
            return DataPortal.Create<UserEC>(new Criteria(userName));
        }

        public static UserEC GetUserEC(string userName)
        {
            return DataPortal.Fetch<UserEC>(new Criteria(userName));
        }

        public static void DeleteUserEC(string userName)
        {
            DataPortal.Delete<UserEC>(new Criteria(userName));
        }

        #endregion //Factory Methods

        #region Child Factory Methods

        private UserEC(string userName)
        {
            _userName = userName;
        }

        internal static UserEC NewUserECChild(string userName)
        {
            var child = new UserEC(userName);
            child.BusinessRules.CheckRules();
            child.MarkAsChild();
            return child;
        }

        internal static UserEC GetUserEC(SafeDataReader dr)
        {
            var child = new UserEC();
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
            public readonly string UserName;

            public Criteria(string userName)
            {
                UserName = userName;
            }
        }

        #endregion //Criteria

        #region Data Access - Create

        [RunLocal]
        private void DataPortal_Create(Criteria criteria)
        {
            _userName = criteria.UserName;
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
                cm.CommandText = "sp_SelectUser";
                cm.Parameters.AddWithValue("@UserName", criteria.UserName);
                using (var dr = new SafeDataReader(cm.ExecuteReader()))
                {
                    dr.Read();
                    FetchObject(dr);
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
            } //using
        }

        #endregion //Data Access - Update

        #region Data Access - Delete

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new Criteria(UserName));
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
                cm.CommandText = "sp_DeleteUser";
                cm.Parameters.AddWithValue("@UserName", criteria.UserName);
                cm.ExecuteNonQuery();
            } //using
        }

        #endregion //Data Access - Delete

        #region Data Access - Fetch

        private void Fetch(SafeDataReader dr)
        {
            FetchObject(dr);
            MarkOld();
        }

        private void FetchObject(SafeDataReader dr)
        {
            _userName = dr.GetString("UserName");
            Password = dr.GetString("Password");
            _isActive = dr.GetBoolean("IsActive");
            _isAutoUser = dr.GetBoolean("IsAutoUser");
            _fkUserprofile = dr.GetInt32("fk_UserProfile");
            Email = dr.GetString("Email");
            _emailConfirmed = dr.GetBoolean("EmailConfirmed");
            PhoneNumber = dr.GetString("PhoneNumber");
            _phoneNumberConfirmed = dr.GetBoolean("PhoneNumberConfirmed");
            _createdDate = dr.GetSmartDate("CreatedDate", _createdDate.EmptyIsMin);
            _createdDate.FormatString = "g";
            _LastLoginDate = dr.GetSmartDate("LastLoginDate", _LastLoginDate.EmptyIsMin);
            _LastLoginDate.FormatString = "g";
            _LastPasswordChangedDate = dr.GetSmartDate("LastPasswordChangedDate", _LastPasswordChangedDate.EmptyIsMin);
            _LastPasswordChangedDate.FormatString = "g";
            _LastLockoutDate = dr.GetSmartDate("LastLockoutDate", _LastLockoutDate.EmptyIsMin);
            _LastLockoutDate.FormatString = "g";
            _lockoutEndDateUtc = dr.GetSmartDate("LockoutEndDateUtc", _lockoutEndDateUtc.EmptyIsMin);
            _lockoutEndDateUtc.FormatString = "g";
            _failedPasswordAttemptCount = dr.GetInt32("FailedPasswordAttemptCount");
            _comment = dr.GetString("Comment");
            _mustChangePassword = dr.GetBoolean("MustChangePassword");
            switch (UserName)
            {
                case "admin":
                    _firstName = "Administrator";
                    break;
                case "auto":
                    _firstName = "Auto bruker";
                    break;
                default:
                    _firstName = dr.GetString("FirstName");
                    break;
            }
            _middleName = dr.GetString("MiddleName");
            _lastName = dr.GetString("LastName");
            _personId = dr.GetInt32("ID");
            _position = dr.GetString("Position");
            _codeSubjects = dr.GetString("CodeSubjects");
            _lockoutEnabled = dr.GetBoolean("LockoutEnabled");
            _twoFactorEnabled = dr.GetBoolean("TwoFactorEnabled");
            _securityStamp = dr.GetString("SecurityStamp");
        }

        #endregion //Data Access - Fetch

        #region Data Access - Insert

        internal void Insert(SqlConnection cn)
        {
            if (!IsDirty)
                return;
            ExecuteInsert(cn);
            MarkOld();
        }

        private void ExecuteInsert(SqlConnection cn)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_InsertUser";
                AddInsertParameters(cm);
                cm.ExecuteNonQuery();
                _userName = (string) cm.Parameters["@UserName"].Value;
                _fkUserprofile = (int) cm.Parameters["@fk_UserProfile"].Value;
                _createdDate = DateTime.Now;
            } //using
        }

        private void AddInsertParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@UserName", UserName);
            cm.Parameters.AddWithValue("@Password", Password);
            cm.Parameters.AddWithValue("@IsActive", _isActive);
            cm.Parameters.AddWithValue("@IsAutoUser", _isAutoUser);
            cm.Parameters.AddWithValue("@MustChangePassword", _mustChangePassword);
            cm.Parameters.AddWithValue("@LockoutEnabled", _lockoutEnabled);
            if (Email.Length > 0)
                cm.Parameters.AddWithValue("@Email", Email);
            else
                cm.Parameters.AddWithValue("@Email", DBNull.Value);
            cm.Parameters.AddWithValue("@IsEmailConfirmed", _emailConfirmed);
            if (PhoneNumber.Length > 0)
                cm.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            else
                cm.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
            cm.Parameters.AddWithValue("@IsPhoneNumberConfirmed", _phoneNumberConfirmed);
            cm.Parameters.AddWithValue("@CreatedDate", _createdDate.DBValue);
            _createdDate.FormatString = "g";
            cm.Parameters.AddWithValue("@LastPasswordChangedDate", _LastPasswordChangedDate.DBValue);
            _LastPasswordChangedDate.FormatString = "g";
            cm.Parameters.AddWithValue("@LastLockoutDate", _LastLockoutDate.DBValue);
            _LastLockoutDate.FormatString = "g";
            cm.Parameters.AddWithValue("@LockoutEndDateUtc", _lockoutEndDateUtc.DBValue);
            _lockoutEndDateUtc.FormatString = "g";
            cm.Parameters.AddWithValue("@FailedPasswordAttemptCount", _failedPasswordAttemptCount);
            if (_comment.Length > 0)
                cm.Parameters.AddWithValue("@Comment", _comment);
            else
                cm.Parameters.AddWithValue("@Comment", DBNull.Value);
            if (_securityStamp.Length > 0)
                cm.Parameters.AddWithValue("@SecurityStamp", _securityStamp);
            else
                cm.Parameters.AddWithValue("@SecurityStamp", DBNull.Value);
            cm.Parameters.AddWithValue("@fk_UserProfile", _fkUserprofile);
            cm.Parameters["@fk_UserProfile"].Direction = ParameterDirection.InputOutput;
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
        }

        private void ExecuteUpdate(SqlConnection cn)
        {
            using (var cm = cn.CreateCommand())
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_UpdateUser";
                AddUpdateParameters(cm);
                cm.ExecuteNonQuery();
            } //using
        }

        private void AddUpdateParameters(SqlCommand cm)
        {
            cm.Parameters.AddWithValue("@UserName", UserName);
            cm.Parameters.AddWithValue("@Password", Password);
            cm.Parameters.AddWithValue("@IsActive", _isActive);
            cm.Parameters.AddWithValue("@IsAutoUser", _isAutoUser);
            cm.Parameters.AddWithValue("@MustChangePassword", _mustChangePassword);
            cm.Parameters.AddWithValue("@LockoutEnabled", _lockoutEnabled);
            if (Email.Length > 0)
                cm.Parameters.AddWithValue("@Email", Email);
            else
                cm.Parameters.AddWithValue("@Email", DBNull.Value);
            cm.Parameters.AddWithValue("@IsEmailConfirmed", _emailConfirmed);
            if (PhoneNumber.Length > 0)
                cm.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            else
                cm.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
            cm.Parameters.AddWithValue("@IsPhoneNumberConfirmed", _phoneNumberConfirmed);
            cm.Parameters.AddWithValue("@CreatedDate", _createdDate.DBValue);
            _createdDate.FormatString = "g";
            cm.Parameters.AddWithValue("@LastPasswordChangedDate", _LastPasswordChangedDate.DBValue);
            _LastPasswordChangedDate.FormatString = "g";
            cm.Parameters.AddWithValue("@LastLockoutDate", _LastLockoutDate.DBValue);
            _LastLockoutDate.FormatString = "g";
            cm.Parameters.AddWithValue("@FailedPasswordAttemptCount", _failedPasswordAttemptCount);
            cm.Parameters.AddWithValue("@LockoutEndDateUtc", _lockoutEndDateUtc.DBValue);
            _lockoutEndDateUtc.FormatString = "g";
            if (_comment.Length > 0)
                cm.Parameters.AddWithValue("@Comment", _comment);
            else
                cm.Parameters.AddWithValue("@Comment", DBNull.Value);
            if (_securityStamp.Length > 0)
                cm.Parameters.AddWithValue("@SecurityStamp", _securityStamp);
            else
                cm.Parameters.AddWithValue("@SecurityStamp", DBNull.Value);
            if (_fkUserprofile != 0)
                cm.Parameters.AddWithValue("@fk_UserProfile", _fkUserprofile);
            else
                cm.Parameters.AddWithValue("@fk_UserProfile", DBNull.Value);
        }

        #endregion //Data Access - Update

        #region Data Access - Delete

        internal void DeleteSelf(SqlConnection cn)
        {
            if (!IsDirty)
                return;
            if (IsNew)
                return;
            ExecuteDelete(cn, new Criteria(UserName));
            MarkNew();
        }

        #endregion //Data Access - Delete

        #endregion //Data Access
    }
}