using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// AIContentGenerator is responsible for generating content using the OpenAI API.

public class AIContentGenerator
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    // Initialises a new instance of the AIContentGenerator class.
    // <param name="apiKey">API key for OpenAI.</param>
    public AIContentGenerator(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
    }

    // Generates text based on a given prompt using the OpenAI API.
    // <param name="prompt">The prompt to generate content from.</param>
    // <returns>The generated text.</returns>
    public async Task<string> GenerateTextAsync(string prompt)
    {
        // Creates the request payload with the prompt and other parameters.
        var requestBody = new
        {
            prompt = prompt,
            max_tokens = 100,
            temperature = 0.7,
        };

        // Converts the payload to JSON format.
        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");

        // Sets the authorization header with the API key.
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

        // Sends the request to the OpenAI API.
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", jsonContent);

        // Parses the response and extract the generated text.
        var responseString = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonSerializer.Deserialize<JsonDocument>(responseString);
        var generatedText = jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("text").GetString();

        return generatedText;
    }
}
