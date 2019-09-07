using System;
using Csla;

namespace Library.Net
{
    [Serializable]
    public class UserReadBase<T> : ReadOnlyBase<T>, IUserInfoRead where T : UserReadBase<T>, IUserInfoRead
    {
        protected SmartDate _createdDate = new SmartDate(true);
        protected SmartDate _lastLoginDate = new SmartDate(true);
        protected SmartDate _lastLockoutDate = new SmartDate(true);
        protected SmartDate _lockoutEndDateUtc = new SmartDate(true);

        // ASP.NET IDENTITY FIELDS
        public string Id { get; protected set; } = UserNameProperty.DefaultValue;
        public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(o => o.UserName, RelationshipTypes.PrivateField);
        public string UserName => GetProperty(UserNameProperty, Id);
        public string Position { get; protected set; } = string.Empty;
        public string FirstName { get; protected set; } = string.Empty;
        public string MiddleName { get; protected set; } = string.Empty;
        public string LastName { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;
        public DateTime CreatedDate => _createdDate.Date;
        public DateTime LastLoginDate => _lastLoginDate.Date;
        public DateTime LastLockoutDate => _lastLockoutDate.Date;
        public DateTime LockoutEndDateUtc => _lockoutEndDateUtc.Date;
        public bool IsActive { get; protected set; }

        public string FullName => throw new NotImplementedException();

        public string FullNameAndUserName => throw new NotImplementedException();
    }
}
