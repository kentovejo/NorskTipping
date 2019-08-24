namespace Security.Identity
{
    public class IdentityUserRole
    {
        public virtual IdentityRole Role { get; set; }
        public virtual string RoleId { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual string UserId { get; set; }
    }
}