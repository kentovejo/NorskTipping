using System;
using Csla;
using Csla.Data;

namespace Library.Net
{
    [Serializable]
    public class UserROC : UserReadBase<UserROC>
    {
        #region Business Properties and Methods

        //declare members
        private SmartDate _lastPasswordChangedDate = new SmartDate(true);

        public string PasswordHash { get; private set; } = string.Empty;

        public bool IsAutoUser { get; private set; }

        public bool MustChangePassword { get; private set; }

        public int FkUserprofile { get; private set; }

        public string CreatedDateString => _createdDate.Text;

        public string LastLoginDateString => _lastLoginDate.Text;

        public DateTime LastPasswordChangedDate => _lastPasswordChangedDate.Date;

        public string LastPasswordChangedDateString => _lastPasswordChangedDate.Text;

        public string LastLockoutDateString => _lastLockoutDate.Text;

        public int FailedPasswordAttemptCount { get; private set; }

        public string Comment { get; private set; } = string.Empty;

        public string UserNameWithServiceProviderId => $"{UserName}|";

        public bool EmailConfirmed { get; private set; }

        public string PhoneNumber { get; private set; } = string.Empty;

        public bool PhoneNumberConfirmed { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public bool LockoutEnabled { get; private set; }

        public string SecurityStamp { get; private set; } = string.Empty;

        public int AccessFailedCount => FailedPasswordAttemptCount;

        protected override object GetIdValue()
        {
            return UserName;
        }

        #endregion //Business Properties and Methods

        #region Factory Methods

        internal static UserROC GetUserROC(SafeDataReader dr)
        {
            return new UserROC(dr);
        }

        private UserROC(SafeDataReader dr)
        {
            Fetch(dr);
        }

        #endregion //Factory Methods

        #region Data Access

        #region Data Access - Fetch

        private void Fetch(SafeDataReader dr)
        {
            FetchObject(dr);
            //load child object(s)
            FetchChildren(dr);
        }

        private void FetchObject(SafeDataReader dr)
        {
            Id = dr.GetString("UserName");
            PasswordHash = dr.GetString("Password");
            IsActive = dr.GetBoolean("IsActive");
            IsAutoUser = dr.GetBoolean("IsAutoUser");
            MustChangePassword = dr.GetBoolean("MustChangePassword");
            FkUserprofile = dr.GetInt32("fk_UserProfile");
            Email = dr.GetString("Email");
            EmailConfirmed = dr.GetBoolean("EmailConfirmed");
            PhoneNumber = dr.GetString("PhoneNumber");
            PhoneNumberConfirmed = dr.GetBoolean("PhoneNumberConfirmed");
            _createdDate = dr.GetSmartDate("CreatedDate", _createdDate.EmptyIsMin);
            _createdDate.FormatString = "g";
            _lastLoginDate = dr.GetSmartDate("LastLoginDate", _lastLoginDate.EmptyIsMin);
            _lastLoginDate.FormatString = "g";
            _lastPasswordChangedDate = dr.GetSmartDate("LastPasswordChangedDate", _lastPasswordChangedDate.EmptyIsMin);
            _lastPasswordChangedDate.FormatString = "g";
            _lastLockoutDate = dr.GetSmartDate("LastLockoutDate", _lastLockoutDate.EmptyIsMin);
            _lastLockoutDate.FormatString = "g";
            FailedPasswordAttemptCount = dr.GetInt32("FailedPasswordAttemptCount");
            Comment = dr.GetString("Comment");
            Position = dr.GetString("Position");
            switch (UserName)
            {
                case "admin":
                    FirstName = "Administrator";
                    break;
                case "auto":
                    FirstName = "Auto bruker";
                    break;
                default:
                    FirstName = dr.GetString("FirstName");
                    break;
            }
            MiddleName = dr.GetString("MiddleName");
            LastName = dr.GetString("LastName");
            CodeSubjects = dr.GetString("CodeSubjects");

            LockoutEnabled = dr.GetBoolean("LockoutEnabled");
            TwoFactorEnabled = dr.GetBoolean("TwoFactorEnabled");
            SecurityStamp = dr.GetString("SecurityStamp");
            _lockoutEndDateUtc = dr.GetSmartDate("LockoutEndDateUtc", _lockoutEndDateUtc.EmptyIsMin);
            _lockoutEndDateUtc.FormatString = "g";
        }

        private void FetchChildren(SafeDataReader dr)
        {
        }

        public UserROC()
        {
        }

        #endregion //Data Access - Fetch

        #endregion //Data Access
    }
}