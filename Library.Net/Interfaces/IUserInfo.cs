using System;

namespace Library.Net
{
    public interface IUserInfo
    {
        string UserName { get; }
        string FullName { get; }
        string FullNameAndUserName { get; }
        string Email { get; }
        string Position { get; }
        bool IsActive { get; }
        DateTime LockoutEndDateUtc { get; }
        DateTime LastLoginDate { get; }
        DateTime CreatedDate { get; }
    }
}
