using Microsoft.Playwright;
using playwrightWithCFramework.Base;

namespace playwrightWithCFramework.Pages
{
    // The DashboardPage class represents the dashboard page of the application and provides methods to interact with its elements.
    public class DashboardPage : BasePage
    {
        // Locators for elements on the dashboard page
        private ILocator welcomeMessage => Page.Locator("h6.oxd-text.oxd-text--h6.oxd-topbar-header-breadcrumb-module");
        private ILocator userProfileIcon => Page.Locator("img[alt='profile picture']");
        private ILocator logoutButton => Page.Locator("a.oxd-userdropdown-link", new PageLocatorOptions { HasTextString = "Logout" });

        public DashboardPage(IPage page) : base(page)
        {
        }

        // Method to get the welcome message text from the dashboard page
        public async Task<string> GetWelcomeMessageAsync()
        {
            return await GetTextAsync(welcomeMessage);
        }

        // Method to perform logout action by clicking on the user profile icon and then the logout button
        public async Task LogoutAsync()
        {
            await ClickAsync(userProfileIcon);
            await ClickAsync(logoutButton);
        }
    }
}