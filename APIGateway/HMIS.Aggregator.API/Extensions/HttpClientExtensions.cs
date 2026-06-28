using CommonExceptionHandler;
using System.Text.Json;

namespace HMIS.Aggregator.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(dataAsString))
                throw new UserFriendlyException("Null Response From Service!");

            return JsonSerializer.Deserialize<T>(dataAsString!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
