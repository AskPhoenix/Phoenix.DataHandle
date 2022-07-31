using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main.Entities;
using System.Security.Cryptography;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUser : IdentityUser<int>, IApplicationUser
    {
        public ApplicationUser()
            : base()
        {
            Claims = new HashSet<ApplicationUserClaim>();
            Logins = new HashSet<ApplicationUserLogin>();
            Tokens = new HashSet<ApplicationUserToken>();
            UserRoles = new HashSet<ApplicationUserRole>();
        }

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationUser Normalize()
        {
            this.NormalizedEmail = this.Email?.ToUpperInvariant();
            this.NormalizedUserName = this.UserName?.ToUpperInvariant();

            return this;
        }

        public string GetHashSignature()
        {
            byte[] salt = new byte[16];
            using var pbkdf2 = new Rfc2898DeriveBytes(this.Id + this.PhoneNumber, salt, 10 * 1000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            string savedPasswordHash = Convert.ToBase64String(hash);

            return savedPasswordHash;
        }

        public bool VerifyHashSignature(string hashSignature)
        {
            return hashSignature == this.GetHashSignature();
        }
    }
}