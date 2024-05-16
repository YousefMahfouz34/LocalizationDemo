using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
namespace LoclizationWebApi.Sevices
{
    public class JsonStringLocilizer : IStringLocalizer
    {
        private readonly JsonSerializer _serializer = new();
        public LocalizedString this[string name]
            {
            get
            {
                var value=GetString(name);
                return new LocalizedString(name, value);
            }
            }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualvalue= this[name];
                return !actualvalue.ResourceNotFound ?
                    new LocalizedString(name,string.Format( actualvalue,arguments)) :
                    actualvalue;
                
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filepath = $"Resourses/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using FileStream stream = new(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamreader = new(stream);
            using JsonTextReader reader = new(streamreader);
            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    continue;
                   var key=reader.Value as string;
                   reader.Read();
                var value = _serializer.Deserialize<string>(reader);
                yield return new LocalizedString(key, value);   
                
            }

        }
        private String GetValueFromJson(string PropertyName,string FilePath) 
        {
            if (string.IsNullOrEmpty(FilePath)||string.IsNullOrEmpty(PropertyName) )
                return string.Empty;

            using FileStream stream = new(FilePath,FileMode.Open,FileAccess.Read,FileShare.Read);
            using StreamReader streamreader = new (stream);
            using JsonTextReader reader = new (streamreader); 
            while (reader.Read()) 
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == PropertyName)
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader);
                }
            }
            return string.Empty;

        }
        private string GetString(string key)
        {
            var filepath = $"wwwroot/Resourses/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullpath = Path.GetFullPath(filepath);
            if(File.Exists(fullpath))
            {
                var res= GetValueFromJson(key, fullpath);
                return res;

            }
            return string.Empty;
        }
    }
}
