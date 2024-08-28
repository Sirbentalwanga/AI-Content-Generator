using Quartz;

// DailyPostJob is a Quartz job responsible for generating and posting content daily.
public class DailyPostJob : IJob
{
    // Executes the job to generate and post content.
    // <param name="context">Job execution context containing job data.</param>
    // <returns>A Task representing the asynchronous operation.</returns>
    public async Task Execute(IJobExecutionContext context)
    {
        // Retrieves instances of AIContentGenerator and SocialMediaPoster from the job context.
        var contentGenerator = (AIContentGenerator)context.MergedJobDataMap["contentGenerator"];
        var socialMediaPoster = (SocialMediaPoster)context.MergedJobDataMap["socialMediaPoster"];

        // Generates content using a specific prompt.
        string content = await contentGenerator.GenerateContentAsync("Generate a tweet about AI and innovation.");

        // Posts the generated content to Twitter.
        await socialMediaPoster.PostToTwitterAsync(content);
    }
}
