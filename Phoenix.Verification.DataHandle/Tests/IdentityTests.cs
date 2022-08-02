using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.Verification.DataHandle.Tests
{
    public class IdentityTests : ContextTestsBase
    {
        private readonly ApplicationStore _applicationStore;

        private readonly string PHONE_NUMBER;
        private readonly string PROVIDER_KEY;

        public IdentityTests()
            : base()
        {
            _applicationStore = new(_applicationContext);

            PHONE_NUMBER = _configuration["IdentityTests:TestPhoneNumber"];
            PROVIDER_KEY = _configuration["IdentityTests:TestProviderKey"];
        }

        [Fact]
        public async void FindByPhoneNumber()
        {
            var user = await _applicationStore.FindByPhoneNumberAsync(PHONE_NUMBER);

            Assert.NotNull(user);
        }

        [Fact]
        public async void FindByProviderKey()
        {
            string provider = ChannelProvider.Facebook.ToString();
            var user = await _applicationStore.FindByProviderKeyAsync(provider, PROVIDER_KEY);

            Assert.NotNull(user);
        }

        [Fact]
        public async void GetRolesAsync()
        {
            var user = await _applicationStore.FindByPhoneNumberAsync(PHONE_NUMBER);
            if (user is null)
                return;

            var roles = await _applicationStore.GetRolesAsync(user);

            Assert.NotNull(roles);
        }
    }
}
