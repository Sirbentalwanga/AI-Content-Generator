using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

// ContentScheduler is responsible for setting up and managing the scheduled jobs for content generation and posting.

public class ContentScheduler
{
    private readonly IScheduler _scheduler;

    // Initialises a new instance of the ContentScheduler class and configures the Quartz scheduler.
    public ContentScheduler()
    {
        // Creates a new scheduler factory and get a scheduler instance.
        StdSchedulerFactory factory = new StdSchedulerFactory();
        _scheduler = factory.GetScheduler().Result;
    }

    // Starts the scheduler and schedules the daily content posting job.
    // <param name="contentGenerator">Instance of AIContentGenerator for generating content.</param>
    // <param name="socialMediaPoster">Instance of SocialMediaPoster for posting content.</param>
    public async Task StartAsync(AIContentGenerator contentGenerator, SocialMediaPoster socialMediaPoster)
    {
        // Defines the job and tie it to the DailyPostJob class.
        IJobDetail job = JobBuilder.Create<DailyPostJob>()
            .WithIdentity("dailyPostJob", "group1")
            .UsingJobData("contentGenerator", contentGenerator)
            .UsingJobData("socialMediaPoster", socialMediaPoster)
            .Build();

        // Triggers the job to run daily at a specific time (e.g., 9:00 AM).
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("dailyPostTrigger", "group1")
            .StartNow()
            .WithDailyTimeIntervalSchedule(s =>
                s.WithIntervalInHours(24)
                 .OnEveryDay()
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 0)))
            .Build();

        // Schedules the job using the trigger.
        await _scheduler.ScheduleJob(job, trigger);

        // Starts the scheduler.
        await _scheduler.Start();
    }

    // Stops the scheduler.
    public async Task StopAsync()
    {
        // Shuts down the scheduler if it is not already shut down.
        if (!_scheduler.IsShutdown)
        {
            await _scheduler.Shutdown();
        }
    }
}
