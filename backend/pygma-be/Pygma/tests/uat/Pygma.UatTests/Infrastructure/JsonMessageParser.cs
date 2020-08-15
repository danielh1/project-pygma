using System.Text.Json;

namespace Pygma.UatTests.Infrastructure
{
    public class JsonMessageParser<T>
    {
        private readonly JsonSerializerOptions _options;

        public JsonMessageParser()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public string Serialize(T content) => JsonSerializer.Serialize(content, _options);

        public T Deserialize(string responseAsString) => JsonSerializer.Deserialize<T>(responseAsString, _options);
    }
}