using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Your OpenAI API key
        string openAiApiKey = "your-openai-api-key";

        // Your Twitter API key
        string twitterApiKey = "your-twitter-api-key";

        // Initialises the AI content generator and social media poster
        AIContentGenerator contentGenerator = new AIContentGenerator(openAiApiKey);
        SocialMediaPoster socialMediaPoster = new SocialMediaPoster(twitterApiKey);

        // Initialises the content scheduler
        ContentScheduler scheduler = new ContentScheduler();

        // Starts the scheduler with the content generator and social media poster
        await scheduler.StartAsync(contentGenerator, socialMediaPoster);

        // Keeps the application running
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        // Stops the scheduler when exiting
        await scheduler.StopAsync();
    }
}
