using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// SocialMediaPoster is responsible for posting content to social media platforms.
public class SocialMediaPoster
{
    private readonly string _twitterApiToken;
    private readonly HttpClient _httpClient;

    // Initialises a new instance of the SocialMediaPoster class.
    // <param name="twitterApiToken">API token for Twitter.</param>
    public SocialMediaPoster(string twitterApiToken)
    {
        _twitterApiToken = twitterApiToken;
        _httpClient = new HttpClient();
    }

    // Posts content to Twitter using the Twitter API.
    // <param name="content">The content to post on Twitter.</param>
    // <returns>A Task representing the asynchronous operation.</returns>
    public async Task PostToTwitterAsync(string content)
    {
        // Creates the request payload with the content to post.
        var requestBody = new
        {
            status = content
        };

        // Converts the payload to JSON format.
        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");

        // Sets the authorization header with the API token.
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _twitterApiToken);

        // Sends the request to the Twitter API.
        var response = await _httpClient.PostAsync("https://api.twitter.com/1.1/statuses/update.json", jsonContent);

        // Checks if the response was successful.
        response.EnsureSuccessStatusCode();
    }
}
