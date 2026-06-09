using Microsoft.Playwright;
using playwrightWithCFramework.config;
namespace playwrightWithCFramework.Driver
{

    public static class DriverFactory
    {
        private static IBrowser _browser;
        // Initializes the browser based on the configuration settings and returns an instance of IBrowser.
        public static async Task<IBrowser> GetBrowserAsync()
        {
            if (_browser == null)
            {
                var playwright = await Playwright.CreateAsync();
                var browserType = ConfigManager.GetBrowser.ToLower();

                _browser = browserType switch
                {
                    "chromium" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless, Args = new[] { "--start-maximized" } }),
                    "chrome" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless, Args = new[] { "--start-maximized" } }),
                    "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless, Args = new[] { "--start-maximized" } }),
                    "webkit" => await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = ConfigManager.Headless, Args = new[] { "--start-maximized" } }),
                    _ => throw new ArgumentException($"Unsupported browser: {ConfigManager.GetBrowser}")
                };
            }
            return _browser;
        }
        // Initializes a new browser context and page, and returns the IPage instance for test interactions.
        public static async Task<IPage> InitBrowserAsync()
        {
            var browser = await GetBrowserAsync();
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null // Maximizes to screen size
            });
            return await context.NewPageAsync();
        }

        // Closes the browser instance if it is open and sets the reference to null for cleanup.
        public static async Task CloseBrowserAsync()
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
                _browser = null;
            }
        }
    }
}