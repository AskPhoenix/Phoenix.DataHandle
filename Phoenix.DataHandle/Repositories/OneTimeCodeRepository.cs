using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;
using Phoenix.DataHandle.Utilities;
using System;
using System.Linq;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class OneTimeCodeRepository : Repository<OneTimeCode>
    {
        public OneTimeCodeRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Search

        public IQueryable<OneTimeCode> Search(int userId)
        {
            return Find().Where(otc => otc.UserId == userId);
        }

        #endregion

        #region Generate

        public static OneTimeCode Generate(int userId, OneTimeCodePurpose purpose, int durationInMinutes)
        {
            OTCGeneratorOptions otcOptions = purpose switch
            {
                OneTimeCodePurpose.Verification => new() { Length = 4, IsAlphanumeric = false },
                OneTimeCodePurpose.Identification => new() { Length = 4, IsAlphanumeric = true},
                _ => new()
            };

            return new()
            {
                UserId = userId,
                Token = OTCGenerator.Generate(otcOptions),
                Purpose = purpose,
                ExpiresAt = DateTime.UtcNow.AddMinutes(durationInMinutes),
            };
        }

        #endregion
    }
}
