using Microsoft.Extensions.Localization;

namespace LoclizationWebApi.Sevices
{
    public class JsonStringLocilizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocilizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocilizer();

        }
    }
}
