using Microsoft.Playwright;
using playwrightWithCFramework.Driver;

namespace playwrightWithCFramework.Base
{
    public class Hooks
    {
        protected IPage Page;
        protected IBrowser Browser;

        // Global setup method that runs once before any tests are executed. It initializes the browser and page instances for use in tests.
        [SetUp]
        public async Task GlobalSetup()
        {
            // Global setup logic, e.g., initializing logging, test data, etc.
            Console.WriteLine("Global setup before any tests run.");
           
            Browser = await DriverFactory.GetBrowserAsync();
            Page = await DriverFactory.InitBrowserAsync();
            // Optionally, navigate to a base URL if needed
            await Page.GotoAsync(config.ConfigManager.GetBaseUrl);

        }

        // Additional setup before each test can be added here if needed
        [TearDown]
        public async Task GlobalTeardown()
        {
            // Global teardown logic, e.g., cleaning up resources, generating reports, etc.
            Console.WriteLine("Global teardown after all tests have run.");
            if(TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                // Capture screenshot on test failure
                var screenshotPath = $"Screenshots/Failure_{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                await Utilities.ScreenshotHelper.CaptureScreenshotAsync(Page, screenshotPath);
                Console.WriteLine($"Screenshot captured for failed test: {screenshotPath}");
            }
            await DriverFactory.CloseBrowserAsync();
        }
    }
}