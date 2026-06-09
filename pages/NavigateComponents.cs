using Microsoft.Playwright;
using playwrightWithCFramework.Base;
namespace playwrightWithCFramework.Pages
{
    // The NavigateComponents class provides methods to navigate to different components of the application, such as the login page and dashboard page.
    public class NavigateComponents : BasePage
    {
        public NavigateComponents(IPage page) : base(page)
        {
        }
        private ILocator dashboardPageLink => Page.Locator("//ul[@class='oxd-main-menu']//span[text()='Dashboard']");
        private ILocator adminPageLink => Page.Locator("//ul[@class='oxd-main-menu']//span[text()='Admin']");

        // Method to navigate to the dashboard page after successful login
        public async Task NavigateToDashboardPageAsync()
        {
            // Assuming that the dashboard page is accessible after login, you can add logic here to perform login if needed before navigating to the dashboard.
            await ClickAsync(dashboardPageLink);
        }

        public async Task NavigateToAdminPageAsync()
        {
            await ClickAsync(adminPageLink);
            await waitforPageLoad();
        }
    }
}