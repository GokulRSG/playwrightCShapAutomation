using Microsoft.Playwright;
namespace playwrightWithCFramework.Base
{
    // The BasePage class serves as a foundational class for all page objects in the framework, providing common methods for interacting with web pages.
    public class BasePage
    {
        protected IPage Page;

        public BasePage(IPage page)
        {
            Page = page;
        }

        /// <summary>
        /// Clicks on the specified locator.
        /// </summary>
        /// <param name="locator">The locator to click.</param>
        public async Task ClickAsync(ILocator locator)
        {
            await locator.ClickAsync();
        }
        /// <summary>
        /// Gets the text content of the specified locator.
        /// </summary>
        /// <param name="locator">The locator to get text from.</param>
        /// <returns>The text content of the locator.</returns>
        public async Task<string> GetTextAsync(ILocator locator)
        {
            return await locator.InnerTextAsync();
        }

        /// <summary>
        /// Checks if the specified locator is visible on the page.
        /// </summary>
        /// <param name="locator">The locator to check.</param>
        /// <returns>True if the locator is visible, otherwise false.</returns>
        public async Task<bool> IsVisibleAsync(ILocator locator)
        {
            return await locator.IsVisibleAsync();
        }

        /// Enters text into the specified locator.
        /// </summary>
        /// <param name="locator">The locator to enter text into.</param>
        /// <param name="text">The text to enter.</param>
        public async Task enterTextAsync(ILocator locator, string text)
        {
            await locator.FillAsync(text);
        }

        /// Retrieves the inner text of the specified locator as a string.
        /// </summary> <param name="locator">The locator to retrieve text from.</param>
        /// <returns>The inner text of the locator.</returns>
        public async Task<string> GetStringAsync(ILocator locator)
        {
            return await locator.InnerTextAsync();
        }

        /// <summary>
        /// Waits for the page to finish loading after navigation or interaction.
        /// </summary>
        public async Task waitforPageLoad()
        {
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
}