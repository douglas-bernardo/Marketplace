namespace Marketplace.Infrastructure
{
    /// <summary>
    /// PurgoMalum is a simple, free, RESTful web service for filtering and removing content of profanity, obscenity and other unwanted text.
    /// Check http://www.purgomalum.com
    /// </summary>
    public class PurgomalumClient
    {
        private readonly HttpClient _httpClient;

        public PurgomalumClient() : this(new HttpClient())
        {
        }

        public PurgomalumClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CheckForProfanity(string text)
        {
            var response = await _httpClient.GetAsync(
                $"https://www.purgomalum.com/service/containsprofanity?text={Uri.EscapeDataString(text)}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return bool.Parse(result);
        }
    }
}
