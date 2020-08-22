using Systore.Domain;

namespace Systore.Tests.Common.Builders
{
    public class AppSettingsBuilder
    {
        private readonly AppSettings _instance;

        public AppSettingsBuilder()
        {
            _instance = new AppSettings()
            {
                ClientId = "12345",
                DatabaseType = "InMen",
                Secret = "188aef56-677c-4fa5-b884-3cba4ed75e07",
                UrlRelease = "http://url"
            };
        }

        public AppSettings Build() => _instance;
    }
}
