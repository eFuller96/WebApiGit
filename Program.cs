using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiGit;

namespace webapi
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await GetGithubAccount();
        }

        private static async Task GetGithubAccount()
        {
            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.github.com/users/eFuller96");
            
            request.Headers.TryAddWithoutValidation("Authorization", "token adbde95cd6986e8e0875b2ceb8a08f23b557a7fd");
            // These two headers below are checked by the GitHub server code, and are necessary to retrieve information from GitHub.
            request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github.v3+json"); // configured to accept the GitHub JSON responses. This format is simply JSON.
            request.Headers.TryAddWithoutValidation("User-Agent", "agent"); // adds a User Agent header to all requests from this object. 
            
            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            var gitAccountDetails = JsonSerializer.Deserialize<GitAccount>(content, jsonSerializerOptions);

            Console.Write($"Name: {gitAccountDetails.Name}, Login: {gitAccountDetails.Login}");
        }
    }
}