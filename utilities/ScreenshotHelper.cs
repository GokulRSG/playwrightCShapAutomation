using Microsoft.Playwright;
using playwrightWithCFramework.Base;
namespace playwrightWithCFramework.Utilities
{
    public static class ScreenshotHelper
    {
        // Method to capture a screenshot of the current page and save it to the specified file path.
        public static async Task CaptureScreenshotAsync(IPage page, string filePath)
        {
            try
            {
                await page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath, FullPage = true });
                Console.WriteLine($"Screenshot captured and saved to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
            }
        }
    }
}