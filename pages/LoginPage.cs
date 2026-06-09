using Microsoft.Playwright;
using playwrightWithCFramework.Base;
using playwrightWithCFramework.config;

namespace playwrightWithCFramework.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IPage page) : base(page)
        {
        }

        private ILocator userNameInput => Page.GetByPlaceholder("Username");
        private ILocator passwordInput => Page.GetByPlaceholder("Password");
        private ILocator loginButton => Page.Locator("button[type='submit']");
        private ILocator lblUserName => Page.Locator("//p[@class='oxd-text oxd-text--p'][1]");
        private ILocator lblPassword => Page.Locator("//p[@class='oxd-text oxd-text--p'][2]");
        public async Task LoginAsync(string username, string password)
        {
            await enterTextAsync(userNameInput, username);
            await enterTextAsync(passwordInput, password);
            await ClickAsync(loginButton);
        }

        /// Verifies that the login page is displayed by checking the visibility of username and password labels.
        /// <returns>True if both labels are visible, otherwise false.</returns>
        public async Task<bool> IsLoginPageDisplayedAsync()
        {
            return await lblUserName.IsVisibleAsync() && await lblPassword.IsVisibleAsync();
        }

        /// <summary>
        /// Retrieves the username from the login page.
        /// </summary>
        /// <returns>The username as a string.</returns>
        public async Task<string> getUserNameAsync()
        {
            return await GetStringAsync(lblUserName);
        }

        /// <summary>
        /// Retrieves the password from the login page.
        /// </summary>
        /// <returns>The password as a string.</returns>
        public async Task<string> getPasswordAsync()
        {
            return await GetStringAsync(lblPassword);
        }

        public async Task<bool> IsLoginSuccessfulAsync()
        {
            // Verify login success by waiting for the dashboard heading or landing page text.
            try
            {
                var dashboardElement = Page.GetByText("Dashboard");
                await dashboardElement.WaitForAsync(new LocatorWaitForOptions { Timeout = ConfigManager.GetTimeout });
                return true;
            }
            catch (PlaywrightException)
            {
                return false;
            }
        }
    }
}