using Microsoft.Extensions.Hosting;
using RouletteWebApi;

namespace RouletteWebApi.UnitTests.Helpers
{
    public static class HostTestHelper
    {
        private static IHost _host;
        public static IHost Host
        {
            get
            {
                if (_host == null)
                {
                    _host = Program.CreateHostBuilder(null).Build();
                }
                return _host;
            }

        }
    }
}