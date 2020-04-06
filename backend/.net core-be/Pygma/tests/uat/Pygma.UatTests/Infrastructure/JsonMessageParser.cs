using System.Text.Json;

namespace Pygma.UatTests.Infrastructure
{
    public class JsonMessageParser<T>
    {
        private readonly JsonSerializerOptions options;

        public JsonMessageParser()
        {
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public string Serialize(T content) => JsonSerializer.Serialize(content, options);

        public T Deserialize(string responseAsString) => JsonSerializer.Deserialize<T>(responseAsString, options);
    }
}